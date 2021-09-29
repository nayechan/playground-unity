using UnityEngine;
using UnityEngine.UI;

public class ImageItemController : MonoBehaviour {

    [SerializeField] private Text typeText;
    [SerializeField] private Text titleText;

    ImageData _imageData;
    public void SetImageData(ImageData data)
    {
        _imageData = data;
        RefreshUI();
    }  
    public void RefreshUI()
    {
        typeText.text = _imageData.GetType();
        titleText.text = _imageData.GetTitle();
    }
}