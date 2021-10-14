using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor.Data;
using GameEditor.EventEditor.UI.Sensor;

public class ObjectBuilder : AbstractSensor
{
    public DataAgent currentDataAgent;
    public Transform rootObject;
    public GameObject objectSensorPrefab;
    public bool isSnap;

    // Start is called before the first frame update
    void Awake()
    {
        currentDataAgent = null;
    }

    public bool GenerateObject(Vector3 cursor)
    {
        Transform transform = null;
        if(isSnap)
        {
            transform = FindNearestObject(cursor, rootObject, 1.0f);
        }

        if(transform != null || currentDataAgent == null) return false;
        GameObject obj = DataManager.CreateGameobject(currentDataAgent);
        obj.transform.parent = rootObject;

        Debug.Log(objectSensorPrefab);

        GameObject objectSensor = GameObject.Instantiate(
            objectSensorPrefab, obj.transform.position, Quaternion.identity, 
            obj.transform
        );

        cursor.z = 10;

        Vector3 objSize = obj.GetComponent<SpriteRenderer>().bounds.size;


        if(isSnap)
        {
            cursor.x = Mathf.Floor(cursor.x);
            cursor.y = Mathf.Floor(cursor.y);
        }

            obj.transform.position = cursor;

        if(isSnap){
            Vector3 pivotAmount = objSize;
            pivotAmount.x *= 0.5f;
            pivotAmount.y *= 0.5f;
            pivotAmount.z = 0;

            obj.transform.position += pivotAmount;
        }
        return true;
    }

    public bool RemoveObject(Vector3 cursor)
    {
        List<Transform> objectsToRemove = new List<Transform>();
        foreach(Transform t in rootObject)
        {
            Vector3 pos = t.position;
            //Vector3 scale = t.GetComponent<ObjectInstanceController>().getDefaultSize();//
            Vector3 scale = t.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            
            Debug.Log(scale);
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
        Debug.Log(transform);
        foreach(Transform t in transform)
        {
            Vector3 tPos = t.position;
            //Vector3 scale = t.GetComponent<ObjectInstanceController>().getDefaultSize();//
            Vector3 scale = t.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            
            Debug.Log(scale);
            if(isSnap)
            {
                if(
                (tPos.x - (scale.x/2)) <= pos.x && pos.x < (tPos.x - (scale.x/2)) + 0.99f &&
                (tPos.y - (scale.y/2)) <= pos.y && pos.y < (tPos.y - (scale.y/2)) + 0.99f
                )
                {
                    float dist = 0.0f;
                    dist += Mathf.Abs((tPos.x - (scale.x/2)) + 0.5f - pos.x);
                    dist += Mathf.Abs((tPos.y - (scale.y/2)) + 0.5f - pos.y); 

                    if(dist < maxDist)
                    {
                        Debug.Log(dist);
                        result = t;
                        maxDist = dist;
                    }
                }
            }
            else
            {
                if(
                (tPos.x - (scale.x/2)) <= pos.x && pos.x < (tPos.x + (scale.x/2)) &&
                (tPos.y - (scale.y/2)) <= pos.y && pos.y < (tPos.y + (scale.y/2))
                )
                {
                    float dist = 0.0f;
                    dist += tPos.x - pos.x;
                    dist += tPos.y - pos.y; 

                    if(dist < maxDist)
                    {
                        Debug.Log(dist);
                        result = t;
                        maxDist = dist;
                    }
                }
            }
            
        }
        return result;
    }
    public void ToggleSnap()
    {
        isSnap = !isSnap;
    }
}
