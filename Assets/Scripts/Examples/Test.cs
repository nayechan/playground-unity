using System.Collections;
using System.Collections.Generic;
using SandboxEditor.Data.Toy;
using Tools;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var toydata = new ToyData();
        toydata.imageData.GetRelativeImagePaths().Add("hfiilj");
        Debug.Log(JsonUtility.ToJson(toydata));
        toydata = toydata.Clone();
        Debug.Log(JsonUtility.ToJson(toydata));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
