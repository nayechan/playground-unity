using System;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Storage;
using Tools;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class InGameUpdater : MonoBehaviour, PhaseChangeCallBackReceiver
    {
        private static InGameUpdater _InGameUpdater;
        public static InGameUpdater inGameUpdater => _InGameUpdater;
        private bool _onPlay = false;

        private void Awake()
        {
            _InGameUpdater = this;
        }
        
        private void FixedUpdate()
        {
            if (!_onPlay) return;
            CollisionInEveryFrame.RecordeToyCollision();
            ConnectionController.SendSignals();
            BlockController.OnEveryFixedUpdateWhenPlaying();
            CollisionInEveryFrame.RenewCollisions2D();
        }

        public void WhenGameStart()
        {
            _InGameUpdater._onPlay = true;
            Misc.EnableChildrenRigidBody(Sandbox.RootOfToy);
        }

        public void WhenTestStart()
        {
            _InGameUpdater._onPlay = true;
            Misc.EnableChildrenRigidBody(Sandbox.RootOfToy);
        }

        public void WhenTestPause()
        {
            _InGameUpdater._onPlay = false;
            Misc.DisableChildrenRigidBody(Sandbox.RootOfToy);
        }

        public void WhenTestResume()
        {
            _InGameUpdater._onPlay = true;
            Misc.EnableChildrenRigidBody(Sandbox.RootOfToy);
        }

        public void WhenBackToEditor()
        {
            _InGameUpdater._onPlay = false;
            Misc.DisableChildrenRigidBody(Sandbox.RootOfToy);
        }

   }
}