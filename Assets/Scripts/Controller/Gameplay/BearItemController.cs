using Domain;
using UnityEngine;
using Util;

namespace Controller.Gameplay
{
	public class BearItemController : MonoBehaviour, IBearPartListener
	{
		public BearItem BearItem { get; set; }

		[SerializeField]
		private PartType _partType = PartType.Torso;
		[SerializeField]
		private PartColor _partColor = PartColor.Blue;

		#region INIT
		public void Init()
		{
			BearItem = new BearItem(transform.position.ToWorldVector2(), new BearPart(_partType, _partColor));
			BearItem.AddListener(this);
		}
		#endregion

		#region DOMAIN_LISTENER
		public void OnPositionChanged(System.Numerics.Vector2 previousPosition)
		{
			transform.position = BearItem.Position.ToUnityVector2();
		}
		#endregion
	}
}