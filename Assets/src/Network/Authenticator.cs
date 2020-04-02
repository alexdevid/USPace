using Model;
using Network.DataTransfer;
using Network.DataTransfer.Security;
using UnityEngine;
using UnityPackages;

namespace Network
{
    public class Authenticator
    {
        private const string LoginMethod = "security.login";
        private const string AuthMethod = "security.auth";
        
        
        // public static Promise<bool> Login(string username, string password)
        // {
        //     LoginRequest data = new LoginRequest(username, password);
        //     Request<LoginRequest> request = new Request<LoginRequest>(LoginMethod, data);
        //     
        //     return new Promise<bool>((resolve, reject) =>
        //     {
        //         request.Send().Then(response =>
        //         {
        //             AuthorizeUser(response);
        //             resolve(true);
        //         }).Catch(reject);
        //     });
        //     
        // }
        //
        // public static Promise<bool> Auth()
        // {
        //     return new Promise<bool>((resolve, reject) =>
        //     {
        //         if (string.IsNullOrEmpty(Game.App.Token))
        //         {
        //             resolve(false);
        //         }
        //         
        //         Request<AuthRequest> request = new Request<AuthRequest>(AuthMethod, new AuthRequest(Game.App.Token));
        //         request.Send().Then(response =>
        //         {
        //             AuthorizeUser(response);
        //             Debug.Log(response);
        //             resolve(true);
        //         }).Catch(reject);
        //     });
        // }

        private static void AuthorizeUser(string response)
        {
            LoginResponse user = JsonUtility.FromJson<LoginResponse>(response);
            Game.App.SetPlayer(Player.CreateFromDTO(user));
        }
    }
}