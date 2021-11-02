using System;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class SandboxPhase :MonoBehaviour
    {
        public GameObject editorInterface;
        public static SandboxPhase _SandboxPhase;

        private void Awake()
        {
            _SandboxPhase ??= this;
        }

        // UI가 모두 사라지고 게임이 시작된다. 호출시 샌드박스 저장없이 시작되므로 주의.
        public void SandboxStart()
        {
            HideEditorInterface();
            SandboxRun();
        }

        private void HideEditorInterface()
        {
            editorInterface.SetActive(false);
        }
        
        public static void SandboxRun()
        {
            Time.timeScale = 1f;
        }
        
        public static void SandboxPause()
        {
            Time.timeScale = 0;
        }

    }
}