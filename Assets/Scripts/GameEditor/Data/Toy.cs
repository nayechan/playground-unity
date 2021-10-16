using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public class Toy : MonoBehaviour
    {
        private ToyData toyData;

        
        public void SetToyData(ToyData toyData)
        {
            this.toyData = toyData;
        } 

        private List<ToyComponentData> GetSupportedToyComponentsData()
        {
            return Toy.GetSupportedToyComponentsData(gameObject);
        }

        private static List<ToyComponentData> GetSupportedToyComponentsData(GameObject gameObject)
        {
            var toyComponentsData = new List<ToyComponentData>();
            foreach(var component in gameObject.GetComponents<Component>())
            {
                if(!ToyComponentData.IsSupportedType(component))
                    continue;
                toyComponentsData.Add(ToyComponentData.CreateComponentData(component));
            }
            return toyComponentsData;
        }

        public static void UpdateComponentFromDataAll(GameObject obj)
        {
            var da = obj.GetComponent<Toy>();
            if (da != null)
            {
                da.UpdateComponentData();
            }

            foreach (Transform tp in obj.transform)
            {
                UpdateComponentFromDataAll(tp.GameObject());
            }
        }
        
        // DataAgent가 속한 GameObject의 ComponentData를 업데이트합니다.
        private void UpdateComponentData()
        {
            objectData.name = name;
            // 삭제된 Component를 확인하고 해당하는 Data를 삭제합니다.
            foreach (var pair in ComponentDatas.Where(pair => pair.Key == null))
            {
                ComponentDatas.Remove(pair.Key);
                // ResourceDatas.Remove(pair.Key);
            }
            // 새 Data를 추가하거나 업데이트 합니다.
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                // 이미 ComponentDatas 에 있다면, 값을 업데이트합니다.
                if (ComponentDatas.ContainsKey(component))
                {
                    ComponentDatas[component].SetData(component);
                }
                // GameObject의 컴포넌트를 ComponentData타입으로 저장하여 ComponentDatas 필드에 추가합니다.
                // 유효한 Data 타입이 없는경우 기록에서 생략합니다.
                else
                {
                    // ResourceDatas.TryGetValue(component, out var rd);
                    var componentData = ToyComponentData.CreateComponentData(component);
                    if (componentData != null)
                    {
                        ComponentDatas.Add(component, componentData);
                    }
                }
            }
        }
        // 인자로 받은 오브젝트와 하위 오브젝트 모두 갖고있는 Data상태로 Component를
        // Set 합니다.
        private static void SetComponentFromDataAll(GameObject obj)
        {
            var da = obj.GetComponent<Toy>();
            if (da != null)
            {
                da.SetComponentFromData();
            }

            foreach (Transform tp in obj.transform)
            {
                SetComponentFromDataAll(tp.GameObject());
            }
        }
        
        private void SetComponentFromData()
        {
            foreach (var pair in ComponentDatas)
            {
                pair.Value.ApplyData(pair.Key);
            }
        }

        // 기록하고 있는 Datas를 JObject형식으로 반환합니다.
        public JObject GetJObject()
        {
            var jObj = new JObject
            {
                {"ObjectData", new JObject()},
            };
            jObj["ObjectData"] = JsonUtility.ToJson(objectData);            
            var comps = new JObject();
            foreach (var pair in ComponentDatas)
            {
                comps.Add(new JProperty(pair.Value.Type, JsonUtility.ToJson(pair.Value)));
            }
            jObj.Add(new JProperty("Components", comps));
            jObj.Add(new JProperty("ImageData", JsonUtility.ToJson(imageData)));
            jObj.Add(new JProperty("AudioData", JsonUtility.ToJson(audioData)));
            return jObj;
        }

        // ComponentData에 해당하는 Component를 오브젝트에 추가하고 해당 값으로 Set 합니다.
        public Component AddComponentFromData(ToyComponentData toyComponentData)
        {
            var component = toyComponentData.AddMatchedToyComponent(gameObject);
            ComponentDatas.Add(component, toyComponentData);
            toyComponentData.ApplyData(component);
            return component;
        }
        
        // gameObject 를 포함한 모든 자식 gameObject의 정보를 JObject형태로 반환합니다.
        public JObject GetJObjectFromAll()
        {
            var jObj = GetJObject();
            var jArray = new JArray();
            foreach (Transform tp in transform)
            { 
                var da = tp.GetComponent<Toy>();
                if (da != null)
                {
                    jArray.Add(da.GetJObjectFromAll());
                }
            }
            jObj.Add(new JProperty("Children", jArray));
            return jObj;
        }

    }
    
}