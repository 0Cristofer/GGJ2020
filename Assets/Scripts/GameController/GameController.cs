using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controller.Gameplay;
using Domains;
using UnityEngine;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        private readonly List<List<Vector2>> _corners =
        new List<List<Vector2>> {
            new List<Vector2> {
                new Vector2(0, 0), new Vector2(0, 1), 
                new Vector2(1, 0), new Vector2(1, 1)
            },
            new List<Vector2> {
                new Vector2(13, 7), new Vector2(14, 7), 
                new Vector2(13, 8), new Vector2(14, 18)
            },
            new List<Vector2> {
                new Vector2(13, 0), new Vector2(14, 0), 
                new Vector2(13, 1), new Vector2(14, 1)
            },
            new List<Vector2> {
                new Vector2(0, 7), new Vector2(1, 7), 
                new Vector2(0, 8), new Vector2(1, 8)
            }
        };

        private bool _running;
        private const float TickTime = 0.016f;
        private int _tickRate;
        private int _currentTick;
        private Queue<GameEvent> _eventQueue;

        [SerializeField]
        private InputController _inputController = null;

        [SerializeField]
        private WorldController _worldController = null;
        [SerializeField]
        private List<BearItemController> _bearItemsControllers = null;
        [SerializeField]
        private List<PlayerController> _playersControllers = null;


        public void Init(List<BearItemController> player1Objective, List<BearItemController> player2Objective)
        {
            int playerQuantity = ApplicationController.Instance.GetNextGamePlayerQuantity();

            List<BearItem> bearItems = new List<BearItem>();
            foreach (BearItemController bearItemController in _bearItemsControllers)
            {
                bearItemController.Init();
                bearItems.Add(bearItemController.GetDomain());
            }


            List<BearItem> player1BearItems = player1Objective.Select(bearItemController => bearItemController.GetDomain()).ToList();
            List<BearItem> player2BearItems = player2Objective.Select(bearItemController => bearItemController.GetDomain()).ToList();
            List<List<BearItem>> playerObjectives = new List<List<BearItem>> {player1BearItems, player2BearItems};

            World world = new World(bearItems, _corners);
            
            List<Player> players = new List<Player>();
            List<PlayerController> toDestroyPlayerControllers = new List<PlayerController>();

            List<CarpetPosition> carpetPositions = EnumUtil.GetValues<CarpetPosition>().ToList();
            int i;
            for (i = 0; i < playerQuantity; i++)
            {
                Player newPlayer = new Player(world, _corners[i][0], carpetPositions[i], playerObjectives[i]);
                _playersControllers[i].transform.position = WorldUtil.ToWorldPos(_corners[i][0]);
                _playersControllers[i].name = "Player" + i;
                _playersControllers[i].Init(newPlayer);
                players.Add(newPlayer);
            }
            
            for (; i < _playersControllers.Count; i++) 
                toDestroyPlayerControllers.Add(_playersControllers[i]);

            foreach (PlayerController toDestroyPlayerController in toDestroyPlayerControllers)
            {
                _playersControllers.Remove(toDestroyPlayerController);
                Destroy(toDestroyPlayerController.gameObject);
            }
            
            _worldController.Init(world, _playersControllers, _bearItemsControllers);
            world.SetPlayers(players);
            
            StartTicking();
        }

        public void EndGame()
        {
            Debug.Log("Ending Game");
            _running = false;
            _worldController.EndGame();
        }

        public void GameWon(Player player)
        {
            _running = false;
            Debug.Log("Game won");
        }

        public void EnqueueEvent(GameEvent inputEvent)
        {
            _eventQueue.Enqueue(inputEvent);
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                _currentTick++;
            
                while (_eventQueue.Count != 0) 
                {
                    GameEvent gameEvent = _eventQueue.Dequeue();

                    HandleEvent(gameEvent);
                }
            
                yield return new WaitForSeconds(TickTime);
            }
        }

        private void HandleEvent(GameEvent gameEvent)
        {
            if (gameEvent.HasJoystickInput)
            {
                gameEvent.PlayerController.Move(gameEvent.JoystickInput.normalized);
                return;
            }
                    
            switch (gameEvent.InputKey)
            {
                case InputKey.ItemChange:
                    gameEvent.PlayerController.ChangeItem();
                    break;
                default:
                    Debug.LogWarning("Unhandled Input Event :" + EnumUtil.GetEnumValueName(gameEvent.InputKey));
                    break;
            }
        }
    
        private void StartTicking()
        {
            _inputController.Init(_playersControllers);
        
            _tickRate = (int) (1 / TickTime);
            _eventQueue = new Queue<GameEvent>();
        
            Debug.Log("Starting Game Controller at " + _tickRate + " ticks per second");

            _running = true;
            StartCoroutine(Tick());
        }
    }

    public class GameEvent
    {
        public PlayerController PlayerController { get; private set; }
        public InputKey InputKey { get; private set; }
        public Vector2 JoystickInput { get; private set; }
        public bool HasJoystickInput { get; private set; }
            
        public GameEvent(PlayerController playerController, InputKey inputKey)
        {
            PlayerController = playerController;
            InputKey = inputKey;
            HasJoystickInput = false;
        }

        public GameEvent(PlayerController playerController, Vector2 joystickInput)
        {
            PlayerController = playerController;
            JoystickInput = joystickInput;
            HasJoystickInput = true;
        }
    }
}