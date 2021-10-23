using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public class ToySaver : MonoBehaviour
    {
        private ToyData toyData;

        void Awake()
        {
            toyData = new ToyData();
        }

        // UpdateToysData
        public static void UpdateToysData(GameObject gameObject)
        {
            var toySaver = gameObject.GetComponent<ToySaver>();
            
            if (toySaver != null)
            {
                toySaver.UpdateToyData();
            }
            foreach (Transform tp in gameObject.transform)
            {
                UpdateToysData(tp.gameObject);
            }
        }
        
        private void UpdateToyData()
        {
            UpdateToyObjectData();
            UpdateSupportedToyComponentsData();
        }

        private void UpdateToyObjectData()
        {
            toyData.toyBuildData.name = name;
        }

        private void UpdateSupportedToyComponentsData()
        {
            var newToyComponentsData = new ToyComponentsData();
            var toyComponents = GetComponents<Component>();
            foreach(var toyComponent in toyComponents)
            {
                if(ToyComponentData.IsSupportedType(toyComponent))
                {
                    newToyComponentsData.Add(GetUpdatedToyComponentData(toyComponent));
                }
            }
            toyData.toyComponentsData = newToyComponentsData;
        }

        private ToyComponentData GetUpdatedToyComponentData(Component component)
        {
            return ToyComponentData.GetUpdatedToyComponentData(component);
        }

        public ToyData GetToyData()
        {
            return toyData;
        }

        // -- GetJsonData
        public static JObject GetJsonToysData(ToySaver toySaver)
        {
            return toySaver.GetJsonToysData();
        }

        public JObject GetJsonToysData()
        {
            var jObject = GetJsonToyData();
            var jChildsArray = new JArray();
            foreach (Transform childToy in transform)
            { 
                var toySaver = childToy.GetComponent<ToySaver>();
                if (toySaver != null)
                {
                    jChildsArray.Add(toySaver.GetJsonToysData());
                }
            }
            jObject.Add(new JProperty("Children", jChildsArray));
            return jObject;
        }

        public static JObject GetJsonToyData(ToySaver toySaver) 
        {
            return toySaver.GetJsonToyData();
        }

        public JObject GetJsonToyData()
        {
            var jObject = new JObject
            {
                {"ToyData", new JObject()},
            };
            jObject["ToyData"] = JsonUtility.ToJson(toyData);            
            return jObject;
        }

    }
    
}