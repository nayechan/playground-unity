using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBuilder : MonoBehaviour
{
    private List<GameObject> _objects, objectsToRemove;
    public GameObject currentObject;
    public GameObject objects;
    // Start is called before the first frame update
    void Start()
    {
        _objects = new List<GameObject>();
        objectsToRemove = new List<GameObject>();
    }

    public bool GenerateObject(Vector3 cursor)
    {
        int objectIndex;
        GameObject gameObject = FindNearestObject(cursor, out objectIndex, 0.1f);
        if(gameObject != null || currentObject == null) return false;
        GameObject obj = Instantiate(currentObject, cursor, Quaternion.identity, objects.transform);
        obj.GetComponent<ObjectInstanceController>().SetObjectPrimitiveData(
            currentObject.GetComponent<ObjectInstanceController>().GetObjectPrimitiveData()
        );
        Debug.Log(obj);
        _objects.Add(obj);
        obj.SetActive(true);
        obj.name = obj.GetComponent<ObjectInstanceController>().GetObjectPrimitiveData().GetObjectName();
        return true;
    }

    public bool RemoveObject(Vector3 cursor)
    {
        foreach(GameObject g in _objects)
        {
            Vector3 pos = g.transform.position;
            Vector3 scale = g.GetComponent<ObjectInstanceController>().getDefaultSize();
            if(
                (pos.x - (scale.x/2)) <= cursor.x && cursor.x <= (pos.x + (scale.x/2)) &&
                (pos.y - (scale.y/2)) <= cursor.y && cursor.y <= (pos.y + (scale.y/2))
            )
            {
                Debug.Log(pos+" "+scale+" "+cursor);
                objectsToRemove.Add(g);
            }
        }
        foreach(GameObject g in objectsToRemove)
        {
            _objects.Remove(g);
            Destroy(g);
        }
        
        objectsToRemove.Clear();
        return true;
    }

    public void SelectObject(GameObject _object){
        currentObject = _object;
    }

    public GameObject FindNearestObject(Vector3 pos, out int index, float maxDist=0.4f)
    {
        GameObject gameObject = null;
        int currentIndex = -1;
        index = currentIndex;
        foreach(GameObject g in _objects)
        {
            float dist = Vector3.Distance(g.transform.position, pos);

            if(maxDist <= dist) continue;
            else{
                gameObject = g;
                maxDist = Mathf.Sqrt(dist);
                index = currentIndex;
            }
        }
        return gameObject;
    }
}
