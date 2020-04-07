using System;
using Network.DataTransfer;

namespace Service
{
    public static class PlayerService
    {
        private const string GetMethod = "player.get";
        private const string SetHomeSystemId = "player.home-id";

        public static void SetPlayerHome(int id, Action<Exception> error)
        {
            Request<object>.Empty(SetHomeSystemId).Then(response => {}).Catch(error.Invoke);
        }
    }
}