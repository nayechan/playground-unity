using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SpriteRendererData : ComponentData
    {
        public ImageData imageData;
        public Color color;
        public bool visible;
        public int orderInLayer;
        public override string Type => _Type;
        public const string _Type = "SpriteRendererData";

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
            SetData(comp);
            //  SetImageData(imgData);
        }
        // 인자로 받은 Component의 설정을 본 class의 Data로 설정한다.
        public override void ApplyData(Component comp)
        {
            Assert.IsTrue(IsCorrectType(comp));
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
            // 나중에 Sandbox 객체의 MakeFullPath를 호출하도록 수정 필요!!!
            // var sandbox = 
            var imagePath = 
                Sandbox.MakeFullPath(imageData.GetImagePaths()[0]);
            var texture = new Texture2D(0, 0, TextureFormat.RGBA32, false); 
            try
            {
                texture.LoadImage(File.ReadAllBytes(imagePath));
            }
            catch
            {
                Debug.Log("Can't Load Image : " + imagePath);
                return;
            }
            spriteRenderer.sprite = Sprite.Create(
                texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f,0.5f)
            );
        }

        public static void resizeObjectScale(GameObject gameObject, ImageData imageData)
        {
            var texture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
            var transform = gameObject.transform;
            var newScale = 
                new Vector3(imageData.GetHSize()/texture.width * 100f,
                            imageData.GetVSize()/texture.height * 100f,
                            1f);
            transform.localScale = newScale;

        }
        // 인자로 받은 GameObject에 SpriteRenderer 컴포넌트를 추가하고
        //해당 컴포넌트를 반환한다.
        public override Component AddComponent(GameObject obj)
        {
            var sr = obj.AddComponent<SpriteRenderer>();
            return sr;
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
        public void SetData(ImageData imgData)
        {
            this.imageData = imgData;
        }
        
        // 인자로 받은 Component의 derived 타입이 본 클래스가 담당하는
        //Component타입과 일치하는지 확인한다.
        public override bool IsCorrectType(Component comp)
        {
            return comp is SpriteRenderer;
        }
        
    }
}