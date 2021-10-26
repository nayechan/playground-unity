using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage.Panel
{
    public interface PanelController
    {
        public void DeactivatePanel();
        public void ActivatePanel();
        public void UpdatePanel();
    }
}
