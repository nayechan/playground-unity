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
        private Toy toy;
        void Start()
        {
            toy = obj.AddComponent<Toy>();
            var imageData = new ImageData(true, false, 3f, 5f, "haha");
            // 기본적으로 C:\Users\lunda\AppData\LocalLow\Lighthouse Keeper\project-playground\-1\beach.png 를 로드합니다.
            // imageData.AddImagePath("beach.png");
            Debug.Log("path : " + imageData.GetRelativeImagePaths()[0]);
            var objectData = new ObjectData();
            objectData.colliderType = ColliderType.Box;
            objectData.name = "new Object";
            objectData.isFixed = false;
            // toy.SetToyData(objectData, imageData);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var gameObject = ToyBuilder.CreateGameobject(toy);
                Debug.Log(gameObject.ToString() + " is Created ");
            }
        }
    }
}