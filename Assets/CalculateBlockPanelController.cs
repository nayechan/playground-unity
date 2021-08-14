using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateBlockPanelController : EventEditorComponentPanelController
{
    [SerializeField] Dropdown dropdown;
    public override string GetValue()
    {
        return dropdown.options[dropdown.value].text;
    }
}
