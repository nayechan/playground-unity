using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ImageData
{
    //private string uuid;
    [SerializeField]private List<string> _imagePaths;
    [SerializeField]private List<Sprite> _sprites;
    [SerializeField]private bool _imageMode, _sizeMode;
    [SerializeField]private float _hSize, _vSize;
    [SerializeField]private string  _title;

    public ImageData(bool imageMode, bool sizeMode, float hSize, float vSize, string title)
    {
        _imageMode = imageMode;
        _sizeMode = sizeMode;
        _hSize = hSize;
        _vSize = vSize;
        _imagePaths = new List<string>();
        _title = title;
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

    public bool GetSizeMode()
    {
        return _sizeMode;
    }

    public bool GetImageMode()
    {
        return _imageMode;
    }

    public float GetHSize()
    {
        return _hSize;
    }
    
    public float GetVSize()
    {
        return _vSize;
    }

}