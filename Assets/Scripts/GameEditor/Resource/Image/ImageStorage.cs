using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using GameEditor;

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

    public static ImageStorage GetSingleton()
    {
       return _imageStorage;
    }

    //이미지 데이터 추가
    public void UpdateImagesDataAndSprites(ImageData imageData)
    {
        _imagesData[imageData.GetHashCode()] = imageData;
        UpdateSprites(imageData);
        
        _imageViewerController.RefreshUI(_imagesData, false);
        _imageSelectorController.RefreshUI(_imagesData);
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
        Debug.Log(sprite.pivot);
        return sprite;
    }

    public List<Sprite> GetSprites(ImageData imageData)
    {
        var sprites = new List<Sprite>();
        foreach(var fileName in imageData.GetRelativeImagePaths())
        {
            sprites.Add(_sprites[fileName]);
        }
        return sprites;
    }

}
