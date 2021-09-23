using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public class DataAgent : MonoBehaviour
    {
        public ObjectData od;
        private Dictionary<Component, ComponentData> ComponentDatas;
        private Dictionary<Component, ResourceData> ResourceDatas;
        
        private void Awake()
        {
            od = new ObjectData();
            ComponentDatas = new Dictionary<Component, ComponentData>();
            ResourceDatas = new Dictionary<Component, ResourceData>();
        }
        // DataAgent가 속한 GameObject의 ComponentData를 업데이트합니다.
        public void UpdateComponentData()
        {
            od.name = name;
            od.id = GetInstanceID();
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                // 이미 ComponentDatas 에 있다면, 값을 업데이트합니다.
                if (ComponentDatas.ContainsKey(component))
                {
                    ComponentDatas[component].SetComponent(component);
                }
                // GameObject의 컴포넌트를 ComponentData타입으로 저장하여 ComponentDatas 필드에 추가합니다.
                // 유효한 Data 타입이 없는경우 기록에서 생략합니다.
                else
                {
                    var cd = ComponentData.CreateComponentData(component, ResourceDatas);
                    if (cd != null)
                    {
                        ComponentDatas.Add(component, cd);
                    }
                }
            }
        }
        
        // 인자로 받은 컴포넌트의 ResourceData를 ResourceDatas 필드에 추가합니다.
        public void AddResourceData(Component component, ResourceData resourceData)
        {
            // 타입이 맞는지 확인합니다.
            switch (component)
            {
                // SpriteRenderer 는 이미지 경로를 저장하고 있는 ImageData 클래스와 함께 Called 하였는지 확인합니다.
                case SpriteRenderer sr:
                    if (resourceData is ImageData)
                    {
                        break;
                    }
                    goto default;
                default:
                    Debug.Log("Unsupported ComponentType.");
                    return;
            }
            ResourceDatas.Add(component, resourceData);
        }

        // 기록하고 있는 Datas를 JObject형식으로 반환합니다.
        public JObject GetJObject()
        {
            var jObj = new JObject
            {
                {"ObjectData", new JObject()},
            };
            jObj["ObjectData"] = JsonUtility.ToJson(od);
            var comps = new JObject();
            foreach (var pair in ComponentDatas)
            {
                comps.Add(new JProperty(pair.Value.Type, JsonUtility.ToJson(pair.Value)));
            }
            jObj.Add(new JProperty("Components", comps));
            return jObj;
        }

        // gameObject 를 포함한 모든 자식 gameObject의 정보를 JObject형태로 반환합니다.
        public JObject GetJObjectFromChildren()
        {
            var jObj = GetJObject();
            var jArray = new JArray();
            foreach (Transform tp in transform)
            { 
                var da = tp.GetComponent<DataAgent>();
                if (da != null)
                {
                    jArray.Add(da.GetJObjectFromChildren());
                }
            }
            jObj.Add(new JProperty("Children", jArray));
            return jObj;
        }

    }
}