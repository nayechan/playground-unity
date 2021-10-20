using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{   
    [SerializeField] List<Transform> panelToIgnore;
    public void DeactivateChildExceptIgnoreListAndActivateTarget(Transform panel)
    {
        DeactivateChildExceptIgnoreList();
        panel.gameObject.SetActive(true);
    }

    public void DeactivateChildExceptIgnoreList()
    {
        foreach(Transform _transform in transform)
        {
            if(panelToIgnore.Contains(_transform))
            {
                continue;
            }
            _transform.gameObject.SetActive(false);
        }
    }
}
