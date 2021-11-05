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
            {
                connection.SendSignal();
                Debug.Log($"destiantion : {connection.destination.Value}, source : {connection.source.Value}");
            }
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
            CreateAndSetConnectionSpriteLine(blockConnection);
            SetChildAndParent(blockConnection.spriteLine, Sandbox.RootOfConnectionSpriteLine); 
        }

        private static void CreateConnection(NewBlockPort source, NewBlockPort destination)
        {
            var blockConnection = new BlockConnection(source.portData, destination.portData);
            BlockConnections.Add(blockConnection);
            AddToPortAndConnectionPairs(source, blockConnection);
            AddToPortAndConnectionPairs(destination, blockConnection);
            CreateAndSetConnectionSpriteLine(blockConnection);
            SetChildAndParent(blockConnection.spriteLine, Sandbox.RootOfConnectionSpriteLine); 
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

        private static void CreateAndSetConnectionSpriteLine(BlockConnection blockConnection)
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

        public static BlockConnections GetBlockConnections()
        {
            return new BlockConnections(BlockConnections.ToList());
        }

        public static void CreateConnectionRootAndRenewConnections(BlockConnections blockConnectionsData, Dictionary<int, GameObject> toyIDPair, Dictionary<int, GameObject> blockIDPair)
        {
            RenewConnectionList();
            foreach (var blockConnectionData in blockConnectionsData.blockConnections)
                CreateConnection(blockConnectionData, toyIDPair, blockIDPair);
        }

        private static void CreateConnection(BlockConnection blockConnection, Dictionary<int, GameObject> toyIDPair,
            Dictionary<int, GameObject> blockIDPair)
        {
            var source = FindMatchingGameObject(blockConnection.source, toyIDPair, blockIDPair);
            var destination = FindMatchingGameObject(blockConnection.destination, toyIDPair, blockIDPair);
            CreateConnection(source, destination);
        }

        private static NewBlockPort FindMatchingGameObject(PortData portData, IReadOnlyDictionary<int, GameObject> toyIDPair,
            IReadOnlyDictionary<int, GameObject> blockIDPair)
        {
            var matchedGameObject = FindMatchingGameObject(portData.gameObjectInstanceID, toyIDPair, blockIDPair);
            return FindMatchingBlockPort(portData.portIndex, matchedGameObject);
        }

        private static GameObject FindMatchingGameObject(int instanceID, IReadOnlyDictionary<int, GameObject> toyIDPair,
            IReadOnlyDictionary<int, GameObject> blockIDPair)
        {
            GameObject matchedGameObject = null;
            if (toyIDPair.ContainsKey(instanceID))
                matchedGameObject = toyIDPair[instanceID];
            if (blockIDPair.ContainsKey(instanceID))
                matchedGameObject = blockIDPair[instanceID];
            return matchedGameObject;
        }
        
        private static NewBlockPort FindMatchingBlockPort(int portIndex, GameObject gameObjectWithBlockPort)
        {
            NewBlockPort matchedBlockPort = null;
            foreach (var port in gameObjectWithBlockPort.GetComponentsInChildren<NewBlockPort>())
                if (port.portData.portIndex == portIndex)
                    matchedBlockPort = port;
            return matchedBlockPort;
        }
        // 연결정보 세이브 로드 구현.
        private static void RenewConnectionList()
        {
            _ConnectionController._blockConnections = new HashSet<BlockConnection>();
        }

        public static HashSet<BlockConnection> GetConnections(NewBlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port)) return null;
            var connections = PortAndConnectionsPairs[port];
            foreach (var connection in connections.ToList().Where(connection => !_ConnectionController._blockConnections.Contains(connection)))
                connections.Remove(connection);
            return connections;
        }

    }
}