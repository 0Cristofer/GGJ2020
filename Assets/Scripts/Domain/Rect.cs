using System.Numerics;

namespace Domain
{
	public struct Rect
	{
		private readonly Vector2 _leftBottomCorner;
		private readonly Vector2 _topRightCorner;

		public Rect(Vector2 leftBottomCorner, Vector2 topRightCorner)
		{
			_leftBottomCorner = leftBottomCorner;
			_topRightCorner = topRightCorner;
		}

		public bool IsPointInside(Vector2 point)
		{
			bool isInsideX, isInsideY;

			isInsideX = point.X >= _leftBottomCorner.X && point.X <= _topRightCorner.X;
			isInsideY = point.Y >= _leftBottomCorner.Y && point.Y <= _topRightCorner.Y;

			return isInsideX && isInsideY;
		}

		public Vector2 Center()
		{
			float difX = _topRightCorner.X - _leftBottomCorner.X;
			float difY = _topRightCorner.Y - _leftBottomCorner.Y;
			float midX = _leftBottomCorner.X + difX / 2;
			float midY = _leftBottomCorner.Y + difY / 2;
			
			return new Vector2(midX, midY);
		}
	}
}