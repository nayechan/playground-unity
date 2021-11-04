using System.Collections.Generic;
using SandboxEditor.UI.Panel;
using Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace SandboxEditor.InputControl.InEditor.Sensor.BlockOptionButton
{
    public class HideUnHideButton : AbstractSensor
    {
        public PanelSwitch panelSwitch;

        public override void OnTouchBegan(Touch touch, out bool isRayBlock)
        {
            isRayBlock = true;
            panelSwitch.Apply();
        }
    }
}