using GameController;
using UnityEngine;

namespace Controller.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        public void OnSinglePlayerClick()
        {
            ApplicationController.Instance.SetNextGamePlayerQuantity(1);
            ApplicationController.Instance.LoadNextScene();
        }

        public void On2PlayerClick()
        {
            ApplicationController.Instance.SetNextGamePlayerQuantity(2);
            ApplicationController.Instance.LoadNextScene();
        }

        public void On4PlayerClick()
        {
            ApplicationController.Instance.SetNextGamePlayerQuantity(4);
            ApplicationController.Instance.LoadNextScene();
        }
    }
}
