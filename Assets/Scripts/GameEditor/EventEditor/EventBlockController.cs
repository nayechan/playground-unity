using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    private HashSet<GameObject> _objs;
    private HashSet<GameObject> _blocks;
    private HashSet<GameObject> _signalLines;
    private GameObject _selectedBlock;
    public void UpdateNewObjs(List<GameObject> allObjs){
        foreach(GameObject obj in allObjs){
            if(_objs.Contains(obj))
                continue;
            // MakeNewBlock(obj);
            _objs.Add(obj);
        }
    }

    // private void MakeNewBlock(GameObject obj){
    //     // obj의 타입을 얻는다.
    //     string type = "objType";
    //     //
    // }

    public void SelectBlock(GameObject block){
        _selectedBlock = block;
    }
}
