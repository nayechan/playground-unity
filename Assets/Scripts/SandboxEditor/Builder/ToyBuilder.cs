using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class ToyBuilder
    {
        private GameObject newToy;
        private readonly JObject toyJsonData;
        private readonly ToyData toyData;
        private ToySaver toySaver;

        public static GameObject BuildToyRoot(JObject toyJsonData)
        {
            var toyBuilder = new ToyBuilder(toyJsonData);
            return toyBuilder.BuildToys();
        }

        private ToyBuilder(JObject toyJsonData)
        {
            this.toyJsonData = toyJsonData;
            Debug.Log(toyJsonData.ToString());
            toyData = LoadToyData(toyJsonData);
        }

        private static ToyData LoadToyData(JToken toyJsonData)
        {
            return toyJsonData.ToObject<ToyData>();
        }

        private GameObject BuildToys()
        {
            BuildToy();
            BuildToyChildren();
            return newToy;
        }
        
        private GameObject BuildToy()
        {
            CreateToyAndAddToySaver();
            AttachComponents();
            ApplyImageData();
            return newToy;
        }

        private void CreateToyAndAddToySaver()
        {
            newToy = new GameObject();
            toySaver = newToy.AddComponent<ToySaver>();
        }

        private void ApplyImageData()
        {
            if (DontHaveImage()) return;
            toyData.imageData.BuildAndAttachSpriteRendererAndAdjustScale(newToy);
        }

        private bool DontHaveImage()
        {
            return toyData.imageData.GetRelativeImagePaths().Count == 0;
        }

        private void AttachComponents()
        {
            foreach (var toyComponentData in toyData.toyComponentsDataContainer.GetToyComponentsData())
            {
                Debug.Log("Attaching This " + toyComponentData.ToString() );
                toyComponentData.AddDataAppliedToyComponent(newToy);
            }
        }

        private void BuildToyChildren()
        {
            foreach (JObject toyChildrenJsonData in toyJsonData["Children"])
            {                
                var toyChildren = BuildToyRoot(toyChildrenJsonData);
                toyChildren.transform.parent = newToy.transform;
            }
        }

        public static GameObject BuildToy(ToyData toyData)
        {
            var toyBuilder = new ToyBuilder(toyData);
            toyBuilder.BuildToy();
            Tools.Misc.SetChildAndParent(toyBuilder.newToy, Sandbox.RootOfToy);
            return toyBuilder.newToy;
        }

        private ToyBuilder(ToyData toyData)
        {
            this.toyData = toyData;
        }

        private void SetSaverResource()
        {
            toySaver.ToyData = toyData;
        }
        

    }
}