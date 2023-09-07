#nullable enable

using Shared;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button playButton = default!;

        private async void Awake()
        {
            playButton.interactable = false;

            try
            {
                if (!FirebaseManager.IsLoggedIn)
                {
                    await FirebaseManager.AnonymousLogin(destroyCancellationToken);
                    await FirebaseManager.FetchPlayerHighscore(destroyCancellationToken);
                }
                playButton.interactable = true;
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void OnEnable()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}