using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GameEditor.Data;
using System;

//이미지를 데이터화 하기 위한 클래스입니다.
[System.Serializable]
public class ImageData
{
    //private string uuid;
    [SerializeField] private List<string> _relativeImagePaths;
    [SerializeField] private bool _usingSingleImage, _isRelativeSize;
    [SerializeField] private float _hSize, _vSize;
    [SerializeField] private string  _title;
    [NonSerializedAttribute] public ToyData toyData;

    // private string uuid;

    public ImageData(bool imageMode, bool sizeMode, float hSize, float vSize, string title)
    {
        _usingSingleImage = imageMode;
        _isRelativeSize = sizeMode;
        _hSize = hSize;
        _vSize = vSize;
        _relativeImagePaths = new List<string>();
        _title = title;

        // uuid = SystemInfo.deviceUniqueIdentifier;
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

    public float GetHSize()
    {
        return _hSize;
    }
    
    public float GetVSize()
    {
        return _vSize;
    }

    public override int GetHashCode()
    {
        return (_relativeImagePaths.ToString()?? "").GetHashCode()
                ^ _usingSingleImage.GetHashCode()
                ^ _isRelativeSize.GetHashCode()
                ^ _hSize.GetHashCode()
                ^ _vSize.GetHashCode()
                ^ _title.GetHashCode();
    }

}