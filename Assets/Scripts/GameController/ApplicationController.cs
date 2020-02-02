﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class ApplicationController: MonoBehaviour
    {
        public static ApplicationController Instance;
        private int _newGamePlayerQuantity;
        
        public void Awake()
        {
            if (Instance != null)
                return;

            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void SetNextGamePlayerQuantity(int quantity)
        {
            _newGamePlayerQuantity = quantity;
        }

        public int GetNextGamePlayerQuantity()
        {
            return _newGamePlayerQuantity;
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}