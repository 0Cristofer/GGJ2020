using DG.Tweening;
using Domains;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour, IPlayerUpdatedListener
    {
        private Player _player;

        public void Init(Player player)
        {
            _player = player;
            _player.AddListener(this);
        }
        
        public void Move(Vector2 direction)
        {
            Transform playerTransform = gameObject.transform;

            direction *= _player.Velocity;
            
            Vector3 currentPosition = playerTransform.position;
            Vector3 newPos = currentPosition + new Vector3(direction.x, direction.y, 0);

            playerTransform.DOMove(newPos, 0.04f);

            Vector2 newGridPos = WorldUtil.ToGridPos(currentPosition);
            Vector2 deltaPos = newGridPos - _player.Position;

            if (deltaPos.x > 0)
            {
                _player.MoveRight();
            }

            if (deltaPos.x < 0)
            {
                _player.MoveLeft();
            }

            if (deltaPos.y > 0)
            {
                _player.MoveUp();
            }

            if (deltaPos.y < 0)
            {
                _player.MoveDown();
            }
        }

        public void ItemChange()
        {
            Debug.Log("Item Change " + gameObject.name +"!!!");
        }

        public void OnPlayerMovedUp(Player player)
        {
            Debug.Log("Player moved up!");
        }

        public void OnPlayerMovedDown(Player player)
        {
            Debug.Log("Player moved down!");
        }

        public void OnPlayerMovedRight(Player player)
        {
            Debug.Log("Player moved right!");
        }

        public void OnPlayerMovedLeft(Player player)
        {
            Debug.Log("Player moved left!");
        }

        public void OnItemChanged(Player player)
        {
            Debug.Log("Player item changed!");
        }

        public void OnItemAdded(Player player)
        {
            Debug.Log("Player item added!");
        }
    }
}
