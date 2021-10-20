using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;

public class ToySample : MonoBehaviour
{
    ObjectBuilder objectBuilder;
    [SerializeField] Text typeText, nameText;
    float defaultWidth = 0, defaultHeight = 0;
    bool isImageLoaded = false;
    [SerializeField] Image displayImage;

    private void Awake() {
        
        defaultWidth = displayImage.GetComponent<RectTransform>().rect.width;
        defaultHeight = displayImage.GetComponent<RectTransform>().rect.height;

        if(objectBuilder == null)
        {
            objectBuilder = GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();
        }
        
        StartCoroutine("WaitUntilImageLoad");
    }

    public void SetDisplayInstanceData(ToyData toyData)
    {
        Debug.Log(JsonUtility.ToJson(toyData));
        Debug.Log(JsonUtility.ToJson(toyData.imageData));
        displayImage.sprite = ImageStorage.GetSprites(toyData.imageData)[0];
        typeText.text = toyData.objectData.toyType.ToString();
        nameText.text = toyData.objectData.name;
    }    

    public void RefreshUI()
    {
        displayImage.sprite = ImageStorage.GetSprites(GetComponent<ToyDataContainer>().ImageData)[0];

        float h = GetComponent<ToyDataContainer>().ImageData.GetVSize();
        float w = GetComponent<ToyDataContainer>().ImageData.GetHSize();


        Debug.Log(h+" "+w);
        if(GetComponent<ToyDataContainer>().ImageData.GetIsRelativeSize())
        {
            if(displayImage.sprite != null)
            {
                h *= displayImage.sprite.texture.height;
                w *= displayImage.sprite.texture.width;
            }
            
        }
        
        Debug.Log(h+" "+w);

        if(h > w)
        {
            w = (w/h) * defaultWidth;
            h = defaultHeight;
        }
        else
        {
            h = (h/w) * defaultHeight;
            w = defaultWidth;
        }
        
        Debug.Log(h+" "+w);

        displayImage.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);


        typeText.text = GetComponent<ToyDataContainer>().ObjectData.toyType.ToString();
        nameText.text = GetComponent<ToyDataContainer>().ObjectData.name;
    }

    IEnumerator WaitUntilImageLoad()
    {
        while(displayImage.GetComponent<RectTransform>().rect.width == 0)
        {
            yield return null;
        }

        defaultWidth = displayImage.GetComponent<RectTransform>().rect.width;
        defaultHeight = displayImage.GetComponent<RectTransform>().rect.height;

        Debug.Log(defaultWidth+" "+defaultHeight);

        isImageLoaded = true;
        RefreshUI();

    }

    public void OnButtonClick()
    {
        objectBuilder.SetCurrentToyData(GetComponent<ToyDataContainer>().toyData);
    }
}
