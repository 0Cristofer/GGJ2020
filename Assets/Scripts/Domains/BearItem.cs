using UnityEngine;

namespace Domains
{
    public class BearItem
    {
        public Vector2 Position { get; private set; }
        private ItemColor _itemColor;

        public BearItem(ItemColor itemColor)
        {
            _itemColor = itemColor;
        }
    }

    public enum ItemColor
    {
        Blue, Green, Red
    }
}