using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    public class ObjectDataAgent : MonoBehaviour
    {
        private ObjectData _od;
        private Dictionary<Component, ComponentData> _datas;
        private string _texturePath;

        private void Awake()
        {
            _od = new ObjectData(gameObject);
            _datas = new Dictionary<Component, ComponentData>();
        }

        public void SetTexturePath(string texturePath)
        {
            _texturePath = texturePath;
        }
        // DataAgent가 관리하는 오브젝트의 모든 컴포넌트 정보를 Data화 한다.
        // 게임 부팅시 JSON Data를 이용해 모든 컴포넌트를 추가한 뒤 실행한다.
        // + JSON 을 읽으며 이 오브젝트의 SpriteRenderer를 위한 texturePath도 Set한 뒤에 호출해야 함.
        public void CreateComponentsData()
        {
            var components = GetComponents<Component>();
            foreach (var component in components)
            {
                CreateComponentData(component);
            }
        }

        public void UpdateComponentData(Component component)
        {
            var cd = _datas[component];
            cd.SetData(gameObject);
        }

        // 오브젝트 컴포넌트 하나를 Data에 추가한다.
        public ComponentData CreateComponentData(Component component)
        {
            ComponentData cd;
            switch (component)
            {
                case BoxCollider2D box2d:
                {
                    cd = new BoxCollider2DData(gameObject);
                    break;
                }
                case CircleCollider2D cir2d:
                {
                    cd = new CircleCollider2DData(gameObject);
                    break;
                }
                case Rigidbody2D rb2d:
                {
                    cd = new Rigidbody2DData(gameObject);
                    break;
                }
                case SpriteRenderer sp:
                {
                    cd = new SpriteRendererData(gameObject, _texturePath);
                    break;
                }
                case Transform tf:
                {
                    cd = new TransformData(gameObject);
                    break;
                }
                default: 
                    Debug.Log("Can't find suitable ComponentData Type");
                    Assert.IsTrue(false);
                    return null;
            }
            _datas.Add(component, cd);
            return cd;
        }
    }
}