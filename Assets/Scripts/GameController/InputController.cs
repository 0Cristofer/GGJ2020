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
        private global::GameController.GameController _gameController = null;

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
                        Debug.Log("Got input + " + GetKeyCode(inputKey) + ", from " + player.gameObject.name);
                        _gameController.EnqueueEvent(new GameEvent(player, inputKey));
                    }
                }
            }
        }

        private string GetKeyCode(InputKey key)
        {
            return EnumUtil.GetEnumValueName(key);
        }
    }

    public enum InputKey
    {
        Fire, Jump
    }
}