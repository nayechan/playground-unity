using System;
using System.Collections.Generic;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class ConnectionController : MonoBehaviour
    {
        private static ConnectionController _ConnectionController;
        public List<BlockConnection> _BlockConnections;
        public GameObject SpriteLine;
        private PortData selectedSourcePort;

        private void Awake()
        {
            _ConnectionController ??= this;
            _BlockConnections = new List<BlockConnection>();
        }

        private void Update()
        {
            SendSignal();
        }

        private static void SendSignal()
        {
            foreach (var connection in _ConnectionController._BlockConnections)
            {
                connection.SendSignal();
            }
        }

        public static void SetSelectedSource(PortData port)
        {
            _ConnectionController.selectedSourcePort = port;
        }

        public static void TryCreateConnection()
        {

        }

        private static bool isCorrectConnection()
        {
            return true;
        }

        public static void DeleteConnection()
        {
            
        }
    }
}