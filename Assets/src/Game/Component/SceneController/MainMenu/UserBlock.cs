using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Component.UI;
using Network;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Component.SceneController.MainMenu
{
    public class UserBlock : MonoBehaviour
    {
        private readonly Queue<Action> _jobs = new Queue<Action>();
        
        public Button logoutButton;
        public Button connectRetryButton;
        public GameObject preloader;
        public ErrorMessageBox errorMessageBox;
        public Text username;
        public Text label;

        private void Start()
        {
            logoutButton.onClick.AddListener(OnLogoutClick);
            connectRetryButton.onClick.AddListener(async () => await TryConnect());
            
            Hide();
            if (!string.IsNullOrEmpty(GameController.Token))
            {
                _jobs.Enqueue(async () => await TryConnect());
            }
            else
            {
                _jobs.Enqueue(async () => await CheckConnection());
            }
        }

        private void Update()
        {
            while (_jobs.Count > 0) _jobs.Dequeue().Invoke();
        }

        private void FixedUpdate()
        {
            if (preloader.gameObject.activeSelf) preloader.transform.Rotate(new Vector3(0, 1, 0), 2.5f);
        }

        private void ShowLoader()
        {
            preloader.SetActive(true);
            
            errorMessageBox.Hide();
            logoutButton.gameObject.SetActive(false);
            connectRetryButton.gameObject.SetActive(false);
            label.gameObject.SetActive(false);
            username.gameObject.SetActive(false);
        }

        private void Hide()
        {
            ShowLoader();
            preloader.SetActive(false);
        }

        private void OnLogoutClick()
        {
            Hide();
            GameController.Logout();
        }

        private async Task CheckConnection()
        {
            ShowLoader();
            try
            {
                await GameController.Client.CheckConnection();
            }
            catch (Exception e)
            {
                ShowError("not connected");
                connectRetryButton.gameObject.SetActive(true);
            }
        }
        
        private async Task TryConnect()
        {
            ShowLoader();
            try
            {
                Authenticator.AuthStatus status = await Authenticator.Auth();
                if (status == Authenticator.AuthStatus.Success)
                    OnLoginSuccess();
                else
                {
                    Hide();
                    Debug.Log(status);
                }
            }
            catch (Exception e)
            {
                ShowError("Could not connect to server.\nPlease check your internet connection and try again");
                connectRetryButton.gameObject.SetActive(true);
            }
        }

        private void ShowError(string message)
        {
            Hide();
            Debug.Log(message);
            errorMessageBox.SetText(message);
            errorMessageBox.Show();
        }

        private void OnLoginSuccess()
        {
            label.gameObject.SetActive(true);
            username.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(true);
            username.text = GameController.Player.Username;
            preloader.SetActive(false);
        }
    }
}