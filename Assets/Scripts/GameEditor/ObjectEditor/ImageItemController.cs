using UnityEngine;
using UnityEngine.UI;

public class ImageItemController : MonoBehaviour {

    [SerializeField] private Text titleText;

    ImageData _imageData;
    public void SetImageData(ImageData data)
    {
        _imageData = data;
        RefreshUI();
    }  
    public void RefreshUI()
    {
        titleText.text = _imageData.GetTitle();
    }
}