using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEditorDeplomat : MonoBehaviour
{
    static EventEditorDeplomat _eed;
    private HashSet<GameObject> _objectsSet;
    public GameObject characterBlock, blockBlock, otherBlock;
    // private GameObject _objects, _tiles;

    void Start(){
        _eed = this;
        _objectsSet = new HashSet<GameObject>();
    }

    static public EventEditorDeplomat GetEED(){
        return _eed;
    }

    public void RefreshObjects(GameObject MovedObjects, GameObject MovedTiles){
        // _objects = MovedObjects; MovedTiles = _tiles;
        foreach(Transform t in MovedObjects.transform){
            if(!_objectsSet.Contains(t.gameObject)){
                CreateObjectBlock(t.gameObject);
            }
        }
    }


    private void CreateObjectBlock(GameObject targetObject) // 타입별로 블럭을 만듬. 타입은 oic 참조한다.
    {
        ObjectInstanceController oic = targetObject.GetComponent<ObjectInstanceController>();
        ObjectPrimitiveData pt = oic.GetObjectPrimitiveData();
        string objectType = pt.GetObjectType();
        EventBlockController ebc = EventBlockController.GetEBC();
        if(objectType == "Character"){
            ebc.GenerateBlockInstance(characterBlock, targetObject.transform.position, targetObject);
        }
        if(objectType == "Block"){
            ebc.GenerateBlockInstance(blockBlock, targetObject.transform.position, targetObject);
        }
        if(objectType == "Other"){
            ebc.GenerateBlockInstance(otherBlock, targetObject.transform.position, targetObject);
        }
    
    }
}
