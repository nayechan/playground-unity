using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectTemplateController : MonoBehaviour
{
    private ObjectPrimitiveData data;
    private const float defaultSizeX=160.0f;
    private const float defaultSizeY=160.0f;
    private GameObject currentObject;

    void Awake() {
        currentObject = 
        GameObject.Find("Objects").transform.Find("currentObject").gameObject;
    }

    void UIRefresh()
    {
        Image image = transform.Find("Image").GetComponent<Image>();
        Text typeText = transform.Find("TileType").Find("Text").GetComponent<Text>();
        Text nameText = transform.Find("Text").GetComponent<Text>();

        float sizeX = data.GetWidth();
        float sizeY = data.GetHeight();
        if(sizeX>sizeY)
        {
            sizeY=sizeY/sizeX*defaultSizeY;
            sizeX=defaultSizeX;
        }
        else{
            sizeX=sizeX/sizeY*defaultSizeX;
            sizeY=defaultSizeY;
        }
        image.GetComponent<RectTransform>().
        sizeDelta = new Vector2(sizeX, sizeY);
        if(data.GetSprites().Length > 0)
        {
            image.sprite = data.GetSprites()[0];
        }
        
        typeText.text = data.GetObjectType();
        nameText.text = data.GetObjectName();
    }

    public void SetData(ObjectPrimitiveData data)
    {
        this.data = data;
        UIRefresh();
    }

    public void SelectObject()
    {
        currentObject.GetComponent<ObjectInstanceController>().SetObjectPrimitiveData(data);
        currentObject.transform.localScale = new Vector3(
            data.GetWidth(), data.GetHeight(), 1
        );
    }
}
