using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainPage
{
    public abstract class UIComponent : MonoBehaviour
    {
        public abstract void OnDeactivateComponent();
        public abstract void OnActivateComponent();
        public abstract void UpdateComponent();
    }
}
