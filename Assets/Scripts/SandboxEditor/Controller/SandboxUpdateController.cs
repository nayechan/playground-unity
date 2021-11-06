using System;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class SandboxUpdateController : MonoBehaviour
    {
        private static SandboxUpdateController _sandboxUpdateController;
        private bool isGameStarted = false;
        public static bool IsGameStarted => _sandboxUpdateController.isGameStarted;


        private void Awake()
        {
            _sandboxUpdateController ??= this;
        }

        private void FixedUpdate()
        {
            if (!isGameStarted) return;
            CollisionInEveryFrame.RecordeToyCollision();
            BlockController.BlockAction();
            ConnectionController.SendSignals();
            CollisionInEveryFrame.RenewCollisions2D();
        }

        public static void SetSignalTransferAndBlockActionOn()
        {
            _sandboxUpdateController.isGameStarted = true;
        }

    }
}