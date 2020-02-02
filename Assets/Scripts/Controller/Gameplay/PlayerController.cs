using System;
using DG.Tweening;
using Domains;
using UnityEngine;

namespace Controller.Gameplay
{
    public class PlayerController : MonoBehaviour, IPlayerUpdatedListener
    {
        private static readonly int YSpeed = Animator.StringToHash("YSpeed");
        private static readonly int XSpeed = Animator.StringToHash("XSpeed");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private Player _player;

        [SerializeField]
        private Animator _animation = null;

        public void Init(Player player)
        {
            _player = player;
            _player.AddListener(this);
        }
        
        public void Move(Vector2 direction)
        {
            Transform playerTransform = gameObject.transform;

            direction *= Player.Velocity;
            
            Vector3 currentPosition = playerTransform.position;
            Vector3 newPos = currentPosition + new Vector3(direction.x, direction.y, 0);

            playerTransform.position = newPos;

            Vector2 newGridPos = WorldUtil.ToGridPos(currentPosition);
            Vector2 deltaPos = newGridPos - _player.Position;

            if (deltaPos.x > 0)
                _player.MoveRight();
            if (deltaPos.x < 0) 
                _player.MoveLeft();
            if (deltaPos.y > 0)
                _player.MoveUp();
            if (deltaPos.y < 0)
                _player.MoveDown();

            if (Math.Abs(direction.magnitude) < 0.01f)
            {
                UpdateAnimationSpeed(0);
            }
            else
                UpdateAnimationSpeed(1);
            
        }

        public void ChangeItem()
        {
            _player.ChangeItem();
        }

        public void OnPlayerMovedUp(Player player)
        {
            Debug.Log("UP");
            UpdateAnimation(0, 1);
        }

        public void OnPlayerMovedDown(Player player)
        {
            Debug.Log("Down");
            UpdateAnimation(0, -1);
        }

        public void OnPlayerMovedRight(Player player)
        {
            Debug.Log("Right");
            UpdateAnimation(1, 0);
        }

        public void OnPlayerMovedLeft(Player player)
        {
            Debug.Log("Left");
            UpdateAnimation(-1, 0);
        }

        public void OnItemChanged(Player player)
        {
            Debug.Log("On Item Changed");
        }

        public void OnItemAdded(Player player)
        {
            Debug.Log("On Item Added");
        }
        
        private void UpdateAnimation(float x, float y)
        {
            _animation.SetFloat(XSpeed, x);
            _animation.SetFloat(YSpeed, y);
        }

        private void UpdateAnimationSpeed(float speed)
        {
            _animation.SetFloat(Speed, speed);
        }
    }
}
