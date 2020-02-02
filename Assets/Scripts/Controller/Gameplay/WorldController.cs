using System.Collections.Generic;
using Domains;
using UnityEngine;

namespace Controller.Gameplay
{
    public class WorldController: MonoBehaviour, IWorldListener
    {
        private World _world;

        private Dictionary<Player, PlayerController> _playerControllers;
        private Dictionary<BearItem, BearItemController> _bearItemControllers;

        public void Init(World world, List<PlayerController> playerControllers, List<BearItemController> bearItemControllers)
        {
            _world = world;
            _world.AddListener(this);

            _playerControllers = new Dictionary<Player, PlayerController>();
            _bearItemControllers = new Dictionary<BearItem, BearItemController>();

            foreach (PlayerController playerController in playerControllers)
            {
                _playerControllers.Add(playerController.GetDomain(), playerController);
            }
            
            foreach (BearItemController bearItemController in bearItemControllers)
            {
                _bearItemControllers.Add(bearItemController.GetDomain(), bearItemController);
            }
        }

        public void OnItemGrabbed(BearItem item, Player player)
        {
            BearItemController bearItemController = _bearItemControllers[item];
            PlayerController playerController = _playerControllers[player];

            bearItemController.transform.parent = playerController.transform;
        }

        public void OnItemDropped(BearItem item, Player player)
        {
            BearItemController bearItemController = _bearItemControllers[item];

            bearItemController.transform.parent = transform;
        }

        public void OnItemAddedToCarpet(BearItem item, Player player)
        {
            BearItemController bearItemController = _bearItemControllers[item];

            bearItemController.transform.parent = transform;
        }
    }
}