using System.Collections.Generic;
using UnityEngine;

namespace Domains
{
    public class Player
    {
        public Vector2 Position { get; private set; }

        private readonly World _world;
        private readonly List<BearItem> _bearItems;
        private BearItem _currentItem;
        public CarpetPosition CarpetPosition { get; private set; }

        public Player(World world, CarpetPosition carpetPosition)
        {
            _world = world;
            _bearItems = new List<BearItem>();
            CarpetPosition = carpetPosition;
        }

        public void ChangeItem()
        {
            if (_currentItem == null)
            {
                _currentItem = _world.PickItem(this);
                return;
            }

            BearItem newItem = _world.PickItem(this);
            _world.DropItem(this, _currentItem);
            _currentItem = newItem;
        }

        public void AddItem(BearItem item)
        {
            _bearItems.Add(item);
        }

        public void MoveUp()
        {
            Position = new Vector2(Position.x, Position.y + 1);
        }
        
        public void MoveDown()
        {
            Position = new Vector2(Position.x, Position.y - 1);
        }
        
        public void MoveRight()
        {
            Position = new Vector2(Position.x + 1, Position.y);
        }
        
        public void MoveLeft()
        {
            Position = new Vector2(Position.x - 1, Position.y);
        }
    }
}