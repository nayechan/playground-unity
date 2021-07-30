using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPort : MonoBehaviour
{
    public string portType;
    public int portNum;
    public BlockProperty block;

    public string PortType{
        get { return portType;}
    }
    public int PortNum{
        get { return portNum;}
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
