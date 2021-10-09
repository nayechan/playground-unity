using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageItemController : MonoBehaviour {

    [SerializeField] private Text titleText, typeText;
    [SerializeField] private Image image;

    bool isImageLoaded = false;

    float defaultWidth, defaultHeight;

    ImageData _imageData;

    public void Awake()
    {
        StartCoroutine("WaitUntilImageLoad");
    }
    public void SetImageData(ImageData data)
    {
        _imageData = data;
        if(isImageLoaded)
            RefreshUI();
    }  
    public void RefreshUI()
    {
        image.sprite = _imageData.GetSprites()[0];

        float h = _imageData.GetHSize();
        float w = _imageData.GetVSize();


        Debug.Log(h+" "+w);
        if(_imageData.GetIsRelativeSize())
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

        Debug.Log(_imageData.GetTitle());
        titleText.text = _imageData.GetTitle();
        typeText.text = _imageData.GetIsUsingSingleImage() ? "Single" : "Multiple";
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