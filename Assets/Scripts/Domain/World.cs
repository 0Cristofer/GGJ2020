using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Domain
{
    public class World
    {
        private readonly WorldConfig _worldConfig;
        
        private readonly List<Player> _players;
        private readonly List<BearItem> _bearParts;
        
        private readonly List<IWorldListener> _worldListeners;
        
        public World(WorldConfig worldConfig, List<Player> players, List<BearItem> bearParts)
        {
            _worldConfig = worldConfig;
            _players = players;
            _bearParts = bearParts;
            _worldListeners = new List<IWorldListener>();
            
            SetWorldToPlayers();
        }

        #region INTERNAL_INPUT
        internal BearItem PickItem(Player player)
        {
            BearItem bearItem = _bearParts.FirstOrDefault((part) =>
                (Vector2.Distance(part.Position, player.Position) < _worldConfig.MaxPickDistance));

            if (bearItem == null) 
                return null;

            _bearParts.Remove(bearItem);
            FireOnItemGrabbed(bearItem, player);

            return bearItem;
        }

        internal void DropItem(BearItem item)
        {
            _bearParts.Add(item);
            FireOnItemDropped(item);
        }
        #endregion
        
        #region PRIVATE_METHODS
        private void SetWorldToPlayers()
        {
            foreach (Player player in _players)
                player.SetWorld(this);
        }
        #endregion
        
        #region LISTENERS_INVOCATION
        private void FireOnItemGrabbed(BearItem item, Player player)
        {
            foreach (IWorldListener listener in _worldListeners)
            {
                listener.OnItemGrabbed(item, player);
            }
        }

        private void FireOnItemDropped(BearItem item)
        {
            foreach (IWorldListener listener in _worldListeners)
            {
                listener.OnItemDropped(item);
            }
        }
        #endregion
        
        #region LISTENER_HANDLERS
        public void AddListener(IWorldListener listener)
        {
            listener.World = this;
            _worldListeners.Add(listener);
        }

        public bool RemoveListener(IWorldListener listener)
        {
            return _worldListeners.Remove(listener);
        }
        #endregion
    }

    public interface IWorldListener
    {
        World World { get; set; }
        
        void OnItemGrabbed(BearItem item, Player player);
        void OnItemDropped(BearItem item);
    }
}