using System.Collections;
using System.Collections.Generic;
using GameEditor.Data;
using UnityEngine;

public class ToyDataContainer : MonoBehaviour
{
    public ToyData toyData;
    public ImageData ImageData { get {return toyData.imageData;}}
    public ObjectData ObjectData { get {return toyData.objectData;}}

}
