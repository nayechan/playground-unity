using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    /*
이미지를 저장하기 위한 저장소입니다.
*/
    public class ImageStorage : MonoBehaviour
    {
        private static ImageStorage _imageStorage;
        public Sandbox.Sandbox sandbox;

        Dictionary<string, Sprite> _sprites;
        Dictionary<int, ImageData> _imagesData;
        public static int ImageDataCount => _imageStorage._imagesData.Count;

        private void Awake()
        {
            _imageStorage = this;
            _sprites = new Dictionary<string, Sprite>();
            _imagesData = new Dictionary<int, ImageData>();
        }

        private static ImageStorage GetSingleton()
        {
            return _imageStorage;
        }

        //이미지 데이터 추가
        public static void UpdateImagesDataAndSprites(ImageData imageData)
        {
            GetSingleton()._StoreImageDataAndItsSprites(imageData);
        }

        private void _StoreImageDataAndItsSprites(ImageData imageData)
        {
            _imagesData[imageData.GetHashCode()] = imageData;
            ContainSprites(imageData);
        }



        private void ContainSprites(ImageData imageData)
        {
            foreach(var fileName in imageData.GetRelativeImagePaths())
                _sprites[fileName] = MakeSprite(MakeFullPath(fileName));
        }

        private string MakeFullPath(string fileName)
        {
            return SandboxChecker.MakeFullPath(sandbox, fileName);
        }

        private Sprite MakeSprite(string imagePath)
        {
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            var imageBytes = File.ReadAllBytes(imagePath);
            texture.LoadImage(imageBytes);
            var sprite = Sprite.Create(
                texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f,0.5f)
            );
            return sprite;
        }

        public static List<ImageData> GetImagesData()
        {
            var imagesData = GetSingleton()._imagesData.Values.ToList();
            return imagesData;   
        }

        public static ImagesData GetImageStorageData()
        {
            var imagesData = new ImagesData(GetSingleton()._imagesData.Values.ToList());
            return imagesData;   
        }

        public static List<Sprite> GetSprites(ImageData imageData)
        {
            var sprites = new List<Sprite>();
            foreach(var fileName in imageData.GetRelativeImagePaths())
                sprites.Add(GetSingleton()._sprites[fileName]);
            return sprites;
        }

    }

    [Serializable]
    public class ImagesData
    {
        public List<ImageData> imagesData;

        public ImagesData(List<ImageData> imagesData)
        {
            this.imagesData = imagesData;
        }
    }
}