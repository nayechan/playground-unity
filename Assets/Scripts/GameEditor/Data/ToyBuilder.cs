using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class ToyBuilder
    {
        private GameObject toy;
        private JObject toyJsonData;
        private ToyData toyData;
        private ToySaver toySaver;

        public static GameObject UpdateImageStorageAndBuildToy(JObject toyJsonData)
        {
            var toyBuilder = new ToyBuilder(toyJsonData);
            toyBuilder.UpdateImageStorage();
            return toyBuilder.BuildToy();
        }

        private ToyBuilder(JObject toyJsonData)
        {
            this.toyJsonData = toyJsonData;
            toyData = LoadToyData(toyJsonData);
        }

        private void UpdateImageStorage()
        {
            var imageStorage = ImageStorage.GetSingleton();
            imageStorage.UpdateImagesDataAndSprites(toyData.imageData);
        }

        private ToyData LoadToyData(JObject toyJsonData)
        {
            return JsonUtility.FromJson<ToyData>(toyJsonData.ToString());
        }

        private GameObject BuildToy()
        {
            CreateToyAndAddToySaver();
            ApplyObjectData();
            ApplyImageData();
            AttachComponents();
            BulidToyChildren();
            return toy;
        }

        private void CreateToyAndAddToySaver()
        {
            toy = new GameObject();
            toySaver = toy.AddComponent<ToySaver>();
        }

        private void ApplyObjectData()
        {
            toy.name = toyData.objectData.name;
        }

        private void ApplyImageData()
        {
            toyData.imageData.BuildAndAttachSpriteRendererAndAdjustScale(toy);
        }

        private void AttachComponents()
        {
            foreach (var toyComponentData in toyData.toyComponentsData.Get())
            {
                toyComponentData.AddDataAppliedToyComponent(toy);
            }
        }

        private void BulidToyChildren()
        {
            foreach (JObject toyChildrenJsonData in toyJsonData["Children"])
            {                
                var toyChildren = UpdateImageStorageAndBuildToy(toyChildrenJsonData);
                toyChildren.transform.parent = toy.transform;
            }
        }

    }
}