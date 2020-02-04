using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controller.Gameplay;
using Domain;
using TMPro;
using UnityEngine;
using Util;
using Rect = Domain.Rect;

namespace GameManager
{
	public class GameController : MonoBehaviour
	{
		private bool _running;
		private const float TickTime = 0.016f;
		private int _tickRate;
		private int _currentTick;
		private Queue<GameEvent> _eventQueue;

		[SerializeField]
		private List<GameObject> _spawnPoints = default;

		[SerializeField]
		private InputController _inputController = null;

		[SerializeField]
		private WorldController _worldController = null;
		[SerializeField]
		private List<BearItemController> _bearItemsControllers = null;
		[SerializeField]
		private List<PlayerController> _playersControllers = null;

		[SerializeField]
		private TimerController _timerController;
		[SerializeField]
		private TextMeshProUGUI _text = null;


		public void Init(IEnumerable<BearItemController> player1Objective,
			IEnumerable<BearItemController> player2Objective)
		{
			_text.gameObject.SetActive(false);
			int playerQuantity = ApplicationController.Instance.GetNextGamePlayerQuantity();

			List<BearItem> bearItems = new List<BearItem>();

			foreach (BearItemController bearItemController in _bearItemsControllers)
			{
				bearItemController.Init();
				bearItems.Add(bearItemController.BearItem);
			}


			List<BearPart> player1BearItems = player1Objective
				.Select(bearItemController => bearItemController.BearItem.BearPart).ToList();
			List<BearPart> player2BearItems = player2Objective
				.Select(bearItemController => bearItemController.BearItem.BearPart).ToList();
			List<List<BearPart>> playerObjectives = new List<List<BearPart>> {player1BearItems, player2BearItems};

			List<Player> players = new List<Player>();
			List<PlayerController> toDestroyPlayerControllers = new List<PlayerController>();
			List<List<GameObject>> spawnRects = new List<List<GameObject>>();

			int i;
			for (i = 0; i < _spawnPoints.Count; i++)
			{
				if (i % 2 == 0)
					spawnRects.Add(new List<GameObject>());
				
				spawnRects[i/2].Add(_spawnPoints[i]);
			}

			for (i = 0; i < playerQuantity; i++)
			{
				_playersControllers[i].name = "Player" + i;

				System.Numerics.Vector2 bottomLeft = spawnRects[i][0].transform.position.ToWorldVector2();
				System.Numerics.Vector2 topRight = spawnRects[i][1].transform.position.ToWorldVector2();
				Rect spawnPos = new Rect(bottomLeft, topRight);

				Player newPlayer = new Player(_playersControllers[i].name, bottomLeft,
					new Objective(spawnPos, playerObjectives[i]));

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

			World world = new World(new WorldConfig(0.6f), players, bearItems);
			_worldController.Init(world);

			StartTicking();
		}

		public void EndGame()
		{
			SetFinalText("No more time");
			StopTicking();
		}

		public void GameWon(Player player)
		{
			_timerController._gameEnded = true;
			SetFinalText("Player " + player.Name + "Won!");
			StopTicking();
		}

		public void EnqueueEvent(GameEvent inputEvent)
		{
			_eventQueue.Enqueue(inputEvent);
		}

		private IEnumerator Tick()
		{
			while (_running)
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

		private void StopTicking()
		{
			_running = false;
		}

		private void SetFinalText(string text)
		{
			_text.text = text;
			_text.gameObject.SetActive(true);
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