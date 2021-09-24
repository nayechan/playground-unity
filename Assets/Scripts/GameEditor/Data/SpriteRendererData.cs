using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SpriteRendererData : ComponentData
    {
        public ImageData ImageData;
        public Color color;
        public bool visible;
        public int orderInLayer;
        
        public override string Type => _Type;
        public const string _Type = "SpriteRendererData";
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
         public override void SetComponent(Component comp)
         {
             Assert.IsTrue(IsCorrectType(comp));
             var sr = (SpriteRenderer)comp;
             sr.color = color;
             var tex = Resources.Load<Texture2D>(ImageData.TexturePath);
             if (tex == null)
             {
                 Debug.Log("Can't find texture : " + ImageData.TexturePath);
                 return;
             }
             sr.sprite = Sprite.Create(tex, new Rect(0f, 0f , tex.width, tex.height),
                 new Vector2(0.5f, 0.5f), tex.width);
             sr.sortingOrder = orderInLayer;
             sr.enabled = visible ? true : false;
         }

        // 인자로 받은 GameObject에 SpriteRenderer 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
         public override Component AddComponent(GameObject obj)
         {
             var sr = obj.GetComponent<SpriteRenderer>();
             return sr;
         }
         
        // Component의 값을 갖는 SpriteRendererData 클래스를 생성한다.
         public SpriteRendererData(Component comp, ImageData imgData)
         {
             SetData(comp);
             SetImageData(imgData);
         }

        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
         public sealed override void SetData(Component comp)
         {
             var sr = (SpriteRenderer)comp;
             color = sr.color;
             orderInLayer = sr.sortingOrder;
             visible = sr.enabled ? true : false;
         }

         // texturePath를 인자의 값으로 Set 한다.
         public void SetImageData(ImageData imgData)
         {
             this.ImageData = imgData;
         }
         
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is SpriteRenderer;
        }
        
        public override void SetResourceData(ResourceData rd)
        {
            ImageData = (ImageData)rd;
        }
    }
}