using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;

public class ObjectItemController : MonoBehaviour
{
// <<<<<<< HEAD
    //ObjectData _objectData;
    ImageStorage imageStorage;
    ObjectBuilder objectBuilder;
    [SerializeField] Text typeText, nameText;
    float defaultWidth = 0, defaultHeight = 0;
    bool isImageLoaded = false;
    [SerializeField] Image displayImage;

    private void Awake() {
        Debug.Log("asdf");
        
        defaultWidth = displayImage.GetComponent<RectTransform>().rect.width;
        defaultHeight = displayImage.GetComponent<RectTransform>().rect.height;

        if(imageStorage == null)
        {
            imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
        }

        if(objectBuilder == null)
        {
            objectBuilder = GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();
        }
        
        StartCoroutine("WaitUntilImageLoad");
    }
//     public void SetData(ObjectData objectData)
//     {
//         ImageData currentImageData = null;

//         if(imageStorage == null)
//         {
//             imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
//         }

//         currentImageData = imageStorage.GetImageData(objectData.imageDataUUID);

//         Debug.Log(objectData.name);
//         GetComponent<ToyData>().SetToyDataResource(objectData, currentImageData);
//         Debug.Log(GetComponent<ToyData>().GetInstanceID());

//         if(isImageLoaded)
//         {
//             RefreshUI();
//         }
// =======

    public void SetDisplayInstanceData(ToyData toyData)
    {
        displayImage.sprite = ImageStorage.GetSprites(toyData.imageData)[0];
        typeText.text = toyData.objectData.toyType.ToString();
        nameText.text = toyData.objectData.name;
// >>>>>>> gameeditor_tae
    }    

    public void RefreshUI()
    {
        displayImage.sprite = ImageStorage.GetSprites(GetComponent<ToyData>().imageData)[0];

        float h = GetComponent<ToyData>().imageData.GetVSize();
        float w = GetComponent<ToyData>().imageData.GetHSize();


        Debug.Log(h+" "+w);
        if(GetComponent<ToyData>().imageData.GetIsRelativeSize())
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


        typeText.text = GetComponent<ToyData>().objectData.toyType.ToString();
        nameText.text = GetComponent<ToyData>().objectData.name;
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
        objectBuilder.SetCurrentToyData(GetComponent<ToyData>());
    }
}
