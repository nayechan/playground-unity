
using System.Collections.Generic;
using UnityEngine;

/*
이미지 선택 화면을 관리하는 스크립트입니다.
*/
public class ImageSelectorController : MonoBehaviour
{
    [SerializeField] GameObject imageItemPrefab;
    [SerializeField] Transform contentPanel;
    [SerializeField] ImageStorage imageStorage;
    ImageItemController.OnClick _onClick;

    List<ImageItemController> currentItemList;
    
    // 이미지 데이터를 기반으로 UI를 재구성합니다.
    private void Awake() {
    }
    public void RefreshUI(Dictionary<string, ImageData> imageDatas)
    {
        foreach(Transform transform in contentPanel)
        {
            Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 0;
        

        foreach(ImageData data in imageDatas.Values)
        {
            GameObject gameObject = Instantiate(imageItemPrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);

            gameObject.GetComponent<ImageItemController>().SetImageData(data);
            currentItemList = new List<ImageItemController>();
            currentItemList.Add(gameObject.GetComponent<ImageItemController>());

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
        _onClick = onClick;

        foreach(ImageItemController item in currentItemList)
        {
            item.onClick += _onClick;
        }

        
    }
}
