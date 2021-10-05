using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class ObjectPrimitiveData{
    private Sprite[] sprites;
    [SerializeField] private string[] spritePaths;
    [SerializeField] private string objectName;
    [SerializeField] private string objectType;
    [SerializeField] private float width, height;
    [SerializeField] private string guid;
    public ObjectPrimitiveData(
        Sprite[] sprites,
        string[] spritePaths,
        string objectName,
        string objectType,
        float width, float hegiht
    )
    {
        this.sprites = sprites;
        this.spritePaths = spritePaths;
        this.objectName = objectName;
        this.objectType = objectType;
        this.width = width;
        this.height = hegiht;

        guid = System.Guid.NewGuid().ToString();

        Debug.Log(objectName+" "+objectType+" "+width+" "+height);
    }
    public string GetObjectName(){return objectName;}
    public string GetObjectType(){return objectType;}
    public float GetWidth(){return width;}
    public float GetHeight(){return height;}
    public Sprite[] GetSprites(){return sprites;}
    public string[] GetSpritePaths(){return spritePaths;}
    public string GetGuid(){return guid;}
    public void ReloadImageFromPath()
    {
        List<Sprite> reloadedSprites = new List<Sprite>();
        foreach(string path in spritePaths)
        {
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            byte[] byteArray = File.ReadAllBytes(path);
            texture.LoadImage(byteArray);
            Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
            reloadedSprites.Add(s);
        }
        sprites = reloadedSprites.ToArray();
    }
}