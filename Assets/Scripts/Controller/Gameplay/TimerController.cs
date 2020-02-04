using GameManager;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.Gameplay
{
	public class TimerController : MonoBehaviour
	{
		private float _seconds;
		private float _minutes;
		private float _milliseconds;

		[SerializeField]
		private float _timerToStart = default;
		private bool _started = false;

		[SerializeField]
		private float _timerToEnd = default;

		[SerializeField]
		private float _timerToEmergency = default;

		[SerializeField]
		private Text _watchText = default;

		[SerializeField]
		private Animator _timerAnimator = default;
		[SerializeField]
		private GameController _gameController = default;

		public bool _gameEnded = false;

		private void Update()
		{
			if (!_started)
			{
				_timerToStart -= Time.deltaTime;
				if (_timerToStart <= 0)
				{
					_started = true;
				}
			}

			if (_gameEnded)
			{
				return;
			}

			if (_timerToEnd >= 0 && _started)
			{
				_timerToEnd -= Time.deltaTime;
				_seconds = _timerToEnd % 60;
				_minutes = (_timerToEnd / 60);
				_milliseconds = (_timerToEnd * 99) % 99;

				_watchText.text = ((int) _minutes).ToString("00") + ":" + ((int) _seconds).ToString("00") + ":" +
				                  ((int) _milliseconds).ToString("00");
			}

			if ((_timerToEnd <= 0) && (!_gameEnded))
			{
				_gameEnded = true;
				_gameController.EndGame();
			}

			if (_timerToEmergency >= _timerToEnd)
			{
				_timerAnimator.Play("TimerEnding");
			}
		}
	}
}