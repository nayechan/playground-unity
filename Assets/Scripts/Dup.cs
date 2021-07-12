using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dup : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform positionToInsantiate;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=-10; i<10; ++i)
        for(int j=-10; j<10; ++j){
            Instantiate(
                prefab, 
                new Vector3((float)i, (float)j, 0), 
                Quaternion.identity, 
                positionToInsantiate
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

