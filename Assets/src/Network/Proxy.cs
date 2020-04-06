﻿using System;
 using System.Diagnostics;
 using System.Net;
 using System.Net.NetworkInformation;
 using System.Net.Sockets;
using System.Text;
 using System.Threading;
 using System.Threading.Tasks;
 using UnityEngine.Events;
 using Debug = UnityEngine.Debug;
 using Ping = System.Net.NetworkInformation.Ping;

 namespace Network
{
    public class Proxy
    {
        private readonly byte[] _bytes = new byte[1024]; //8142
        private readonly Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private bool _connected = false;
    
        public readonly UnityEvent OnConnected = new UnityEvent();

        public async Task<string> SetupServer()
        {
            return await Task.Run(Connect);
        }

        public bool IsConnected()
        {
            return _connected;
        }

        public async Task<string> CheckConnection()
        {
            return await SendMessage("a");
        }
        
        //TODO refactor!
        private string Connect()
        {
            try
            {
                if (IPAddress.TryParse("127.0.0.1:8080".Split(':')[0], out IPAddress ip))
                {
                    _clientSocket.Connect(new IPEndPoint(ip, 8080));
                    _connected = true;
                    OnConnected.Invoke();
                
                    return $"Socket connected to {_clientSocket.RemoteEndPoint}";
                }
            }
            catch (SocketException ex)
            {
                Debug.LogError(ex.Message);
                throw;
            }

            return "error";
        }

        public async Task<string> SendMessage(string message)
        {
            if (!IsConnected())
            {
                await SetupServer();
            }
            
            return await Task.Run(() =>
            {

                try
                {
                    byte[] msg = Encoding.ASCII.GetBytes(message);
                    int bytesSent = _clientSocket.Send(msg);
                    int bytesRec = _clientSocket.Receive(_bytes);
                    string response = Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                    
                    return response;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Unexpected exception : {e}");
                    Disconnect();
                    
                    return e.ToString();
                }
            });
        }
        
        public void Disconnect()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
            _connected = false;
        }
    }
}