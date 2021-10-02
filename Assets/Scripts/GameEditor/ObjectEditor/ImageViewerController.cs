using System.Collections.Generic;
using UnityEngine;

public class ImageViewerController : MonoBehaviour
{
    [SerializeField] GameObject imageItemPrefab;
    [SerializeField] Transform contentPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI(List<ImageData> imageDatas)
    {
        foreach(Transform transform in contentPanel)
        {
            if(transform.gameObject.name != "AddObject")
                Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 1;

        foreach(ImageData data in imageDatas)
        {
            GameObject gameObject = Instantiate(imageItemPrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);

            gameObject.GetComponent<ImageItemController>().SetImageData(data);

            ++col;
            if(col>=4) {col=0; ++row;}
        }

        float sizeX = contentPanel.GetComponent<RectTransform>().sizeDelta.x;

        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
            sizeX,
            400 + row*320
        );
    }
}
