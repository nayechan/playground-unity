using System.Collections.Generic;
using UnityEngine;

namespace GameEditor.Common
{
    public class PanelSwitch : MonoBehaviour
    {
        public List<GameObject> toActivate;
        public List<GameObject> toDeactivate;

        public void Apply()
        {
            foreach(var gameObject in toDeactivate)
                gameObject.SetActive(false);
            foreach(var gameObject in toActivate)
                gameObject.SetActive(true);
        }
    }
}
