using System.Collections.Generic;
using System.Numerics;

namespace Domain
{
	public class BearItem
	{
		public Vector2 Position { get; private set; }
		public BearPart BearPart { get; }

		private readonly List<IBearPartListener> _bearPartListeners;

		public BearItem(Vector2 position, BearPart bearPart)
		{
			Position = position;
			BearPart = bearPart;

			_bearPartListeners = new List<IBearPartListener>();
		}

		#region INTERNAL_INPUT
		internal void SetPosition(Vector2 newPosition)
		{
			Vector2 previousPosition = Position;

			Position = newPosition;
			FireOnPositionChanged(previousPosition);
		}
		#endregion

		#region LISTENERS_INVOCATION
		private void FireOnPositionChanged(Vector2 previousPosition)
		{
			foreach (IBearPartListener listener in _bearPartListeners)
			{
				listener.OnPositionChanged(previousPosition);
			}
		}
		#endregion

		#region LISTENER_HANDLERS
		public void AddListener(IBearPartListener listener)
		{
			listener.BearItem = this;
			_bearPartListeners.Add(listener);
		}

		public bool RemoveListener(IBearPartListener listener)
		{
			return _bearPartListeners.Remove(listener);
		}
		#endregion
	}

	public interface IBearPartListener
	{
		BearItem BearItem { get; set; }
		void OnPositionChanged(Vector2 previousPosition);
	}
}