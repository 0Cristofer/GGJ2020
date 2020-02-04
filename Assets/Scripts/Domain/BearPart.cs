namespace Domain
{
	public struct BearPart
	{
		public PartColor PartColor { get; }
		public PartType PartType { get; }

		public BearPart(PartType partType, PartColor partColor)
		{
			PartType = partType;
			PartColor = partColor;
		}

		public static bool operator ==(BearPart a, BearPart b)
		{
			return (a.PartType == b.PartType) && (a.PartColor == b.PartColor);
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
				return ((int) PartColor * 397) ^ (int) PartType;
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