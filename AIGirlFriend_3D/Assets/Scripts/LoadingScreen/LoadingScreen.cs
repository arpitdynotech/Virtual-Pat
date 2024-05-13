using System;
using ExtraUtils;

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;
using Task = System.Threading.Tasks.Task;

namespace UI.LoadingScreen
{
    public class LoadingScreen : UISystem.Screen
    {
        
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI loadingText;
        
        private int _percentage;

        private void Start()
        {
            FillBar();
        }

        private async void  FillBar()
        {
            while (true)
            {
                fillImage.fillAmount += 0.005f;
                _percentage = (int)(fillImage.fillAmount * 100);
                loadingText.text = _percentage + "%";
                
                if (Math.Abs(fillImage.fillAmount - 1f) < 0.001f)
                {
                    OnLoadingFinished();
                    break;
                }
                await Task.Yield();
            }
           
        }

        private void OnLoadingFinished()
        {
           if (!PlayerPrefs.HasKey(PlayerPrefsNames.LoggedInPrefs) )
            {
                //loading screen changes
                UIManager.Instance.OnChangePanel(2);
            }
            else
            {
               UIManager.Instance.OnChangePanel(4);
                //   AuthenticationManager.Instance.Login(true);
                //  UIManager.Instance.OpenHomeScreen(); // For Testing Game Play.
            }

         //   if (TestManager.Instance.isTesting)
            {
              //  AuthenticationManager.Instance.Login(true);
            }
            
        }
    }
}
