using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor.Data;

public class ObjectBuilder : AbstractSensor
{
    public DataAgent currentDataAgent;
    public Transform rootObject;

    // Start is called before the first frame update
    void Awake()
    {
        currentDataAgent = null;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && currentDataAgent != null)
        {
            Vector3 pos = Input.mousePosition;
            pos = Camera.main.ScreenToWorldPoint(pos);
            GenerateObject(pos);
        }    
    }

    public bool GenerateObject(Vector3 cursor)
    {
        Transform transform = FindNearestObject(cursor, rootObject, 0.1f);
        if(transform != null || currentDataAgent == null) return false;
        GameObject obj = DataManager.CreateGameobject(currentDataAgent);
        obj.transform.parent = rootObject;
        cursor.z = 0;
        obj.transform.position = cursor;
        return true;
    }

    public bool RemoveObject(Vector3 cursor)
    {
        List<Transform> objectsToRemove = new List<Transform>();
        foreach(Transform t in rootObject)
        {
            Vector3 pos = t.position;
            Vector3 scale = t.GetComponent<ObjectInstanceController>().getDefaultSize();//
            if(
                (pos.x - (scale.x/2)) <= cursor.x && cursor.x <= (pos.x + (scale.x/2)) &&
                (pos.y - (scale.y/2)) <= cursor.y && cursor.y <= (pos.y + (scale.y/2))
            )
            {
                Debug.Log(pos+" "+scale+" "+cursor);
                objectsToRemove.Add(t);
            }
        }
        foreach(Transform t in objectsToRemove)
        {
            Destroy(t.gameObject);
        }
        
        return true;
    }

    public void SetCurrentDataAgent(DataAgent dataAgent)
    {
        currentDataAgent = dataAgent;
    }

    public Transform FindNearestObject(Vector3 pos, Transform transform, float maxDist=0.4f)
    {
        Transform result = null;
        float zIndex = pos.z;
        pos.z = 0;
        foreach(Transform t in transform)
        {
            if(t.position.z != zIndex) continue;
            Vector3 modifiedPosition = t.position;
            modifiedPosition.z = 0;

            float dist = Vector3.Distance(modifiedPosition, pos);

            if(maxDist <= dist) continue;
            else
            {
                result = t;
                maxDist = Mathf.Sqrt(dist);
            }
        }
        return result;
    }
}
