using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private bool status = false;
    private float width;
    [SerializeField] private GameObject sidebar;
    
    private void Awake() {
        width = GetComponent<RectTransform>().sizeDelta.x;
        width += sidebar.GetComponent<RectTransform>().sizeDelta.x;
    }
    public void ToggleStatus()
    {
        status = !status;

        if(status)
        {
            StartCoroutine("MoveMenu",new KeyValuePair<float,float>(width, 60.0f));
        }
        else
        {
            StartCoroutine("MoveMenu",new KeyValuePair<float,float>(-width, 60.0f));
        }
    }

    IEnumerator MoveMenu(KeyValuePair<float,float> pair)
    {
        if(pair.Key > 0)
        {
            for(float i=0;i<pair.Key;i+=(pair.Key/pair.Value))
            {
                GetComponent<RectTransform>().anchoredPosition += new Vector2(
                    (pair.Key/pair.Value),0
                );
                yield return null;
            }
        }
        else
        {
            for(float i=0;i<-pair.Key;i+=(-pair.Key/pair.Value))
            {
                GetComponent<RectTransform>().anchoredPosition += new Vector2(
                    (pair.Key/pair.Value),0
                );
                yield return null;
            }
        }
    }
}
