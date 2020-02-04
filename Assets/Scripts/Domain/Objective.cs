using System.Collections.Generic;

namespace Domain
{
	public struct Objective
	{
		public Rect CarpetArea { get; }
		private readonly List<BearPart> _bearParts;

		public Objective(Rect carpetArea, List<BearPart> bearParts)
		{
			CarpetArea = carpetArea;
			_bearParts = bearParts;
		}

		internal bool AddProgress(BearPart bearPart)
		{
			return _bearParts.Remove(bearPart);
		}

		internal bool IsObjectiveReached()
		{
			return _bearParts.Count == 0;
		}
	}
}