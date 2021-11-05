using System.Collections.Generic;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor.BlockOptionButton
{
    public class BlockPropertyButton : AbstractSensor
    {
        public List<GameObject> _props;
        private bool isTurnOn = true;
        protected override void Start()
        {
        }
        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            foreach (var prop in _props)
            {
                if(prop == null) return;
                prop.SetActive(isTurnOn);
            }
            isTurnOn = !isTurnOn;
        }

    }
}
