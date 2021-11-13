using System;
using System.Collections;
using System.Collections.Generic;
using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.UI;
using Tools;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SandboxEditor.Controller
{
    public class SandboxPhaseChanger : MonoBehaviour, PhaseChangeCallBackReceiver
    {
        private static SandboxPhaseChanger _sandboxPhaseChanger;
        private ToyData _toyData;
        private BlocksData _blocksData;
        private BlockConnections _blockConnections;
        private List<PhaseChangeCallBackReceiver> phaseChangeCallBackReceivers;

        private void Awake()
        {
            _sandboxPhaseChanger = this;
        }

        public static void InitializePhaseReceiverList()
        {
            _sandboxPhaseChanger.phaseChangeCallBackReceivers = new List<PhaseChangeCallBackReceiver>
            {
                UISwitch.uISwitch,
                InGameUpdater.inGameUpdater,
                CollisionInEveryFrame._CollisionInEveryFrame,
                TouchInEditor._TouchInEditor
            };
        }

        public void WhenGameStart()
        {
            GameStartCallBack();
            BlockController.WhenBegin();
        }
        
        public void WhenTestStart()
        {
            TestStartCallBack();
            SaveTemporalSandboxData();
            ResumeTest();
            BlockController.WhenBegin();
        }

        private static void SaveTemporalSandboxData()
        {
            ToySaver.UpdateToysData(Sandbox.RootOfToy);
            _sandboxPhaseChanger._toyData = Sandbox.RootOfToy.GetComponent<ToySaver>().GetToyData();
            _sandboxPhaseChanger._blocksData = BlockStorage.GetLatestBlocksData(Sandbox.RootOfBlock);
            _sandboxPhaseChanger._blockConnections = ConnectionController.GetBlockConnections();
        }
        public void WhenTestPause()
        {
            TestPauseCallBack();
            Misc.DisableChildrenRigidBody(Sandbox.RootOfToy);
            Misc.DisableChildrenBlock(Sandbox.RootOfBlock);
        }

        public void WhenTestResume()
        {
            TestResumeCallBack();
        }

        public void WhenBackToEditor()
        {
            BackToEditorCallBack();
            ReloadInstance();
            PauseTest();
        }

        private static void ReloadInstance()
        {
            Sandbox.ResetObjectAndReference();
            Sandbox.LoadGameObjectAndAddIDReference(_sandboxPhaseChanger._toyData, _sandboxPhaseChanger._blocksData);
        }
        
        public static void StartGame()
        {
            _sandboxPhaseChanger.WhenGameStart();
        }
        
        public static void StartTest()
        {
            _sandboxPhaseChanger.WhenTestStart();
        }

        public static void PauseTest()
        {
            _sandboxPhaseChanger.WhenTestPause();
        }

        private static void ResumeTest()
        {
            _sandboxPhaseChanger.WhenTestResume();
        }
        
        public static void BackToEditor()
        {
            _sandboxPhaseChanger.WhenBackToEditor();
        }
        
        
        private static void GameStartCallBack()
        {
            foreach (var callBackReceiver in _sandboxPhaseChanger.phaseChangeCallBackReceivers)
            {
                Debug.Log(callBackReceiver.GetType());
                callBackReceiver.WhenGameStart();
            }
        }
        
        private static void TestStartCallBack()
        {
            foreach (var callBackReceiver in _sandboxPhaseChanger.phaseChangeCallBackReceivers)
            {
                Debug.Log(callBackReceiver.GetType());
                callBackReceiver.WhenTestStart();
            }
        }
        private static void TestResumeCallBack()
        {
            foreach (var callBackReceiver in _sandboxPhaseChanger.phaseChangeCallBackReceivers)
            {
                Debug.Log(callBackReceiver.GetType());
                callBackReceiver.WhenTestResume();
            }
        }
        private static void TestPauseCallBack()
        {
            foreach (var callBackReceiver in _sandboxPhaseChanger.phaseChangeCallBackReceivers)
            {
                Debug.Log(callBackReceiver.GetType());
                callBackReceiver.WhenTestPause();
            }
        }
        
        private static void BackToEditorCallBack()
        {
            foreach (var callBackReceiver in _sandboxPhaseChanger.phaseChangeCallBackReceivers)
            {
                Debug.Log(callBackReceiver.GetType());
                callBackReceiver.WhenBackToEditor();
            }
        }
        
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

    }
    
}