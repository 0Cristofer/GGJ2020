namespace Domain
{
	public struct WorldConfig
	{
		public float MaxPickDistance { get; }

		public WorldConfig(float maxPickDistance)
		{
			MaxPickDistance = maxPickDistance;
		}
	}
}