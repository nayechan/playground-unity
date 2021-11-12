using System;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class InGameUpdater : MonoBehaviour
    {
        private static InGameUpdater _InGameUpdater;
        private bool _isGameStarted = false;
        private void FixedUpdate()
        {
            if (!_isGameStarted) return;
            CollisionInEveryFrame.RecordeToyCollision();
            ConnectionController.SendSignals();
            BlockController.BlockAction();
            CollisionInEveryFrame.RenewCollisions2D();
        }

        public static void WhenGameStart()
        {
            _InGameUpdater._isGameStarted = true;
        }
        
        private void Awake()
        {
            _InGameUpdater = this;
        }
    }
}