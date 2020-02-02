using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Domains
{
    public class World
    {
        private List<Player> _players;
        private readonly List<BearItem> _bearItems;
        private readonly List<List<Vector2>> _worldCorners;
        
        public World(List<BearItem> bearItems, List<List<Vector2>> worldCorners)
        {
            _bearItems = bearItems;
            _worldCorners = worldCorners;
        }

        public BearItem PickItem(Player player)
        {
            BearItem bearItem = _bearItems.FirstOrDefault(item => item.Position.Equals(player.Position));
            
            if (bearItem != null)
                _bearItems.Remove(bearItem);

            return bearItem;
        }
        
        public void DropItem(Player player, BearItem item)
        {
            if (IsOnOwnCarpet(player))
            {
                player.AddItem(item);
                return;
            }
            
            _bearItems.Add(item);
        }

        private bool IsOnOwnCarpet(Player player)
        {
            foreach (CarpetPosition position in EnumUtil.GetValues<CarpetPosition>())
            {
                foreach (Vector2 corner in _worldCorners[(int) position])
                {
                    if (corner.Equals(player.Position))
                        return true;
                }
            }

            return false;
        }

        public void SetPlayers(List<Player> players)
        {
            _players = players;
        }
    }

    public enum CarpetPosition
    {
        LeftDown, RightDown,
        LeftUp, RightUp
    }
}