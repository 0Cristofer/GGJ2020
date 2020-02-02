using UnityEngine;

namespace Domains
{
    public class BearItem
    {
        public Vector2 Position { get; private set; }
        
        private ItemColor _itemColor;
        private ItemType _itemType;

        public BearItem(ItemColor itemColor, ItemType itemType)
        {
            _itemColor = itemColor;
            _itemType = itemType;
        }
    }

    public enum ItemColor
    {
        Blue, Green, Red
    }

    public enum ItemType
    {
        Head, Torso, Arm, Leg
    }
}