using System;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.InputControl.InPlay
{
    public class PlayerTouchController : MonoBehaviour
    {
        public static PlayerTouchController playerTouchController;
        private Touch[] touches;
        public static Touch[] Touches => playerTouchController.touches;

        private void Awake()
        {
            playerTouchController ??= this;
        }

        private void Update()
        {
            touches = Input.touches;
        }

        public static Vector2 TouchToViewport()
        {
            if (Touches.Length == 0) return Vector2.zero;
            Vector2 viewPort = Sandbox.Camera.ScreenToViewportPoint(Touches[0].position);
            viewPort -= new Vector2(0.5f, 0.5f);
            return viewPort;
        }
    }
}