using System.Collections.Generic;
using System.Linq;
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
            // 삭제된 Component를 확인하고 해당하는 Data를 삭제합니다.
            foreach (var pair in ComponentDatas.Where(pair => pair.Key == null))
            {
                ComponentDatas.Remove(pair.Key);
                ResourceDatas.Remove(pair.Key);
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
                    ResourceDatas.TryGetValue(component, out var rd);
                    var cd = ComponentData.CreateComponentData(component, rd);
                    if (cd != null)
                    {
                        ComponentDatas.Add(component, cd);
                    }
                }
            }
            // ResourceData를 업데이트합니다.
            foreach (var pair in ResourceDatas)
            {
                   
            }
        }

        // 인자로 받은 오브젝트와 그 하위 오브젝트의 Data를 모두 업데이트 합니다.
        public static void UpdateComponentFromDataAll(GameObject obj)
        {
            var da = obj.GetComponent<DataAgent>();
            if (da != null)
            {
                da.UpdateComponentData();
            }

            foreach (Transform tp in obj.transform)
            {
                UpdateComponentFromDataAll(tp.GameObject());
            }
        }
        
        // 인자로 받은 컴포넌트의 ResourceData를 ResourceDatas 필드에 추가합니다.
        // 게임 제작에서 resourceData가 필요한 컴포넌트를 추가하는 경우 꼭 호출해줍니다.
        public void AddResourceData(Component component, ResourceData resourceData)
        {
            // 타입이 맞는지 확인합니다.
            switch (component)
            {
                // SpriteRenderer 는 이미지 경로를 저장하고 있는 ImageData 클래스와 함께 Called 하였는지 확인합니다.
                case SpriteRenderer sr:
                    if (resourceData is SpriteData)
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

        // ResoruceData를 삭제합니다.
        public bool DelResourceData(Component component)
        {
            return ResourceDatas.Remove(component);
        }

        // 갖고 있는 Data 상태로 Component를 Set 합니다.
        public void SetComponentFromData()
        {
            foreach (var pair in ComponentDatas)
            {
                pair.Value.SetComponent(pair.Key);
            }
        }
        
        // 인자로 받은 오브젝트와 하위 오브젝트 모두 갖고있는 Data상태로 Component를
        // Set 합니다.
        public static void SetComponentFromDataAll(GameObject obj)
        {
            var da = obj.GetComponent<DataAgent>();
            if (da != null)
            {
                da.SetComponentFromData();
            }

            foreach (Transform tp in obj.transform)
            {
                SetComponentFromDataAll(tp.GameObject());
            }
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

        // ComponentData에 해당하는 Component를 오브젝트에 추가하고 해당 값으로 Set 합니다.
        public Component AddComponentFromData(ComponentData cd)
        {
            var comp = cd.AddComponent(gameObject);
            ComponentDatas.Add(comp, cd);
            cd.SetComponent(comp);
            return comp;
        }
        
        // gameObject 를 포함한 모든 자식 gameObject의 정보를 JObject형태로 반환합니다.
        public JObject GetJObjectFromAll()
        {
            var jObj = GetJObject();
            var jArray = new JArray();
            foreach (Transform tp in transform)
            { 
                var da = tp.GetComponent<DataAgent>();
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