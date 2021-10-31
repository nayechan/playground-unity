using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchUIProperty<T> : MonoBehaviour 
where T :DesiredUIPropertyChangeAction
{
    [SerializeField] List<T> dataPairList;

    public void switchProperty()
    {
        foreach(T action in dataPairList)
        {
            action.Action();
        }
    }
}
