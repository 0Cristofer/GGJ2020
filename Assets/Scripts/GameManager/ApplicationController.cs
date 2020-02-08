using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
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

        public void LoadGameplay()
        {
            SceneManager.LoadScene("Arena01");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void OnQuitClick()
        {
            Application.Quit();
        }
    }
}