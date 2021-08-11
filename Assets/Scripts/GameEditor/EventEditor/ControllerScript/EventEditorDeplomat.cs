using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEditorDeplomat : MonoBehaviour
{
    static EventEditorDeplomat _eed;
    private HashSet<GameObject> _objects;
    public GameObject characterBlock, blockBlock, otherBlock;

    void Start(){
        _eed = this;
    }

    static public EventEditorDeplomat GetEED(){
        return _eed;
    }

    public void RefreshObjects(GameObject MovedObjects){
        Debug.Log(MovedObjects);
        foreach(Transform t in MovedObjects.transform){
            if(!_objects.Contains(t.gameObject)){
                ObjectInstanceController oic = t.GetComponent<ObjectInstanceController>();
                if(!oic) continue;
                CreateObjectBlock(t.gameObject);
            }
        }
    }


    private void CreateObjectBlock(GameObject targetObject) // 타입별로 블럭을 만듬. 타입은 oic 참조한다.
    {
        ObjectInstanceController oic = targetObject.GetComponent<ObjectInstanceController>();
        ObjectPrimitiveData pt = oic.GetObjectPrimitiveData();
        string objectType = pt.GetObjectType();
        if(objectType == "Character"){

        }
        if(objectType == "Block"){
    
        }
        if(objectType == "Other"){
    
        }
    
    }
}
