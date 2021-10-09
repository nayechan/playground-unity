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
    private void Awake() {
        Debug.Log("asdf");
        if(imageStorage == null)
        {
            imageStorage = GameObject.Find("ImageStorage").GetComponent<ImageStorage>();
        }
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


        image.sprite = 
        imageStorage.GetImageData(_objectData.imageDataUUID).GetSprites()[0];


        typeText.text = _objectData.toyType.ToString();
        nameText.text = _objectData.name;
    }    
}
