using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompareBlockPanelController : EventEditorComponentPanelController
{
    [SerializeField] Dropdown dropdown;
    public override string GetValue()
    {
        string data;
        data = dropdown.options[dropdown.value].text;

        switch(data)
        {
        case "Is lesser than":
            return "isLesserThan";
        case "Is equal or lesser than":
            return "isEqualOrLesserThan";
        case "Is greater than":
            return "isGreaterThan";
        case "Is equal or greater than":
            return "isEqualOrGreaterThan";
        case "Is equal":
            return "isEqual";
        case "Is not equal":
            return "isNotEqual";
        default:
            return "";
        }
    }
}
