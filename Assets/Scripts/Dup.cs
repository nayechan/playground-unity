using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dup : MonoBehaviour
{
    public Transform prefab;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=-10; i<10; ++i)
        for(int j=-10; j<10; ++j){
            Instantiate(prefab, new Vector3((float)i, (float)j, 0), Quaternion.identity);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

