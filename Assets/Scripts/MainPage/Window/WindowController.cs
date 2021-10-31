using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainPage;
using MainPage.Panel;

namespace MainPage.Window
{
    public class WindowController : UIComponent
    {
        [SerializeField] BlackPanelController blackPanel;

        private void OnEnable() {
            OnActivateComponent();
        }

        private void OnDisable() {
            OnDeactivateComponent();
        }

        public override void OnActivateComponent()
        {
            blackPanel.gameObject.SetActive(true);
            blackPanel.OnActivateComponent();
        }

        public override void OnDeactivateComponent()
        {
            blackPanel.OnDeactivateComponent();
            blackPanel.gameObject.SetActive(false);
        }

        public override void UpdateComponent()
        {
            
        }

    }
}
