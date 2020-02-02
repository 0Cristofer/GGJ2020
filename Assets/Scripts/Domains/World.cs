using System.Collections.Generic;
using System.Linq;
using Controller.Gameplay;
using UnityEngine;

namespace Domains
{
    public class World
    {
        private List<Player> _players;
        private readonly List<BearItem> _bearItems;
        private readonly List<List<Vector2>> _worldCorners;
        
        private readonly List<IWorldListener> _worldListeners;
        
        public World(List<BearItem> bearItems, List<List<Vector2>> worldCorners)
        {
            _bearItems = bearItems;
            _worldCorners = worldCorners;
            _worldListeners = new List<IWorldListener>();
        }

        public BearItem PickItem(Player player)
        {
            BearItem bearItem = _bearItems.FirstOrDefault(item => item.Position.Equals(player.Position));

            if (bearItem != null)
            {
                _bearItems.Remove(bearItem);
                FireOnItemGrabbed(bearItem, player);                
            }

            return bearItem;
        }
        
        public void DropItem(Player player, BearItem item)
        {
            if (IsOnOwnCarpet(player))
            {
                player.AddItem(item);
                FireOnItemAddedToCarped(item, player);
                return;
            }

            item.Position = new Vector2(player.Position.x, player.Position.y);
            _bearItems.Add(item);
            FireOnItemDropped(item, player);
        }

        private bool IsOnOwnCarpet(Player player)
        {
            foreach (Vector2 tile in _worldCorners[(int) player.CarpetPosition])
            {
                if (tile.Equals(player.Position))
                    return true;
            }

            return false;
        }

        public void SetPlayers(List<Player> players)
        {
            _players = players;
        }

        public void AddListener(IWorldListener listener)
        {
            _worldListeners.Add(listener);
        }
        
        private void FireOnItemGrabbed(BearItem item, Player player)
        {
            foreach (IWorldListener listener in _worldListeners)
            {
                listener.OnItemGrabbed(item, player);
            }
        }

        private void FireOnItemDropped(BearItem item, Player player)
        {
            foreach (IWorldListener listener in _worldListeners)
            {
                listener.OnItemDropped(item, player);
            }
        }
        
        private void FireOnItemAddedToCarped(BearItem item, Player player)
        {
            foreach (IWorldListener listener in _worldListeners)
            {
                listener.OnItemAddedToCarpet(item, player);
            }
        }
    }

    public enum CarpetPosition
    {
        LeftDown, RightUp, 
        RightDown, LeftUp
    }

    public interface IWorldListener
    {
        void OnItemGrabbed(BearItem item, Player player);
        void OnItemDropped(BearItem item, Player player);
        void OnItemAddedToCarpet(BearItem item, Player player);
    }
}