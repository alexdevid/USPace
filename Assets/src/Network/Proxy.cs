using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Network.DataTransfer;
using UnityEngine;

namespace Network
{
    public class Proxy
    {
        private bool _connected = false;
        private readonly byte[] _bytes = new byte[1024]; //8142
        private readonly Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public async Task<string> SetupServer()
        {
            return await Task.Run(Connect);
        }

        public bool IsConnected()
        {
            return _connected;
        }

        private string Connect()
        {
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 8080));
                _connected = true;
                
                return $"Socket connected to {_clientSocket.RemoteEndPoint}";
            }
            catch (SocketException ex)
            {
                Debug.LogError(ex.Message);
            }

            return "error";
        }

        public async Task<string> SendMessage(string message)
        {
            if (!_connected)
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

                    return Encoding.ASCII.GetString(_bytes, 0, bytesRec);
                }
                catch (ArgumentNullException ane)
                {
                    Debug.Log($"ArgumentNullException : {ane.ToString()}");
                    
                    return ane.ToString();
                }
                catch (SocketException se)
                {
                    Debug.Log($"SocketException : {se.ToString()}");
                    
                    return se.ToString();
                }
                catch (Exception e)
                {
                    Debug.Log($"Unexpected exception : {e.ToString()}");
                    
                    return e.ToString();
                }
            });
        }

        public void Disconnect()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
        }
    }
}