using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deprecation
{
    public class EventEditorComponentSettings : MonoBehaviour
    {
        Dictionary<ComponentType, GameObject> ComponentObject;
        [SerializeField] GameObject _basicIncrementation, _compareBlock, _calculationBlock;
        private ComponentType currentComponentType;
    
        [Serializable]
        public enum ComponentType{
            None,
            BasicIncrementation,
            CompareBlock,
            CalculationBlock
        }
        private void Awake() {
            ComponentObject = new Dictionary<ComponentType, GameObject>();
            ComponentObject[ComponentType.None] = null;
            ComponentObject[ComponentType.BasicIncrementation] = _basicIncrementation;
            ComponentObject[ComponentType.CompareBlock] = _compareBlock;
            ComponentObject[ComponentType.CalculationBlock] = _calculationBlock;
        }    
    
        public void ActivateWindow(ComponentType componentType)
        {
            gameObject.SetActive(true);
            ComponentType prevComponentType = currentComponentType;
            currentComponentType = componentType;
            RefreshGUI(prevComponentType, currentComponentType);
        }

        public void CloseWindow()
        {
            ComponentType prevComponentType = currentComponentType;
            currentComponentType = ComponentType.None;
            RefreshGUI(prevComponentType, currentComponentType);
            gameObject.SetActive(false);
        }

        void RefreshGUI(
            ComponentType prevComponentType,
            ComponentType currentComponentType
        )
        {
            Debug.Log(currentComponentType);
            if(ComponentObject[prevComponentType] != null)
                ComponentObject[prevComponentType].SetActive(false);
            if(ComponentObject[currentComponentType] != null)
                ComponentObject[currentComponentType].SetActive(true);
        }
    }
}
