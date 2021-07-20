using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    private List<KeyValuePair<KeyValuePair<float,float>, GameObject>> _objects;
    public GameObject selectedObject;
    public GameObject objects;
    // Start is called before the first frame update
    void Start()
    {
        _objects = new List<KeyValuePair<KeyValuePair<float,float>, GameObject>>();
    }

    public bool GenerateObject(Vector3 cursor)
    {
        KeyValuePair<float,float> pair = new KeyValuePair<float, float>(cursor.x, cursor.y);
        int objectIndex;
        GameObject gameObject = FindNearestObject(pair, out objectIndex, 0.1f);
        if(gameObject != null || selectedObject == null) return false;
        GameObject obj = Instantiate(selectedObject, cursor, Quaternion.identity, objects.transform);
        _objects.Add(
            new KeyValuePair<
            KeyValuePair<float,float>,GameObject
            >(pair, obj)
        );
        return true;
    }

    public bool RemoveObject(Vector3 cursor)
    {
        KeyValuePair<float,float> pair = new KeyValuePair<float, float>(cursor.x, cursor.y);
        int objectIndex;
        GameObject gameObject = FindNearestObject(pair, out objectIndex);
        if(gameObject == null) return false;
        _objects.RemoveAt(objectIndex);
        Destroy(gameObject);
        return true;
    }

    public void SelectObject(GameObject _object){
        selectedObject = _object;
    }

    public GameObject FindNearestObject
    (KeyValuePair<float,float> pos, out int index, float maxDist=0.4f)
    {
        GameObject gameObject = null;
        int currentIndex = -1;
        index = currentIndex;
        foreach(
            KeyValuePair
            <
            KeyValuePair<float,float>, 
            GameObject
            > 
            pair in _objects
        )
        {
            ++currentIndex;
            float distX = pair.Key.Key - pos.Key;
            float distY = pair.Key.Value - pos.Value;
            float distSquare = distX * distX + distY * distY;

            if(maxDist*maxDist <= distSquare) continue;
            else{
                gameObject = pair.Value;
                maxDist = Mathf.Sqrt(distSquare);
                index = currentIndex;
            }
        }
        return gameObject;
    }
}
