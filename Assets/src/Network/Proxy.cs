using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Network.DataTransfer;
using UnityEngine;

namespace Network
{
    public class Proxy
    {
        private byte[] _bytes = new byte[1024]; //8142
        private Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public void SetupServer()
        {
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, 8080));
                Debug.Log($"Socket connected to {_clientSocket.RemoteEndPoint}");
            }
            catch (SocketException ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public string SendMessage(string message)
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
            }
            catch (SocketException se)
            {
                Debug.Log($"SocketException : {se.ToString()}");
            }
            catch (Exception e)
            {
                Debug.Log($"Unexpected exception : {e.ToString()}");
            }

            return "error";
        }

        public void Disconnect()
        {
            _clientSocket.Shutdown(SocketShutdown.Both);
            _clientSocket.Close();
        }
    }
}