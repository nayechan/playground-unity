using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;

public class ObjectItemController : MonoBehaviour
{
    ObjectData _objectData;
    ImageStorage imageStorage;
    [SerializeField] Image image;
    [SerializeField] Text typeText, nameText;
    private ImageData currentImageData = null;
    float defaultWidth = 0, defaultHeight = 0;
    bool isImageLoaded = false;

    private void Awake() {
        Debug.Log("asdf");
        
        defaultWidth = image.GetComponent<RectTransform>().rect.width;
        defaultHeight = image.GetComponent<RectTransform>().rect.height;

        if(imageStorage == null)
        {
            imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
        }
        
        StartCoroutine("WaitUntilImageLoad");
    }
    public void SetData(ObjectData objectData)
    {
        _objectData = objectData;
        
        Debug.Log(_objectData.imageDataUUID);
        Debug.Log(imageStorage);

        if(imageStorage == null)
        {
            imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
        }

        currentImageData = imageStorage.GetImageData(_objectData.imageDataUUID);
        if(isImageLoaded)
        {
            RefreshUI();
        }
    }    

    public void RefreshUI()
    {
        image.sprite = currentImageData.GetSprites()[0];

        float h = currentImageData.GetHSize();
        float w = currentImageData.GetVSize();


        Debug.Log(h+" "+w);
        if(currentImageData.GetIsRelativeSize())
        {
            if(image.sprite != null)
            {
                h *= image.sprite.texture.height;
                w *= image.sprite.texture.width;
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

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);


        typeText.text = _objectData.toyType.ToString();
        nameText.text = _objectData.name;
    }

    IEnumerator WaitUntilImageLoad()
    {
        while(image.GetComponent<RectTransform>().rect.width == 0)
        {
            yield return null;
        }

        defaultWidth = image.GetComponent<RectTransform>().rect.width;
        defaultHeight = image.GetComponent<RectTransform>().rect.height;

        Debug.Log(defaultWidth+" "+defaultHeight);

        isImageLoaded = true;
        RefreshUI();

    }
}
