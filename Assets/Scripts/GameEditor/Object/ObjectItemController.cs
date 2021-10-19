using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;

public class ObjectItemController : MonoBehaviour
{
<<<<<<< HEAD
    //ObjectData _objectData;
    ImageStorage imageStorage;
    ObjectBuilder objectBuilder;
    [SerializeField] Image image;
    [SerializeField] Text typeText, nameText;
    //private ImageData currentImageData = null;
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

        if(objectBuilder == null)
        {
            objectBuilder = GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();
        }
        
        StartCoroutine("WaitUntilImageLoad");
    }
    public void SetData(ObjectData objectData)
    {
        ImageData currentImageData = null;

        if(imageStorage == null)
        {
            imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
        }

        currentImageData = imageStorage.GetImageData(objectData.imageDataUUID);

        Debug.Log(objectData.name);
        GetComponent<DataAgent>().SetDataAgentResource(objectData, currentImageData);
        Debug.Log(GetComponent<DataAgent>().GetInstanceID());

        if(isImageLoaded)
        {
            RefreshUI();
        }
=======
    [SerializeField] Image displayImage;
    [SerializeField] Text typeText, nameText;

    public void SetDisplayInstanceData(ToyData toyData)
    {
        var imageStorage = ImageStorage.GetSingleton();
        displayImage.sprite = imageStorage.GetSprites(toyData.imageData)[0];
        typeText.text = toyData.objectData.toyType.ToString();
        nameText.text = toyData.objectData.name;
>>>>>>> gameeditor_tae
    }    

    public void RefreshUI()
    {
        
        image.sprite = GetComponent<DataAgent>().imageData.GetSprites()[0];

        float h = GetComponent<DataAgent>().imageData.GetVSize();
        float w = GetComponent<DataAgent>().imageData.GetHSize();


        Debug.Log(h+" "+w);
        if(GetComponent<DataAgent>().imageData.GetIsRelativeSize())
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


        typeText.text = GetComponent<DataAgent>().objectData.toyType.ToString();
        nameText.text = GetComponent<DataAgent>().objectData.name;
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

    public void OnButtonClick()
    {
        objectBuilder.SetCurrentDataAgent(GetComponent<DataAgent>());
    }
}
