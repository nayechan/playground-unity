using System;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor;
using UnityEngine;
using static Tools.Misc;

namespace GameEditor.EventEditor.Controller
{
    public class SandboxPhase : MonoBehaviour
    {
        public GameObject editorInterface;
        private ToyData _toyData;
        // private BlockData _blockData;

        // UI가 모두 사라지고 게임이 시작된다. 호출시 샌드박스 저장없이 시작되므로 주의.
        public void GameStart()
        {
            HideEditorInterface();
            DisableEditorFunction();
            EnableChildrenRigidBody(Sandbox.RootOfToy); 
            BlockController.BlockActionWhenGameStart();
            SandboxUpdateController.SetSignalTransferAndBlockActionOn();
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
            DisableEditorFunction();
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
        public static void TestStop()
        {
            EnableEditorFunction();
            RecoverTemporalData();
            Pause();
        }

        private static void RecoverTemporalData()
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
            TouchController.GetTID().enabled = true;
        }
        
        private static void DisableEditorFunction()
        {
            TouchController.GetTID().enabled = false;
        }
    }
}