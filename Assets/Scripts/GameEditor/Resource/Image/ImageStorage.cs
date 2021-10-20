using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using GameEditor;
using Newtonsoft.Json.Linq;

/*
이미지를 저장하기 위한 저장소입니다.
*/
public class ImageStorage : MonoBehaviour
{
    //[SerializeField] private List<ImageData> _imageDatas;
    [SerializeField] private ImageViewerController _imageViewerController;
    [SerializeField] private ImageSelectorController _imageSelectorController;
    private static ImageStorage _imageStorage;
    // 정상 작동을 위해 inspector에서 sandbox를 지정해주세요.
    public Sandbox sandbox;

    Dictionary<string, Sprite> _sprites;
    Dictionary<int, ImageData> _imagesData;
    private void Awake()
    {
        SetSingletonIfUnset();
        _sprites = new Dictionary<string, Sprite>();
        _imagesData = new Dictionary<int, ImageData>();
    }

    private void SetSingletonIfUnset()
    {
        if(_imageStorage == null)
        {
            _imageStorage = this;
        }
    }

    private static ImageStorage GetSingleton()
    {
       return _imageStorage;
    }

    //이미지 데이터 추가
    public static void UpdateImagesDataAndSprites(ImageData imageData)
    {
        GetSingleton()._UpdateImagesDataAndSprites(imageData);
    }

    private void _UpdateImagesDataAndSprites(ImageData imageData)
    {
        _imagesData[imageData.GetHashCode()] = imageData;
        UpdateSprites(imageData);
        
        _imageViewerController.RefreshUI(false);
        _imageSelectorController.RefreshUI();
    }



    private void UpdateSprites(ImageData imageData)
    {
        foreach(var fileName in imageData.GetRelativeImagePaths())
        {
            var imagePath = SandboxChecker.MakeFullPath(sandbox, fileName);
            _sprites[fileName] = MakeSprite(imagePath);
        }
    }

    private Sprite MakeSprite(string imagePath)
    {
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        texture.LoadImage(imageBytes);
        Sprite sprite = Sprite.Create(
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
        {
            sprites.Add(GetSingleton()._sprites[fileName]);
        }
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
