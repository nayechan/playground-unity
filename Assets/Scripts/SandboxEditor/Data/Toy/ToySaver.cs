using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class ToySaver : MonoBehaviour
    {
        public ToyData ToyData { get; set; }

        void Awake()
        {
            ToyData = new ToyData();
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
            UpdateSupportedToyComponentsData();
        }

        private void UpdateSupportedToyComponentsData()
        {
            var newToyComponentsData = new ToyComponentsDataContainer();
            var toyComponents = GetComponents<Component>();
            foreach(var toyComponent in toyComponents)
            {
                if(ToyComponentData.IsSupportedType(toyComponent))
                {
                    newToyComponentsData.Add(GetUpdatedToyComponentData(toyComponent));
                }
            }
            ToyData.toyComponentsDataContainer = newToyComponentsData;
        }

        private ToyComponentData GetUpdatedToyComponentData(Component component)
        {
            return ToyComponentData.GetUpdatedToyComponentData(component);
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
            jObject["ToyData"] = JsonUtility.ToJson(ToyData);            
            return jObject;
        }

    }
    
}