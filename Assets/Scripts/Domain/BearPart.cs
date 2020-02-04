namespace Domain
{
	public struct BearPart
	{
		private readonly PartColor _partColor;
		private readonly PartType _partType;

		public BearPart(PartType partType, PartColor partColor)
		{
			_partType = partType;
			_partColor = partColor;
		}

		public static bool operator ==(BearPart a, BearPart b)
		{
			return (a._partType == b._partType) && (a._partColor == b._partColor);
		}

		public static bool operator !=(BearPart a, BearPart b)
		{
			return !(a == b);
		}

		public override bool Equals(object obj)
		{
			return obj is BearPart other && (this == other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((int) _partColor * 397) ^ (int) _partType;
			}
		}
	}

	public enum PartType
	{
		Torso,
		ArmLeft,
		Head,
		LegLeft,
		ArmRight,
		LegRight,
	}

	public enum PartColor
	{
		Blue,
		Brown,
		Pink,
		Green
	}
}