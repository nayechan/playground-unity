using GameEditor.EventEditor.Controller;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.UI
{
    public class UISwitch : MonoBehaviour, PhaseChangeCallBackReceiver
    {
        private static UISwitch _UISwitch;
        public static UISwitch uISwitch => _UISwitch;
        public GameObject editorUIRoot;
        public GameObject inTestUIRoot;
        public GameObject inPlayUIRoot;

        private void Awake()
        {
            _UISwitch = this;
        }
        
        public void WhenGameStart()
        {
            editorUIRoot.SetActive(false);
            Sandbox.RootOfConnectionSpriteLine.SetActive(false);
            inPlayUIRoot.SetActive(true);
            inTestUIRoot.SetActive(false);
        }

        public void WhenTestStart()
        {
            editorUIRoot.SetActive(false);
            Sandbox.RootOfConnectionSpriteLine.SetActive(false);
            inPlayUIRoot.SetActive(false);
            inTestUIRoot.SetActive(true);
        }

        public void WhenTestPause() { }

        public void WhenTestResume() { }

        public void WhenBackToEditor()
        {
            editorUIRoot.SetActive(true);
            Sandbox.RootOfConnectionSpriteLine.SetActive(true);
            inPlayUIRoot.SetActive(false);
            inTestUIRoot.SetActive(false);
        }
    }
}