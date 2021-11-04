using System;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Builder
{
    public class ObjectBuilder : MonoBehaviour
    {
        private ToyData currentToyData;
        private GameObject _newToy;
        public Transform rootObject;
        public bool isSnap;
        private static ObjectBuilder _ObjectBuilder;
        public static bool IsSnap => _ObjectBuilder.isSnap;

        private void Awake()
        {
            _ObjectBuilder ??= this;
        }

        public static void BuildAndPlaceToy(Vector3 cursorPosition)
        {
            _ObjectBuilder._BuildAndPlaceToy(cursorPosition);
        }

        private void _BuildAndPlaceToy(Vector3 cursorPosition)
        {
            _newToy = Sandbox.BuildSelectedToyOnToyRoot();
            if (_newToy is null) return;
            var newPosition = isSnap ? AdjustedPositionForSnapFunction(cursorPosition) : cursorPosition;
            newPosition.z = 0;
            _newToy.transform.position = newPosition;
        }

        private Vector3 AdjustedPositionForSnapFunction(Vector3 cursorPosition)
        {
            var toySize = _newToy.GetComponent<SpriteRenderer>().bounds.size;
            var newPosition = cursorPosition;
            var pivotAmount = toySize;
            pivotAmount.x *= 0.5f;
            pivotAmount.y *= -0.5f;
            pivotAmount.z = 10;
            newPosition-=pivotAmount;
            newPosition.x = Mathf.Round(newPosition.x);
            newPosition.y = Mathf.Round(newPosition.y);
            newPosition+=pivotAmount;
            return newPosition;
        }

        public static void SetCurrentToyData(ToyData ToyData)
        {
            _ObjectBuilder.currentToyData = ToyData;
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
