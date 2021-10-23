using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SpriteRendererData : ToyComponentData
    {
        public ImageData imageData;
        public Color color;
        public bool visible;
        public int orderInLayer;

        public SpriteRendererData(ImageData imageData = null)
        {
            color = Color.white;
            visible = true;
            orderInLayer = 0;
            this.imageData = imageData;
        }

        // SpriteRendererData 클래스를 생성한다.
        public SpriteRendererData(Component comp)
        {
            UpdateByToyComponent(comp);
            //  SetImageData(imgData);
        }
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyDataToToyComponent(Component comp)
        {
            Assert.IsTrue(IsMatchedType(comp));
            var spriteRenderer = (SpriteRenderer)comp;
            SetWithFields(ref spriteRenderer);
            SetWithImageData(ref spriteRenderer);
        }

        private void SetWithFields(ref SpriteRenderer spriteRenderer)
        {
            spriteRenderer.color = color;
            spriteRenderer.sortingOrder = orderInLayer;
            spriteRenderer.enabled = visible ? true : false;
        }

        private void SetWithImageData(ref SpriteRenderer spriteRenderer)
        {
            // spriteRenderer.sprite = imageData.GetSprites()[0];
        }

        public static void resizeObjectScale(
            GameObject gameObject, ImageData imageData, bool isRelativeSize)
        {
            var texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            var transform = gameObject.transform;
            
            Vector3 newScale;
            if(!isRelativeSize)
            {
                newScale = new Vector3(
                    imageData.GetWidth()/texture.width * 100f,
                    imageData.GetHeight()/texture.height * 100f, 1f
                );
            }
            else
            {
                newScale = new Vector3(
                    imageData.GetWidth(),
                    imageData.GetHeight(), 1f
                );
            }
            
            transform.localScale = newScale;

        }
        // 인자로 받은 GameObject에 SpriteRenderer 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddMatchedTypeToyComponent(GameObject obj)
        {
            var sr = obj.AddComponent<SpriteRenderer>();
            return sr;
        }
        


        // 본 Class의 data를 받은 Component의 설정값으로 바꾼다.
        public sealed override ToyComponentData UpdateByToyComponent(Component comp)
        {
            var sr = (SpriteRenderer)comp;
            color = sr.color;
            orderInLayer = sr.sortingOrder;
            visible = sr.enabled ? true : false;
            return this;
        }

        // texturePath를 인자의 값으로 Set 한다.
        public void SetData(ImageData imgData)
        {
            this.imageData = imgData;
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsMatchedType(Component comp)
        {
            return comp is SpriteRenderer;
        }
        
    }
}