using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private static HashSet<BlockConnection> BlockConnections => _ConnectionController._blockConnections;
        private HashSet<BlockConnection> _blockConnections;
        private static Dictionary<NewBlockPort, HashSet<BlockConnection>> PortAndConnectionsPairs =>
            _ConnectionController.portLineAndConnectionsPairs;
        private Dictionary<NewBlockPort, HashSet<BlockConnection>> portLineAndConnectionsPairs;
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
            _blockConnections = new HashSet<BlockConnection>();
            portLineAndConnectionsPairs = new Dictionary<NewBlockPort, HashSet<BlockConnection>>();
        }

        public static void SendSignal()
        {
            foreach (var connection in BlockConnections)
                connection.SendSignal();
            Debug.Log("Signal Sent");
        }

        public static void WhenPortClicked(NewBlockPort newPort)
        {
            if (!IsOnConnecting() && !Is.ConnectionStartType(newPort.PortType)) return;
            if (!IsOnConnecting())
            {
                SetSelectedSource(newPort);
                ShowGuideMessage("선을 목적지 포트에 연결하세요.");
            }
            else
            {
                if (newPort == SelectedSourcePort || !Is.CorrectPortPair(SelectedSourcePort.PortType, newPort.PortType))
                {
                    ShowGuideMessage("올바르지 않은 포트입니다. 다시 연결해 주세요.");
                    SelectedSourcePort = null;
                    _ConnectionController.OffGuideMessage(2f);
                    return;
                }
                CreateConnection(newPort);
                _ConnectionController.OffGuideMessage(0f);
                SelectedSourcePort = null;
            }
        }
        private static void SetSelectedSource(NewBlockPort port)
        {
            SelectedSourcePort = port;
        }

        public static bool IsOnConnecting()
        {
            return SelectedSourcePort != null;
        }

        private static void ShowGuideMessage(string message)
        {
            _ConnectionController.guideMessage.GetComponent<Text>().text = message;
            _ConnectionController.guideMessage.SetActive(true);
        }

        private void OffGuideMessage(float delay)
        {
            StartCoroutine(_OffGuideMessage(delay));
        }

        private static IEnumerator _OffGuideMessage(float delay)
        {
            yield return new WaitForSeconds(delay);
            _ConnectionController.guideMessage.SetActive(false);
        }


        public static void CreateConnection(NewBlockPort destinationPort)
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
                PortAndConnectionsPairs[port] = new HashSet<BlockConnection>();
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

        public static BlocksConnection GetBlockConnections()
        {
            return new BlocksConnection(BlockConnections.ToList());
        }
        // 연결정보 세이브 로드 구현.
    }
}