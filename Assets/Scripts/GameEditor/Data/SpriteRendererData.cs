using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SpriteRendererData : ComponentData
    {
        // Resources 폴더를 Root로 하는 경로 입력.
        // 예를들어 /Asset/Resources/Common/ABC.png 의 경우
        // "Common/ABC" 를 값으로 가진다.
        public string texturePath;
        public Color color;
        public bool visible;
        public int orderInLayer;
        
         public void SetComponent(GameObject obj)
         {
             var sr = obj.GetComponent<SpriteRenderer>();
             Assert.IsNotNull(sr);
             SetComponent(sr);
         }

         public Component AddComponent(GameObject obj)
         {
             var sr = obj.GetComponent<SpriteRenderer>();
             return sr;
         }
         
         public void SetComponent(SpriteRenderer sr)
         {
             sr.color = color;
             var tex = Resources.Load<Texture2D>(texturePath);
             if (tex == null)
             {
                 Debug.Log("Can't find texture : " + texturePath);
                 return;
             }
             sr.sprite = Sprite.Create(tex, new Rect(0f, 0f , tex.width, tex.height),
                 new Vector2(0.5f, 0.5f), tex.width);
             sr.sortingOrder = orderInLayer;
             sr.enabled = visible ? true : false;
         }
         
         public SpriteRendererData(GameObject obj, string tp)
         {
             SetData(obj);
             SetTexturePath(tp);
         }

         public void SetData(GameObject obj)
         {
             var sr = obj.GetComponent<SpriteRenderer>();
             SetData(sr);
         }

         public void SetData(SpriteRenderer sr)
         {
             color = sr.color;
             orderInLayer = sr.sortingOrder;
             visible = sr.enabled ? true : false;
         }

         public void SetTexturePath(string tp)
         {
             this.texturePath = tp;
         }
    }
}