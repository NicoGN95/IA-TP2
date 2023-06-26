using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace _Main._main.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            quitButton.onClick.AddListener(OnQuitButtonClicked);
            
        }

        private void OnPlayButtonClicked()
        {
            
            SceneManager.LoadScene(sceneToLoad);
            
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}