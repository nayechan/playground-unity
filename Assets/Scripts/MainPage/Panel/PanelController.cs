using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage.Panel
{
    public abstract class PanelController : MonoBehaviour
    {
        public abstract void DeactivatePanel();
        public abstract void ActivatePanel();
        public abstract void UpdatePanel();
    }
}
