using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace GameController
{
    public class GameController : MonoBehaviour
    {
        private List<List<Vector2>> corners =
        new List<List<Vector2>> {
            new List<Vector2> {
                new Vector2(0, 0), new Vector2(0, 1), 
                new Vector2(1, 0), new Vector2(1, 1)
            },
            new List<Vector2> {
                new Vector2(0, 0), new Vector2(0, 1), 
                new Vector2(1, 0), new Vector2(1, 1)
            },
            new List<Vector2> {
                new Vector2(0, 0), new Vector2(0, 1), 
                new Vector2(1, 0), new Vector2(1, 1)
            },
            new List<Vector2> {
                new Vector2(0, 0), new Vector2(0, 1), 
                new Vector2(1, 0), new Vector2(1, 1)
            }
        };
        
        private const float TickTime = 0.1f;
        private int _tickRate;
        private int _currentTick;

        private Queue<GameEvent> _eventQueue;

        private List<PlayerController> _players;

        [SerializeField]
        private InputController _inputController = null;
        [SerializeField]
        private GameObject _playerPrefab = null;

        private void Init(int numPlayers)
        {
            _players = new List<PlayerController>();
        
            for (int i = 0; i < numPlayers; i++)
            {
                GameObject newPlayer = Instantiate(_playerPrefab);
                newPlayer.name = _playerPrefab.name + i;
                _players.Add(newPlayer.GetComponent<PlayerController>());
            }
        
            StartTicking();
        }

        private void StartTicking()
        {
            _inputController.Init(_players);
        
            _tickRate = (int) (1 / TickTime);
            _eventQueue = new Queue<GameEvent>();
        
            Debug.Log("Starting Game Controller at " + _tickRate + " ticks per second");

            StartCoroutine(Tick());
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
            
                    switch (gameEvent.InputKey)
                    {
                        case InputKey.Fire1:
                            Debug.Log("Game received fire1");
                            gameEvent.PlayerController.Fire1();
                            break;
                        case InputKey.Jump:
                            Debug.Log("Game received jump");
                            gameEvent.PlayerController.Jump();
                            break;
                        case InputKey.Horizontal:
                            break;
                        case InputKey.Vertical:
                            break;
                        default:
                            Debug.LogWarning("Unhandled Input Event :" + EnumUtil.GetEnumValueName(gameEvent.InputKey));
                            break;
                    }
                }
            
                yield return new WaitForSeconds(TickTime);
            }
        }
    
        private void Start()
        {
            Init(2);
        }
    }

    public class GameEvent
    {
        public PlayerController PlayerController { get; private set; }
        public InputKey InputKey { get; private set; }

        public GameEvent(PlayerController playerController, InputKey inputKey)
        {
            PlayerController = playerController;
            InputKey = inputKey;
        }
    }
}