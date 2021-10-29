using GameEditor.Data;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Builder
{
    public class ObjectBuilder : AbstractSensor
    {
        private ToyData currentToyData;
        public Transform rootObject;
        public GameObject objectSensorPrefab;
        public bool isSnap;

        // Start is called before the first frame update
        void Awake()
        {
            currentToyData = null;
        }

        public bool GenerateObject(Vector3 cursor)
        {
            Transform transform = null;
            if(isSnap)
            {
                transform = FindNearestObject(cursor, rootObject, 1.0f);
            }

            if(transform != null || currentToyData == null) return false;
            GameObject obj = ToyLoader.BuildToys(currentToyData);
            obj.transform.parent = rootObject;

            Debug.Log(objectSensorPrefab);

            GameObject objectSensor = GameObject.Instantiate(
                objectSensorPrefab, obj.transform.position, Quaternion.identity, 
                obj.transform
            );

            cursor.z = 10;

            Vector3 objSize = obj.GetComponent<SpriteRenderer>().bounds.size;
            Vector3 objPos = cursor;

            if(isSnap){

                Vector3 pivotAmount = objSize;
                pivotAmount.x *= 0.5f;
                pivotAmount.y *= -0.5f;
                pivotAmount.z = 10;


                objPos-=pivotAmount;
            
                objPos.x = Mathf.Round(objPos.x);
                objPos.y = Mathf.Round(objPos.y);

                objPos+=pivotAmount;

            }

            obj.transform.position = objPos;
            return true;
        }

        public void SetCurrentToyData(ToyData ToyData)
        {
            currentToyData = ToyData;
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
}
