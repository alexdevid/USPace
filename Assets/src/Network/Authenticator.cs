using System;
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
        
        public static void Auth(Action<AuthStatus> callback, Action<Exception> error)
        {
            Request<AuthRequest> request = new Request<AuthRequest>(AuthMethod, new AuthRequest(GameController.Token));
            request.Then(json =>
            {
                callback.Invoke(AuthorizeUser(json));
            }).Catch(error.Invoke);
        }

        public static void Login(LoginRequest data, Action<AuthStatus> callback, Action<Exception> error)
        {
            Request<LoginRequest> request = new Request<LoginRequest>(LoginMethod, data);
            request.Then(json =>
            {
                callback.Invoke(AuthorizeUser(json));
            }).Catch(error.Invoke);
        }

        private static AuthStatus AuthorizeUser(string response)
        {
            LoginResponse user = JsonUtility.FromJson<LoginResponse>(response);

            if (!string.IsNullOrEmpty(user.error))
            {
                return GetErrorStatus(user.error);
            }

            GameController.SetPlayer(Player.CreateFromDTO(user));

            return AuthStatus.Success;
        }

        private static AuthStatus GetErrorStatus(string error)
        {
            return error == ExceptionWrongCredentials ? AuthStatus.WrongCredentials : AuthStatus.Error;
        }
    }
}