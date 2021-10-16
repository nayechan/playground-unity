using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;

public class ObjectItemController : MonoBehaviour
{
    [SerializeField] Image displayImage;
    [SerializeField] Text typeText, nameText;

    public void SetDisplayInstanceData(ToyData toyData)
    {
        var imageStorage = ImageStorage.GetSingleton();
        displayImage.sprite = imageStorage.GetSprites(toyData.imageData)[0];
        typeText.text = toyData.objectData.toyType.ToString();
        nameText.text = toyData.objectData.name;
    }    
}
