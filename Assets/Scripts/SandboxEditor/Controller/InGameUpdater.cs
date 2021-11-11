using System;
using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class InGameUpdater : MonoBehaviour
    {
        private void FixedUpdate()
        {
            if (!SandboxPhaseChanger.IsGameStarted) return;
            CollisionInEveryFrame.RecordeToyCollision();
            ConnectionController.SendSignals();
            BlockController.BlockAction();
            CollisionInEveryFrame.RenewCollisions2D();
        }
    }
}