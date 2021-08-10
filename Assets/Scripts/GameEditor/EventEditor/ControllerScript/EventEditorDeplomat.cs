using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEditorDeplomat : MonoBehaviour
{
    static EventEditorDeplomat _eed;
    private string _storagePath = "/DontDestroyOnLoad/GameEditorBasicComponent/TemporaryDataManager";

    void Start(){
        _eed = this;
    }

    static public EventEditorDeplomat GetEED(){
        return _eed;
    }

    public void RefreshObjects(GameObject MovedObjects){
        Debug.Log(MovedObjects);
    }
}
