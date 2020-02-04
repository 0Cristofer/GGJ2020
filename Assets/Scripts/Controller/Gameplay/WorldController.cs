using Domain;
using UnityEngine;

namespace Controller.Gameplay
{
	public class WorldController : MonoBehaviour, IWorldListener
	{
		public World World { get; set; }

		#region INIT
		public void Init(World world)
		{
			world.AddListener(this);
		}
		#endregion

		#region DOMAIN_LISTENERS
		public void OnItemGrabbed(BearItem item, Player player)
		{
		}

		public void OnItemDropped(BearItem item)
		{
		}
		#endregion
	}
}