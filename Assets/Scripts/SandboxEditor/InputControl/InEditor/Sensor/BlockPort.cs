using GameEditor.EventEditor.Controller;
using SandboxEditor.Data;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class BlockPort : AbstractSensor
    {
        public PortData portData;
        public object value;
        public PortType Type => portData.portType;

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = false;
            if (TouchController.Mode == TouchMode.CreateObject || TouchController.Mode == TouchMode.CreateBlock || TouchController.Mode == TouchMode.MoveObject) return;
            ConnectionController.WhenPortClicked(this);
            isRayBlock = true;
        }

        private void OnDestroy()
        {
            InitializeDestinationValue();
            ConnectionController.DeleteConnections(this);
        }

        private void InitializeDestinationValue()
        {
            value = null;
            var connections = ConnectionController.GetConnections(this);
            if (connections == null) return;
            foreach (var connection in connections)
                connection.SendSignal();
        }

        public static void WhenToyHit(Collision2D collision2D)
        {
            Debug.Log($"Hit, {collision2D.gameObject} , other : {collision2D.otherRigidbody.gameObject}");
            CollisionInEveryFrame.AddCollision2D(collision2D);
        }
    }
}
