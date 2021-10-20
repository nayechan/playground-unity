using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageSample : MonoBehaviour {

    [SerializeField] private Text titleText, typeText;
    [SerializeField] private Image imageGuide;
    bool isImageLoaded = false;
    float defaultWidth, defaultHeight;
    ImageData _containingImageData;
    public delegate void OnClick(ImageData imageData);
    public OnClick onClick;

    public void Awake()
    {
        StartCoroutine("WaitUntilImageLoad");
    }

    public void SetImageData(ImageData imageData)
    {
        _containingImageData = imageData;
        if(isImageLoaded)
            RefreshUI();
    }  

    public void RefreshUI()
    {
        imageGuide.sprite = ImageStorage.GetSprites(_containingImageData)[0];
        imageGuide.GetComponent<RectTransform>().sizeDelta = GetSampleSize(_containingImageData);
        titleText.text = _containingImageData.GetTitle();
        typeText.text = _containingImageData.GetIsUsingSingleImage() ? "Single" : "Multiple";
    }

    public Vector2 GetSampleSize(ImageData imageData)
    {
        float width = imageData.GetHSize();
        float height = imageData.GetVSize();
        if(imageData.GetIsRelativeSize())
        {
            if(imageGuide.sprite != null)
            {
                width *= imageGuide.sprite.texture.width;
                height *= imageGuide.sprite.texture.height;
            }
        }
        if(height > width)
        {
            width = (width/height) * defaultWidth;
            height = defaultHeight;
        }
        else
        {
            height = (height/width) * defaultHeight;
            width = defaultWidth;
        }
        return new Vector2(width,height);
    }

    IEnumerator WaitUntilImageLoad()
    {
        while(imageGuide.GetComponent<RectTransform>().rect.width == 0)
        {
            yield return null;
        }

        defaultWidth = imageGuide.GetComponent<RectTransform>().rect.width;
        defaultHeight = imageGuide.GetComponent<RectTransform>().rect.height;

        isImageLoaded = true;
        RefreshUI();
    }

    public void ExecuteOnClick()
    {
        if(onClick != null)
            onClick(_containingImageData);
    }

}