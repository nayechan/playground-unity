using System;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.UI
{
    public class UISwitch : MonoBehaviour
    {
        private static UISwitch _UISwitch;
        public GameObject editorUIRoot;

        private void Awake()
        {
            _UISwitch = this;
        }

        public static void WhenGameStart()
        {
            _UISwitch.editorUIRoot.SetActive(false);
            Sandbox.RootOfConnectionSpriteLine.SetActive(false);
        }
        
    }
}