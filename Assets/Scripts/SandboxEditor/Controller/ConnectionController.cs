using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SandboxEditor.Block;
using SandboxEditor.Data;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;
using UnityEngine.UI;
using static Tools.Misc;
using Object = UnityEngine.Object;

namespace GameEditor.EventEditor.Controller
{
    public class ConnectionController : MonoBehaviour
    {
        private static ConnectionController _ConnectionController;
        private static HashSet<PortConnectionData> BlockConnections => _ConnectionController._blockConnections;
        private HashSet<PortConnectionData> _blockConnections;
        private static Dictionary<BlockPort, HashSet<PortConnectionData>> PortAndConnectionsPairs =>
            _ConnectionController.portLineAndConnectionsPairs;
        private Dictionary<BlockPort, HashSet<PortConnectionData>> portLineAndConnectionsPairs;
        private static BlockPort SelectedSourcePort
        {
            get => _ConnectionController.selectedSourcePort;
            set => _ConnectionController.selectedSourcePort = value;
        }
        private BlockPort selectedSourcePort;
        private static GameObject SpriteLine => _ConnectionController.spriteLine;
        public GameObject spriteLine;
        public GameObject guideMessage;
        


        private void Awake()
        {
            _ConnectionController = this;
            _blockConnections = new HashSet<PortConnectionData>();
            portLineAndConnectionsPairs = new Dictionary<BlockPort, HashSet<PortConnectionData>>();
        }

        public static void SendSignals()
        {
            foreach (var connection in BlockConnections)
                connection.SendSignal();
        }

        public static void WhenPortClicked(BlockPort clickedPort)
        {
            if (!IsWaitingAnotherPort())
            {
                if (IsNotBlock(clickedPort)) return;
                SetSelectedSource(clickedPort);
                ShowGuideMessage("선을 목적지 포트에 연결하세요.");
                return;
            }
            if(BothPortIsEqual(clickedPort, SelectedSourcePort) || !ConnectionChecker.correctCombinations.Contains((clickedPort.Type, SelectedSourcePort.Type)))
                InitializeConnectPhaseAndShowGuideMessage();
            var (senderPort, receiverPort) = (SelectedSourcePort, clickedPort);
            if (!ConnectionChecker.senderTypes.Contains(senderPort.Type))
                (senderPort, receiverPort) = (receiverPort, senderPort);
            CreateConnection(senderPort, receiverPort);
            _ConnectionController.OffGuideMessage(0f);
            SelectedSourcePort = null;
        }

        private static bool IsNotBlock(BlockPort port)
        {
            return port.portData.abstractBlock == null;
        }
        
        
        private static void SetSelectedSource(BlockPort port)
        {
            SelectedSourcePort = port;
        }

        private static bool IsWaitingAnotherPort()
        {
            return SelectedSourcePort != null;
        }

        private static bool BothPortIsEqual(Object first, Object second)
        {
            return first == second;
        }

        private static void InitializeConnectPhaseAndShowGuideMessage()
        {
            SelectedSourcePort = null;
            ShowGuideMessage("올바르지 않은 연결입니다. 다시 연결해 주세요.");
            _ConnectionController.OffGuideMessage(2f);
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

        private static void CreateConnection(BlockPort senderPort, BlockPort receiverPort)
        {
            var blockConnection = new PortConnectionData(senderPort, receiverPort);
            BlockConnections.Add(blockConnection);
            AddToPortAndConnectionPairs(senderPort, blockConnection);
            AddToPortAndConnectionPairs(receiverPort, blockConnection);
            CreateAndSetConnectionSpriteLine(blockConnection);
            SetChildAndParent(blockConnection.spriteLine, Sandbox.RootOfConnectionSpriteLine); 
        }

        private static void AddToPortAndConnectionPairs(BlockPort port, PortConnectionData portConnectionData)
        {
            CreateNewDictionaryIfDoesntExist(port);
            PortAndConnectionsPairs[port].Add(portConnectionData);
        }

        private static void CreateNewDictionaryIfDoesntExist(BlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port))
                PortAndConnectionsPairs[port] = new HashSet<PortConnectionData>();
        }

        private static void CreateAndSetConnectionSpriteLine(PortConnectionData portConnectionData)
        {
            portConnectionData.spriteLine = Instantiate(SpriteLine);
            portConnectionData.spriteLine.GetComponent<PortConnection>().SetConnection(portConnectionData);
        }

        public static void DeleteConnections(BlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port)) return;
            var blockConnections = PortAndConnectionsPairs[port];
            foreach (var blockConnection in blockConnections)
                DeleteConnection(blockConnection);
            PortAndConnectionsPairs.Remove(port);
        }

        private static void DeleteConnection(PortConnectionData portConnectionData)
        {
            Destroy(portConnectionData.spriteLine);
            BlockConnections.Remove(portConnectionData);
        }

        public static BlockConnections GetBlockConnections()
        {
            return new BlockConnections(BlockConnections.ToList());
        }

        public static void CreateConnectionAndAddConnectionReference(BlockConnections blockConnectionsData, Dictionary<int, GameObject> toyIDPair, Dictionary<int, GameObject> blockIDPair)
        {
            foreach (var blockConnectionData in blockConnectionsData.blockConnections)
                CreateConnection(blockConnectionData, toyIDPair, blockIDPair);
        }

        private static void CreateConnection(PortConnectionData portConnectionData, Dictionary<int, GameObject> toyIDPair,
            Dictionary<int, GameObject> blockIDPair)
        {
            var source = FindMatchingGameObject(portConnectionData.senderData, toyIDPair, blockIDPair);
            var destination = FindMatchingGameObject(portConnectionData.receiverData, toyIDPair, blockIDPair);
            CreateConnection(source, destination);
        }

        private static BlockPort FindMatchingGameObject(PortData portData, IReadOnlyDictionary<int, GameObject> toyIDPair,
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
        
        private static BlockPort FindMatchingBlockPort(int portIndex, GameObject gameObjectWithBlockPort)
        {
            BlockPort matchedBlockPort = null;
            foreach (var port in gameObjectWithBlockPort.GetComponentsInChildren<BlockPort>())
                if (port.portData.portIndex == portIndex)
                    matchedBlockPort = port;
            return matchedBlockPort;
        }
        // 연결정보 세이브 로드 구현.
        public static void RenewConnectionList()
        {
            _ConnectionController._blockConnections = new HashSet<PortConnectionData>();
        }

        public static HashSet<PortConnectionData> GetConnections(BlockPort port)
        {
            if (!PortAndConnectionsPairs.ContainsKey(port)) return null;
            var connections = PortAndConnectionsPairs[port];
            foreach (var connection in connections.ToList().Where(connection => !_ConnectionController._blockConnections.Contains(connection)))
                connections.Remove(connection);
            return connections;
        }

    }
}