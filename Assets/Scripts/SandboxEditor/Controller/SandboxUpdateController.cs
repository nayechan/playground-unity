using System;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class SandboxUpdateController : MonoBehaviour
    {
        private static SandboxUpdateController _sandboxUpdateController;
        private bool isGameStarted = false;

        private void Awake()
        {
            _sandboxUpdateController ??= this;
        }

        private void FixedUpdate()
        {
            if (!isGameStarted) return;
            ConnectionController.SendSignal();
            BlockController.BlockAction();
        }

        public static void SetSignalTransferAndBlockActionOn()
        {
            _sandboxUpdateController.isGameStarted = true;
        }

    }
}