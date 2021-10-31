using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage.Panel
{
    public abstract class MenuController : MonoBehaviour
    {
        [SerializeField] List<PanelController> panels;
        [SerializeField] PanelController currentPanel;
        private void Awake() {
            
        }

        public void SwitchMenu(PanelController selectedPanel)
        {
            foreach(PanelController panel in panels)
            {
                panel.OnDeactivateComponent();
            }
            selectedPanel.OnActivateComponent();
            currentPanel = selectedPanel;
        }
        public void UpdateCurrentPanel()
        {
            currentPanel.UpdateComponent();
        }
    }
}

