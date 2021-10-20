
using System.Collections.Generic;
using UnityEngine;

/*
이미지 선택 화면을 관리하는 스크립트입니다.
*/
public class ImageSelectorController : MonoBehaviour
{
    [SerializeField] GameObject imageItemPrefab;
    [SerializeField] Transform contentPanel;
    List<ImageItemController> items;
    
    // 이미지 데이터를 기반으로 UI를 재구성합니다.
    private void Awake() {
    }
    public void RefreshUI()
    {
        foreach(Transform transform in contentPanel)
        {
            Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 0;
        
        var imagesData = ImageStorage.GetImagesData();
        foreach(ImageData imageData in imagesData)
        {
            GameObject gameObject = Instantiate(imageItemPrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);

            gameObject.GetComponent<ImageItemController>().SetImageData(imageData);
            items = new List<ImageItemController>();
            items.Add(gameObject.GetComponent<ImageItemController>());

            ++col;
            if(col>=4) {col=0; ++row;}
        }

        float sizeX = contentPanel.GetComponent<RectTransform>().sizeDelta.x;

        contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
            sizeX,
            400 + row*320
        );
    }

    public void SetOnClick(ImageItemController.OnClick onClick)
    {
        Debug.Log(onClick);
        foreach(ImageItemController item in items)
        {
            item.onClick += onClick;
        }
    }
}
