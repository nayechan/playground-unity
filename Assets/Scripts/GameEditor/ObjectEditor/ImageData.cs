using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageData
{
    //private string uuid;
    private List<string> imagePaths;
    private List<Sprite> spritePaths;
    private string _type, _title;

    public ImageData(string type)
    {
        imagePaths = new List<string>();
        spritePaths = new List<Sprite>();
        _type = type;
    }

    public List<string> GetImagePaths()
    {
        return imagePaths;
    }

    public void SetImagePathAtIndex(string path, int i)
    {
        imagePaths[i] = path;
    }

    public void SetSprites(List<Sprite> sprites)
    {
        spritePaths = sprites;
    }


    public string GetType()
    {
        return _type;
    }

    public string GetTitle()
    {
        return _title;
    }
}