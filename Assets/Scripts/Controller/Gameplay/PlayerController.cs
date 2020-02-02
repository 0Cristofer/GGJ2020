﻿using DG.Tweening;
using Domains;
using UnityEngine;

namespace Controller.Gameplay
{
    public class PlayerController : MonoBehaviour, IPlayerUpdatedListener
    {
        private static readonly int YSpeed = Animator.StringToHash("YSpeed");
        private static readonly int XSpeed = Animator.StringToHash("XSpeed");

        private Player _player;

        [SerializeField]
        private Animator _animation = null;

        private Vector2 _lastDirection;

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

            playerTransform.DOMove(newPos, 0.04f);

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
            
            if (deltaPos.x == 0 && deltaPos.y == 0)
                _player.SetIdle();

            _lastDirection = direction;
        }

        public void ChangeItem()
        {
            _player.ChangeItem();
            Debug.Log("Item Change " + gameObject.name +"!!!");
        }

        private void UpdateAnimation()
        {
            _animation.SetFloat(XSpeed, _lastDirection.x);
            _animation.SetFloat(YSpeed, _lastDirection.y);
        }

        public void OnPlayerMovedUp(Player player)
        {
            Debug.Log("UP");
            UpdateAnimation();
        }

        public void OnPlayerMovedDown(Player player)
        {
            Debug.Log("Down");
            UpdateAnimation();
        }

        public void OnPlayerMovedRight(Player player)
        {
            Debug.Log("Right");
            UpdateAnimation();
        }

        public void OnPlayerMovedLeft(Player player)
        {
            Debug.Log("Left");
            UpdateAnimation();
        }

        public void OnPlayerIdle(Player player)
        {
            Debug.Log("Idle");
            UpdateAnimation();
        }

        public void OnItemChanged(Player player)
        {
        }

        public void OnItemAdded(Player player)
        {
        }
    }
}