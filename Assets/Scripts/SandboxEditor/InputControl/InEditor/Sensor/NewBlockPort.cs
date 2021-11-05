using GameEditor.EventEditor.Controller;
using SandboxEditor.Data;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor
{
    public class NewBlockPort : AbstractSensor
    {
        public PortData portData;
        public int PortIndex => portData.portIndex;
        public object Value
        {
            get => portData.Value;
            set => portData.Value = value;
        }

        public PortType PortType
        {
            get => portData.portType;
            set => portData.portType = value;
        }

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
            Value = null;
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
