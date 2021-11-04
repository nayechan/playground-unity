using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        object a = gameObject;
        var b = (GameObject)a;
        Debug.Log(b.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

}

public class GO
{
    public List<HAHA> list = new List<HAHA>();
}

public class HAHA
{
    public int a;
}

