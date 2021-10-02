using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageData
{
    //private string uuid;
    private List<string> _imagePaths;
    private List<Sprite> _sprites;
    private bool _imageMode, _sizeMode;
    private float _hSize, _vSize;
    private string  _title;

    public ImageData(bool imageMode, bool sizeMode, float hSize, float vSize)
    {
        _imageMode = imageMode;
        _sizeMode = sizeMode;
        _hSize = hSize;
        _vSize = vSize;
        _imagePaths = new List<string>();
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

    public string GetTitle()
    {
        return _title;
    }
    
}