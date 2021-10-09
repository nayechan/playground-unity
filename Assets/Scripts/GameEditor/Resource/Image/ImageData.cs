using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//이미지를 데이터화 하기 위한 클래스입니다.
public class ImageData
{
    //private string uuid;
    [SerializeField] private List<string> _imagePaths;
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private bool _usingSingleImage, _isRelativeSize;
    [SerializeField] private float _hSize, _vSize;
    [SerializeField] private string  _title;

    private string uuid;

    public ImageData(bool imageMode, bool sizeMode, float hSize, float vSize, string title)
    {
        _usingSingleImage = imageMode;
        _isRelativeSize = sizeMode;
        _hSize = hSize;
        _vSize = vSize;
        _imagePaths = new List<string>();
        _title = title;

        uuid = SystemInfo.deviceUniqueIdentifier;
    }

    public List<string> GetImagePaths()
    {
        return _imagePaths;
    }

    public void AddImagePath(string path)
    {
        _imagePaths.Add(path);
    }

    public void SetImagePaths(List<string> imagePaths)
    {
        _imagePaths = imagePaths;
    }

    public void SetSprites(List<Sprite> sprites)
    {
        _sprites = sprites;
    }

    public List<Sprite> GetSprites()
    {
        return _sprites;
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

    public float GetHSize()
    {
        return _hSize;
    }
    
    public float GetVSize()
    {
        return _vSize;
    }

    public string GetUUID()
    {
        return uuid;
    }

}