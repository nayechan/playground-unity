using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainPage;

namespace MainPage.Panel
{
    public class BlackPanelController : PanelController
    {
        private void OnEnable() {
            OnActivateComponent();
        }

        private void OnDisable() {
            OnDeactivateComponent();
        }

        public override void OnActivateComponent()
        {
            
        }

        public override void OnDeactivateComponent()
        {
            
        }

        public override void UpdateComponent()
        {
            
        }
    }
}
