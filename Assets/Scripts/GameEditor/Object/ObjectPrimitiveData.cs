using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class ObjectPrimitiveData{
    [SerializeField] private string objectName;
    [SerializeField] private string objectType;
    [SerializeField] private bool isPassable;
    public ObjectPrimitiveData(
        string objectName,
        string objectType,
        bool isPassable
    )
    {
        this.objectName = objectName;
        this.objectType = objectType;
        this.isPassable = isPassable;
    }
    public string GetObjectName(){return objectName;}
    public string GetObjectType(){return objectType;}
}