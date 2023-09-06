#nullable enable

using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
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
                Debug.Log("Logging in");
                await FirebaseManager.AnonymousLogin();
                Debug.Log("Fetching highscore");
                await FirebaseManager.FetchPlayerHighscore();
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