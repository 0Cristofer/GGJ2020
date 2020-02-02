﻿using System.Collections.Generic;
using UnityEngine;

namespace Domains
{
    public class Player
    {
        public const float Velocity = 0.02f;
        public Vector2 Position { get; private set; }

        private readonly World _world;
        private readonly List<BearItem> _bearItems;
        private BearItem _currentItem;
        public CarpetPosition CarpetPosition { get; private set; }

        private readonly List<IPlayerUpdatedListener> _playerListeners;

        public Player(World world, Vector2 initialPosition, CarpetPosition carpetPosition)
        {
            _world = world;
            _bearItems = new List<BearItem>();
            _playerListeners = new List<IPlayerUpdatedListener>();
            Position = initialPosition;
            CarpetPosition = carpetPosition;
        }

        public void ChangeItem()
        {
            bool itemGrabbed, itemDropped = false;
            BearItem newItem = _world.PickItem(this);

            itemGrabbed = newItem != null;

            if (_currentItem != null)
            {
                itemDropped = true;
                _world.DropItem(this, _currentItem);
            }

            _currentItem = newItem;

            if (itemDropped)
                FireOnItemDropped(this);
            
            if (itemGrabbed)
                FireOnItemGrabbed(this);
        }

        public void AddItem(BearItem item)
        {
            _bearItems.Add(item);
            FireOnItemAddedEvent(this);
        }

        public void MoveUp()
        {
            Position = new Vector2(Position.x, Position.y + 1);
            FireOnPlayerMovedUpEvent(this);
        }
        
        public void MoveDown()
        {
            Position = new Vector2(Position.x, Position.y - 1);
            FireOnPlayerMovedDownEvent(this);
        }
        
        public void MoveRight()
        {
            Position = new Vector2(Position.x + 1, Position.y);
            FireOnPlayerMovedRightEvent(this);
        }
        
        public void MoveLeft()
        {
            Position = new Vector2(Position.x - 1, Position.y);
            FireOnPlayerMovedLeftEvent(this);
        }

        private void FireOnItemGrabbed(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnItemGrabbed(player);
            }
        }

        private void FireOnItemDropped(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnItemDropped(player);
            }
        }
        
        private void FireOnPlayerMovedUpEvent(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnPlayerMovedUp(player);
            }
        }

        private void FireOnPlayerMovedDownEvent(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnPlayerMovedDown(player);
            }
        }

        private void FireOnPlayerMovedRightEvent(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnPlayerMovedRight(player);
            }
        }

        private void FireOnPlayerMovedLeftEvent(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnPlayerMovedLeft(player);
            }
        }

        private void FireOnItemAddedEvent(Player player)
        {
            foreach (IPlayerUpdatedListener listener in _playerListeners)
            {
                listener.OnItemAdded(player);
            }
        }
        
        public void AddListener(IPlayerUpdatedListener listener)
        {
            _playerListeners.Add(listener);
        }
    }

    public interface IPlayerUpdatedListener
    {
        void OnPlayerMovedUp(Player player);
        void OnPlayerMovedDown(Player player);
        void OnPlayerMovedRight(Player player);
        void OnPlayerMovedLeft(Player player);
        void OnItemGrabbed(Player player);
        void OnItemDropped(Player player);
        void OnItemAdded(Player player);
    }
}