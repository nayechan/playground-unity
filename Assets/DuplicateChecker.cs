using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("EventSystem").Length > 1)
            GameObject.Destroy(gameObject);
    }
}
