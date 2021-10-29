using System.Collections.Generic;
using SandboxEditor.Data.Storage;
using UnityEngine;

//이미지를 데이터화 하기 위한 클래스입니다.
namespace SandboxEditor.Data.Resource
{
    [System.Serializable]
    public class ImageData
    {
        //private string uuid;
        [SerializeField] private List<string> _relativeImagePaths;
        [SerializeField] private bool _usingSingleImage, _isRelativeSize;
        [SerializeField] private float _toyWidth, _toyHeight;
        [SerializeField] private string  _title;

        public ImageData()
        {
            _relativeImagePaths = new List<string>();
        }
        public ImageData(bool imageMode, bool sizeMode, float toyWidth, float toyHeight, string title)
        {
            _usingSingleImage = imageMode;
            _isRelativeSize = sizeMode;
            _toyWidth = toyWidth;
            _toyHeight = toyHeight;
            _relativeImagePaths = new List<string>();
            _title = title;
        }

        public void BuildAndAttachSpriteRendererAndAdjustScale(GameObject toy)
        {
            var spriteRenderer = CreateSpriteRendererAndLoadSprite(toy);
            var texture = spriteRenderer.sprite.texture;
            var newScale = 
                new Vector3(GetWidth()/texture.width * 100f,
                    GetHeight()/texture.height * 100f,
                    1f);
            toy.transform.localScale = newScale;
        }

        private SpriteRenderer CreateSpriteRendererAndLoadSprite(GameObject toy)
        {
            var spriteRenderer = toy.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = ImageStorage.GetSprites(this)?[0];
            return spriteRenderer;
        }


        public List<string> GetRelativeImagePaths()
        {
            return _relativeImagePaths;
        }

        public void SetRelativeImagePaths(List<string> relativeImagePaths)
        {
            _relativeImagePaths = relativeImagePaths;
        }

        public string GetTitle()
        {
            return _title;
        }

        public bool GetIsRelativeSize()
        {
            return _isRelativeSize;
        }

        public bool GetIsUsingSingleImage()
        {
            return _usingSingleImage;
        }

        public float GetWidth()
        {
            return _toyWidth;
        }
    
        public float GetHeight()
        {
            return _toyHeight;
        }

        public override int GetHashCode()
        {
            return (_relativeImagePaths.ToString()?? "").GetHashCode()
                   ^ _usingSingleImage.GetHashCode()
                   ^ _isRelativeSize.GetHashCode()
                   ^ _toyWidth.GetHashCode()
                   ^ _toyHeight.GetHashCode()
                   ^ _title.GetHashCode();
        }
    }
}