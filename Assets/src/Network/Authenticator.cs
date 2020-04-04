using System;
using System.Threading.Tasks;
using Model;
using Network.DataTransfer;
using Network.DataTransfer.Security;
using UnityEngine;

namespace Network
{
    public static class Authenticator
    {
        private const string LoginMethod = "security.login";
        private const string AuthMethod = "security.auth";

        private const string ExceptionWrongCredentials = "WrongCredentialsException";

        public enum AuthStatus
        {
            Success,
            InvalidToken,
            WrongCredentials,
            Error
        }

        public static async Task<AuthStatus> Auth()
        {
            if (string.IsNullOrEmpty(GameController.Token))
            {
                return AuthStatus.InvalidToken;
            }

            if (GameController.IsLogged)
            {
                return AuthStatus.Success;
            }

            Request<AuthRequest> request = new Request<AuthRequest>(AuthMethod, new AuthRequest(GameController.Token));
            string json = await request.Send();

            return AuthorizeUser(json);
        }

        public static async Task<AuthStatus> Login(string username, string password)
        {
            LoginRequest data = new LoginRequest(username, password);
            Request<LoginRequest> request = new Request<LoginRequest>(LoginMethod, data);
            string json = await request.Send();

            return AuthorizeUser(json);
        }

        private static AuthStatus AuthorizeUser(string response)
        {
            try
            {
                LoginResponse user = JsonUtility.FromJson<LoginResponse>(response);

                if (!string.IsNullOrEmpty(user.error))
                {
                    return GetErrorStatus(user.error);
                }

                GameController.SetPlayer(Player.CreateFromDTO(user));

                return AuthStatus.Success;
            }
            catch (ArgumentException e)
            {
                return AuthStatus.Error;
            }
        }

        private static AuthStatus GetErrorStatus(string error)
        {
            return error == ExceptionWrongCredentials ? AuthStatus.WrongCredentials : AuthStatus.Error;
        }
    }
}