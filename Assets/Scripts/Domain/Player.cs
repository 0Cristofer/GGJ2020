using System.Collections.Generic;
using System.Numerics;

namespace Domain
{
	public class Player
	{
		public const float Velocity = 0.035f;

		public Vector2 Position { get; private set; }

		public string Name { get; private set; }
		private World _world;
		private BearItem _currentItem;
		private Objective _objective;

		private bool _progressUpdated;
		
		private readonly List<IPlayerUpdatedListener> _playerListeners;

		public Player(string name, Vector2 initialPosition, Objective objective)
		{
			Position = initialPosition;

			Name = name;
			_objective = objective;

			_playerListeners = new List<IPlayerUpdatedListener>();
		}

		public void SetPosition(Vector2 newPosition)
		{
			Vector2 previousPosition = Position;

			Position = newPosition;

			_currentItem?.SetPosition(newPosition);

			FireOnPositionChanged(previousPosition);
		}

		public void ChangeItem()
		{
			DropItem();
			PickItem();
			CheckObjectiveReached();
		}

		private void PickItem()
		{
			_currentItem = _world.PickItem(this);

			if (_currentItem == null) 
				return;
			
			FireOnItemPicked();
		}

		private void DropItem()
		{
			if (_currentItem == null)
				return;

			UpdateProgress();

			_world.DropItem(_currentItem);
			FireOnItemDropped();
		}

		private void UpdateProgress()
		{
			if (!_objective.CarpetArea.IsPointInside(Position))
			{
				_progressUpdated = false;
				return;
			}

			if (!_objective.AddProgress(_currentItem.BearPart))
			{
				_progressUpdated = false;
				return;
			}
			
			FireOnObjectiveUpdated();
		}

		private void CheckObjectiveReached()
		{
			if (!_progressUpdated)
				return;
			
			if (_objective.IsObjectiveReached())
				FireObjectiveReached();
		}

		internal void SetWorld(World world)
		{
			_world = world;
		}

		private void FireOnPositionChanged(Vector2 previousPosition)
		{
			foreach (IPlayerUpdatedListener listener in _playerListeners)
			{
				listener.OnPositionChanged(previousPosition);
			}
		}

		private void FireOnItemPicked()
		{
			foreach (IPlayerUpdatedListener listener in _playerListeners)
			{
				listener.OnItemPicked();
			}
		}

		private void FireOnItemDropped()
		{
			foreach (IPlayerUpdatedListener listener in _playerListeners)
			{
				listener.OnItemDropped();
			}
		}

		private void FireOnObjectiveUpdated()
		{
			foreach (IPlayerUpdatedListener listener in _playerListeners)
			{
				listener.OnObjectiveUpdated();
			}
		}

		private void FireObjectiveReached()
		{
			foreach (IPlayerUpdatedListener listener in _playerListeners)
			{
				listener.OnObjectiveReached();
			}
		}

		public void AddListener(IPlayerUpdatedListener listener)
		{
			listener.Player = this;
			_playerListeners.Add(listener);
		}

		public bool RemoveListener(IPlayerUpdatedListener listener)
		{
			return _playerListeners.Remove(listener);
		}
	}

	public interface IPlayerUpdatedListener
	{
		Player Player { get; set; }

		void OnPositionChanged(Vector2 previousPosition);
		void OnItemPicked();
		void OnItemDropped();
		void OnObjectiveUpdated();
		void OnObjectiveReached();
	}
}