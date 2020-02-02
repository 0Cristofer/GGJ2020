using Domains;
using UnityEngine;

namespace Controller.Gameplay
{
    public class WorldController: MonoBehaviour
    {
        private World _world;

        public void Init(World world)
        {
            _world = world;
        }
    }
}