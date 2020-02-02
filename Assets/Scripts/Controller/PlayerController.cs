using DG.Tweening;
using Domains;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour, IPlayerUpdatedListener
    {
        private Player _player;
        [SerializeField]
        private Animator Animacao;

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


            Debug.Log("deltaPos.x : " + deltaPos.x);

            //Debug.Log("deltaPos.y : " + deltaPos.y);

            //if (deltaPos.x > 0)
            //{
            //    _player.MoveRight();
            //}

            //if (deltaPos.x < 0)
            //{
            //    _player.MoveLeft();
            //}

            //if (deltaPos.y > 0)
            //{
            //    _player.MoveUp();
            //}

            //if (deltaPos.y < 0)
            //{
            //    _player.MoveDown();

            if (deltaPos.y != 0 && deltaPos.x != 0)
            {
                Animacao.SetFloat("YSpeed", direction.y);
                Animacao.SetFloat("XSpeed", direction.x * -1);
                Animacao.SetFloat("velocity",    0.5f);
            }
            else
            {
                Animacao.SetFloat("velocity", 0);
            }
            

            //if (deltaPos.y == 0 && deltaPos.x == 0)
            //{

            //    Animacao.SetFloat("YSpeed", 0);
            //    Animacao.SetFloat("XSpeed", 0);
            //}
        }

        public void ItemChange()
        {
            Debug.Log("Item Change " + gameObject.name +"!!!");
        }

        public void OnPlayerMovedUp(Player player)
        {
            Animacao.SetFloat("YSpeed", 1);
            Animacao.SetFloat("XSpeed", 0);
            Debug.Log("Player moved up!");
        }

        public void OnPlayerMovedDown(Player player)
        {
            Animacao.SetFloat("YSpeed", -1);
            Animacao.SetFloat("XSpeed", 0);
            Debug.Log("Player moved down!");
        }

        public void OnPlayerMovedRight(Player player)
        {
            Animacao.SetFloat("XSpeed", -1);
            Animacao.SetFloat("YSpeed", 0);
            Debug.Log("Player moved right!");
        }

        public void OnPlayerMovedLeft(Player player)
        {
            Animacao.SetFloat("XSpeed", 1);
            Animacao.SetFloat("YSpeed", 0);
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
