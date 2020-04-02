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
            if (Game.App.IsLogged) return AuthStatus.Success;
            
            if (string.IsNullOrEmpty(Game.App.Token))
            {
                return AuthStatus.InvalidToken;
            }

            Request<AuthRequest> request = new Request<AuthRequest>(AuthMethod, new AuthRequest(Game.App.Token));
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
            LoginResponse user = JsonUtility.FromJson<LoginResponse>(response);
            if (!string.IsNullOrEmpty(user.error))
            {
                return GetErrorStatus(user.error);
            }

            Game.App.SetPlayer(Player.CreateFromDTO(user));

            return AuthStatus.Success;
        }

        private static AuthStatus GetErrorStatus(string error)
        {
            return error == ExceptionWrongCredentials ? AuthStatus.WrongCredentials : AuthStatus.Error;
        }
    }
}