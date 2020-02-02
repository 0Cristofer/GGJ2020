using System;
using System.Collections.Generic;
using Controller;
using UnityEngine;

namespace GameController
{
    public class InputController : MonoBehaviour
    {
        private List<PlayerController> _players = null;
    
        [SerializeField] 
        private GameController _gameController = null;

        public void Init(List<PlayerController> players)
        {
            _players = players;
        }
    
        private void Update()
        {
            if (_players == null)
                return;

            foreach (PlayerController player in _players)
            {
                foreach (InputKey inputKey in EnumUtil.GetValues<InputKey>())
                {
                    if (Input.GetButtonDown(GetKeyCode(inputKey) + player.gameObject.name))
                    {
                        _gameController.EnqueueEvent(new GameEvent(player, inputKey));
                    }
                }

                float xAxis = Input.GetAxisRaw(GetAxisCode(InputAxis.Horizontal) + player.gameObject.name);
                float yAxis = Input.GetAxisRaw(GetAxisCode(InputAxis.Vertical) + player.gameObject.name) * -1;
                
                Vector2 joystickVector = new Vector2(
                    xAxis,
                    yAxis
                    );
                

                _gameController.EnqueueEvent(new GameEvent(player, joystickVector));
            }
        }

        private string GetKeyCode(InputKey key)
        {
            return EnumUtil.GetEnumValueName(key);
        }
        
        private string GetAxisCode(InputAxis key)
        {
            return EnumUtil.GetEnumValueName(key);
        }
    }

    public enum InputKey
    {
        ItemChange
    }

    public enum InputAxis
    {
        Horizontal, Vertical   
    }
}