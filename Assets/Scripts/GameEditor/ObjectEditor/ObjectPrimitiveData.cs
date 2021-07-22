using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPrimitiveData{
    private Sprite[] sprites;
    private string objectName;
    private string objectType;
    private float width, height;
    public ObjectPrimitiveData(
        Sprite[] sprites,
        string objectName,
        string objectType,
        float width, float hegiht
    )
    {
        this.sprites = sprites;
        this.objectName = objectName;
        this.objectType = objectType;
        this.width = width;
        this.height = hegiht;
        Debug.Log(objectName+" "+objectType+" "+width+" "+height);
    }
    public string GetObjectName(){return objectName;}
    public string GetObjectType(){return objectType;}
    public float GetWidth(){return width;}
    public float GetHeight(){return height;}
    public Sprite[] GetSprites(){return sprites;}
}