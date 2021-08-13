using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventEditorComponentSettings : MonoBehaviour
{
    Dictionary<ComponentType, GameObject> ComponentObject;
    [SerializeField] GameObject _basicIncrementation, _compareBlock, _calculationBlock;
    private ComponentType currentComponentType;

    private BlockProperty currentBlock;
    
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
    
    public void ActivateWindow(ComponentType componentType, BlockProperty block)
    {
        currentBlock = block;
        gameObject.SetActive(true);
        ComponentType prevComponentType = currentComponentType;
        currentComponentType = componentType;
        RefreshGUI(prevComponentType, currentComponentType);
    }

    public void ApplySetting()
    {
        currentBlock.GetMessage("");
        CloseWindow();
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
