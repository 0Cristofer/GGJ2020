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

        [SerializeField] private Animator _animation = null;
        [SerializeField] private GameController.GameController _gameController;

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
            
            MovePlayer(currentPosition);

            AnimationDirection animDirection = AnimationDirection.None;
            if (direction.x > 0)
                animDirection = AnimationDirection.Right;
            if (direction.x < 0)
                animDirection = AnimationDirection.Left;
            if (direction.y > 0)
                animDirection = AnimationDirection.Up;
            if (direction.y < 0)
                animDirection = AnimationDirection.Down;
            
            
            if (Math.Abs(direction.magnitude) < 0.01f)
                UpdateAnimationSpeed(0);
            else
                UpdateAnimationSpeed(1);
            
            UpdateAnimationDirection(animDirection);
        }

        public void ChangeItem()
        {
            Debug.Log("Change Item");
            _player.ChangeItem();
        }

        public Player GetDomain()
        {
            return _player;
        }
        
        public void OnPlayerMovedUp(Player player)
        {
            Debug.Log("UP, new pos: " + player.Position);
        }

        public void OnPlayerMovedDown(Player player)
        {
            Debug.Log("Down, new pos: " + player.Position);
        }

        public void OnPlayerMovedRight(Player player)
        {
            Debug.Log("Right, new pos: " + player.Position);
        }

        public void OnPlayerMovedLeft(Player player)
        {
            Debug.Log("Left, new pos: " + player.Position);
        }

        public void OnItemGrabbed(Player player)
        {
            Debug.Log("Item grabbed!");
        }

        public void OnItemDropped(Player player)
        {
            Debug.Log("Item Dropped!");
        }

        public void OnItemAdded(Player player)
        {
            Debug.Log("On Item Added");
        }

        public void OnObjectiveUpdated(Player player)
        {
            if (player.ObjectiveReached())
            {
                _gameController.GameWon(player);
            }
        }

        private void MovePlayer(Vector2 currentPosition)
        {
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
        }

        private void UpdateAnimationDirection(AnimationDirection direction)
        {
            switch (direction)
            {
                case AnimationDirection.Up:
                    UpdateAnimation(0, 1);
                    break;
                case AnimationDirection.Down:
                    UpdateAnimation(0, -1);
                    break;
                case AnimationDirection.Right:
                    UpdateAnimation(1, 0);
                    break;
                case AnimationDirection.Left:
                    UpdateAnimation(-1, 0);
                    break;
                case AnimationDirection.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private void UpdateAnimation(float x, float y)
        {
            _animation.SetFloat(XSpeed, x);
            _animation.SetFloat(YSpeed, y);
        }

        private void UpdateAnimationSpeed(int speed)
        {
            _animation.SetFloat(Speed, speed);
        }
    }

    internal enum AnimationDirection
    {
        Up, Down,
        Right, Left,
        None
    }
}