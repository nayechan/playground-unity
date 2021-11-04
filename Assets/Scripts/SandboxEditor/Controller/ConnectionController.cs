using System;
using System.Collections.Generic;
using GameEditor.EventEditor.Line;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;
using UnityEngine.UI;
using static Tools.Misc;

namespace GameEditor.EventEditor.Controller
{
    public class ConnectionController : MonoBehaviour
    {
        private static ConnectionController _ConnectionController;
        private static List<BlockConnection> BlockConnections => _ConnectionController._blockConnections;
        private List<BlockConnection> _blockConnections;
        private static Dictionary<NewBlockPort, List<BlockConnection>> PortAndConnectionsPairs =>
            _ConnectionController.portLineAndConnectionsPairs;
        private Dictionary<NewBlockPort, List<BlockConnection>> portLineAndConnectionsPairs;
        private static NewBlockPort SelectedSourcePort
        {
            get => _ConnectionController.selectedSourcePort;
            set => _ConnectionController.selectedSourcePort = value;
        }
        private NewBlockPort selectedSourcePort;
        private static GameObject SpriteLine => _ConnectionController.spriteLine;
        public GameObject spriteLine;
        public GameObject guideMessage;
        


        private void Awake()
        {
            _ConnectionController ??= this;
            _blockConnections = new List<BlockConnection>();
            portLineAndConnectionsPairs = new Dictionary<NewBlockPort, List<BlockConnection>>();
        }

        private void Update()
        {
            SendSignal();
        }

        private static void SendSignal()
        {
            foreach (var connection in BlockConnections)
            {
                connection.SendSignal();
                
            }
        }

        public static void WhenPortClicked(NewBlockPort newPort)
        {
            if (SelectedSourcePort == null)
            {
                SetSelectedSource(newPort);
                ShowGuideMessage("선을 목적지 포트에 연결하세요.");
            }
            else
            {
                if (!Is.CorrectPortPair(SelectedSourcePort.PortType, newPort.PortType)) return;
                if (newPort == SelectedSourcePort)
                {
                    ShowGuideMessage("올바르지 않은 포트입니다. 다시 연결해 주세요.");
                    return;
                }
                OffGuideMessage();
                CreateConnection(newPort);
                SelectedSourcePort = null;
            }
        }
        private static void SetSelectedSource(NewBlockPort port)
        {
            SelectedSourcePort = port;
        }

        private static void ShowGuideMessage(string message)
        {
            _ConnectionController.guideMessage.GetComponent<Text>().text = message;
            _ConnectionController.guideMessage.SetActive(true);
        }

        private static void OffGuideMessage()
        {
            _ConnectionController.guideMessage.SetActive(false);
        }


        private static void CreateConnection(NewBlockPort destinationPort)
        {
            var blockConnection = new BlockConnection(SelectedSourcePort.portData, destinationPort.portData);
            BlockConnections.Add(blockConnection);
            AddToPortAndConnectionPairs(SelectedSourcePort, blockConnection);
            AddToPortAndConnectionPairs(destinationPort, blockConnection);
            CreateAndSetBlockConnection(blockConnection);
            SetChildAndParent(blockConnection.spriteLine, Sandbox.RootOfLine); 
        }

        private static void AddToPortAndConnectionPairs(NewBlockPort port, BlockConnection blockConnection)
        {
            CreateNewDictionaryIfDoesntExist(port);
            PortAndConnectionsPairs[port].Add(blockConnection);
        }

        private static void CreateNewDictionaryIfDoesntExist(NewBlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port))
                PortAndConnectionsPairs[port] = new List<BlockConnection>();
        }

        private static void CreateAndSetBlockConnection(BlockConnection blockConnection)
        {
            blockConnection.spriteLine = Instantiate(SpriteLine);
            blockConnection.spriteLine.GetComponent<ConnectionLine>().SetConnection(blockConnection);
        }

        public static void DeleteConnections(NewBlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port)) return;
            var blockConnections = PortAndConnectionsPairs[port];
            foreach (var blockConnection in blockConnections)
                DeleteConnection(blockConnection);
            PortAndConnectionsPairs.Remove(port);
        }

        private static void DeleteConnection(BlockConnection blockConnection)
        {
            Destroy(blockConnection.spriteLine);
            BlockConnections.Remove(blockConnection);
        }
    }
}