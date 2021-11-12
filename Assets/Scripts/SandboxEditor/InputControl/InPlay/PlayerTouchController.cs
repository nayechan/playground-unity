using System;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.InputControl.InPlay
{
    public class PlayerTouchController : MonoBehaviour
    {
        public static PlayerTouchController playerTouchController;
        private Touch[] touches;
        private static Touch[] Touches => playerTouchController.touches;

        private void Awake()
        {
            playerTouchController = this;
        }
        
        public static Vector2 TouchToViewport()
        {
            var touches = Input.touches;
            if (touches.Length == 0) return Vector2.zero;
            Vector2 viewPort = Sandbox.EditorCamera.ScreenToViewportPoint(touches[0].position);
            viewPort -= new Vector2(0.5f, 0.5f);
            return viewPort;
        }
    }
}