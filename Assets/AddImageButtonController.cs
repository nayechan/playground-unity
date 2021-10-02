using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddImageButtonController : MonoBehaviour
{
    Transform imagePanel;
    [SerializeField] string target;
    private void Awake() {
        imagePanel = transform.parent.parent.parent.parent;
        //Dirty Code
    }    
    
    public void OnButton()
    {
        imagePanel.GetComponent<ImagePanelController>().OpenPanel(imagePanel.Find(target));
    }
}
