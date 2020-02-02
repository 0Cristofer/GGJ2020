using Domains;
using UnityEngine;

namespace Controller.Gameplay
{
    public class BearItemController: MonoBehaviour
    {
        [SerializeField]
        private ItemType _itemType = ItemType.Torso;
        [SerializeField]
        private ItemColor _itemColor = ItemColor.Blue;
        private BearItem _bearItem;

        public void Init()
        {
            _bearItem = new BearItem(WorldUtil.ToGridPos(gameObject.transform.position), _itemColor, _itemType);
        }

        public BearItem GetDomain()
        {
            return _bearItem;
        }
    }
}