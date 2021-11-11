using System;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor;
using SandboxEditor.InputControl.InPlay;
using UnityEngine;
using static Tools.Misc;

namespace GameEditor.EventEditor.Controller
{
    public class SandboxPhaseChanger : MonoBehaviour
    {
        public GameObject editorInterface;
        private static SandboxPhaseChanger _sandboxPhaseChanger;
        private ToyData _toyData;
        private bool isGameStarted = false;
        public static bool IsGameStarted
        {
            get => _sandboxPhaseChanger.isGameStarted;
            private set => _sandboxPhaseChanger.isGameStarted = value;
        }
        // private BlockData _blockData;

        private void Awake()
        {
            _sandboxPhaseChanger = this;
        }

        // UI가 모두 사라지고 게임이 시작된다. 호출시 샌드박스 저장없이 시작되므로 주의.
        public static void GameStart()
        {
            _sandboxPhaseChanger.HideEditorInterface();
            DisableEditorFunctionAndEnablePlayerFunction();
            EnableChildrenRigidBody(Sandbox.RootOfToy); 
            BlockController.BlockActionWhenGameStart();
            Sandbox.RootOfConnectionSpriteLine.SetActive(false);
            IsGameStarted = true;
        }


        private void HideEditorInterface()
        {
            editorInterface.SetActive(false);
        }

        // 에디터 UI를 유지한 상태로 Toy Block을 동작시켜 시뮬레이션을 돌릴 수 있다.
        // SandRewind 를 호출해 시뮬레이션 이전 상태로 돌아간다.
        public static void TestStart()
        {
            SaveTemporalSandboxData();
            DisableEditorFunctionAndEnablePlayerFunction();
            Resume();
        }

        private static void SaveTemporalSandboxData()
        {
            throw new NotImplementedException();
        }

        // Toy, Block 을 동작시킨다. 
        private static void Resume()
        {
        }

        // Run 페이즈에서 다시 Run 을 누르기 이전 상태로 되돌린다.
        // 토이의 물리효과와 블럭의 효과는 다시 멈춤상태가 된다.
        public static void BackToEditor()
        {
            EnableEditorFunction();
            ReloadInstance();
            Pause();
        }

        private static void ReloadInstance()
        {
            throw new NotImplementedException();
        }


        // Toy의 물리효과, Block의 효과를 멈춘다.
        public static void Pause()
        {
            DisableChildrenRigidBody(Sandbox.RootOfToy);
            DisableChildrenBlock(Sandbox.RootOfBlock);
        }

        private static void EnableEditorFunction()
        {
            PlayerTouchController.playerTouchController.enabled = false;
            TouchController.GetTID().enabled = true;
            Sandbox.EditorCamera.enabled = true;
            Sandbox.EditorCamera.GetComponent<AudioListener>().enabled = true;
        }
        
        private static void DisableEditorFunctionAndEnablePlayerFunction()
        {
            PlayerTouchController.playerTouchController.enabled = true;
            TouchController.GetTID().enabled = false;
            Sandbox.EditorCamera.enabled = false;
            Sandbox.EditorCamera.GetComponent<AudioListener>().enabled = false;
        }
    }
    
}