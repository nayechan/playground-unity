using System.Collections;
using System.Collections.Generic;
using GameEditor.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Examples
{
    public class CreateGameObjectFromDataAgentExample : MonoBehaviour
    {
        public GameObject obj;
        private DataAgent dataAgent;
        void Start()
        {
            dataAgent = obj.AddComponent<DataAgent>();
            var imageData = new ImageData(true, false, 3f, 5f, "haha");
            // 기본적으로 C:\Users\lunda\AppData\LocalLow\Lighthouse Keeper\project-playground\-1\beach.png 를 로드합니다.
            imageData.AddImagePath("beach.png");
            Debug.Log("path : " + imageData.GetImagePaths()[0]);
            var objectData = new ObjectData();
            objectData.colliderType = ColliderType.Box;
            objectData.name = "new Object";
            objectData.isFixed = false;
            dataAgent.SetDataAgentResource(objectData, imageData);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var gameObject = DataManager.CreateGameobject(dataAgent);
                Debug.Log(gameObject.ToString() + " is Created ");
            }
        }
    }
}