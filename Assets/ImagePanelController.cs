using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePanelController : MonoBehaviour
{   
    public void OpenPanel(Transform panel)
    {
        ResetPanel();
        panel.gameObject.SetActive(true);
    }

    public void ResetPanel()
    {
        foreach(Transform _transform in transform)
        {
            Debug.Log(_transform.name);
            if(_transform.name != "Title" && _transform.name != "Close Button")
            {
                _transform.gameObject.SetActive(false);
            }
        }
    }
}
