using GameManager;
using UnityEngine;

namespace Controller.MainMenu
{
    public class MainMenuSceneController : MonoBehaviour
    {
        public void OnSinglePlayerClick()
        {
            SfxManager.Instance.PlayClickButtonSfx();
            ApplicationController.Instance.SetNextGamePlayerQuantity(1);
            ApplicationController.Instance.LoadNextScene();
        }

        public void On2PlayerClick()
        {
            SfxManager.Instance.PlayClickButtonSfx();
            ApplicationController.Instance.SetNextGamePlayerQuantity(2);
            ApplicationController.Instance.LoadNextScene();
        }

        public void On4PlayerClick()
        {
            SfxManager.Instance.PlayClickButtonSfx();
            ApplicationController.Instance.SetNextGamePlayerQuantity(4);
            ApplicationController.Instance.LoadNextScene();
        }
    }
}
