using UnityEngine;

namespace Domains
{
    public class BearItem
    {
        public Vector2 Position { get; set; }
        
        private ItemColor _itemColor;
        private ItemType _itemType;

        public BearItem(Vector2 position, ItemColor itemColor, ItemType itemType)
        {
            position.y++;
            Debug.Log("Starting item at: " + position);
            Position = position;
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