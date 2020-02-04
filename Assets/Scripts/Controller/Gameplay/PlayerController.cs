using System;
using Domain;
using GameManager;
using UnityEngine;
using Util;

namespace Controller.Gameplay
{
	public class PlayerController : MonoBehaviour, IPlayerUpdatedListener
	{
		private static readonly int YSpeed = Animator.StringToHash("YSpeed");
		private static readonly int XSpeed = Animator.StringToHash("XSpeed");
		private static readonly int Speed = Animator.StringToHash("Speed");

		public Player Player { get; set; }

		[SerializeField]
		private Animator _animation = null;
		[SerializeField]
		private OrdemDosLayers _ordemlayers = null;
		[SerializeField]
		private GameController _gameController = null;

		#region INIT
		public void Init(Player player)
		{
			player.AddListener(this);
		}
		#endregion

		#region INPUT
		public void ChangeItem()
		{
			Player.ChangeItem();
		}

		public void Move(Vector2 direction)
		{
			_ordemlayers.BeginLayerChange();

			direction *= Player.Velocity;

			Vector3 currentPosition = transform.position;
			Vector3 newPos = currentPosition + new Vector3(direction.x, direction.y, 0);

			UpdateAnimationDirectionAndSpeed(direction);
			Player.SetPosition(newPos.ToWorldVector2());
		}
		#endregion

		#region DOMAIN_LISTENER
		public void OnPositionChanged(System.Numerics.Vector2 previousPosition)
		{
			transform.position = WorldUtil.ToUnityPos(Player.Position.ToUnityVector2());
		}

		public void OnItemPicked()
		{
			SfxManager.Instance.PlayDropSfx();
		}

		public void OnItemDropped()
		{
			SfxManager.Instance.PlayDropSfx();
		}

		public void OnObjectiveUpdated()
		{
		}

		public void OnObjectiveReached()
		{
			_gameController.GameWon(Player);
		}
		#endregion

		#region ANIMATION
		private void UpdateAnimationDirectionAndSpeed(Vector3 direction)
		{
			AnimationDirection animDirection = AnimationDirection.None;
			if (direction.x > 0)
				animDirection = AnimationDirection.Right;
			if (direction.x < 0)
				animDirection = AnimationDirection.Left;
			if (direction.y > 0)
				animDirection = AnimationDirection.Up;
			if (direction.y < 0)
				animDirection = AnimationDirection.Down;

			UpdateAnimationSpeed(Math.Abs(direction.magnitude) < 0.01f ? 0 : 1);

			switch (animDirection)
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
		#endregion
	}

	internal enum AnimationDirection
	{
		Up,
		Down,
		Right,
		Left,
		None
	}
}