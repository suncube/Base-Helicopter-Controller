using UnityEngine;
using UnityEngine.UI;


namespace NewHelicopter
{
    public class UIViewController : MonoBehaviour
    {
        public GameObject PlayerUI;
        public GyroscopeViewController GyroscopeView;

        [Header("FlightView")]
        public Text UpDragView;
        public Text HeigthUpView;
        public Text HeigthView;
        public Text EngineForceView;

        public static UIViewController runtime;

        void Awake()
        {
            runtime = this;
            //CanvasGroup.alpha = 0;
        }

        public void SetPlayer(Transform player)
        {
            PlayerUI.SetActive(true);
            GyroscopeView.SetPlayer(player);
        }

      
        public void DeletePlayer(Transform player)
        {
            GyroscopeView.DeletePlayer(player);
        }

        void Start()
        {
            ShowInfoPanel(false);
        }


        private void ShowInfoPanel(bool isShow)
        {
            EngineForceView.gameObject.SetActive(!isShow);
            /*  RestartButton.SetActive(!isShow);
                 InfoButton.SetActive(!isShow);
                    InfoPanel.SetActive(isShow);*/
        }

        public void ShowInfo()
        {
            ShowInfoPanel(true);
        }

        public void HideInfo()
        {
            ShowInfoPanel(false);
        }

        public void RestartGame()
        {
            // Application.LoadLevel("Main");
        }
    }
}