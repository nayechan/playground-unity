using System.Collections.Generic;
using UnityEngine;

/*
이미지 뷰어를 관리하는 스크립트입니다.
*/
public class ImageViewerController : MonoBehaviour
{
    [SerializeField] GameObject imageItemPrefab, addImageButtonPrefab;
    [SerializeField] Transform contentPanel;
    [SerializeField] GameObject imageEditor;
    [SerializeField] PanelSwitcher imagePanelSwitcher;

    /*
    isSelectMode: 
    True    -> 이미지 선택 모드, 
    False   -> 이미지 추가 모드
    위의 데이터를 기반으로 UI를 구성하여 리프레시합니다.
    */
    public void RefreshUI(bool isSelectMode)
    {
        foreach(Transform transform in contentPanel)
        {
            Destroy(transform.gameObject);
        }

        int row = 0;
        int col = 0;

        
        if(!isSelectMode)
        {
            //선택 모드 여부 따라 이미지 추가 버튼을 생성
            GameObject addImageButton =
            GameObject.Instantiate(addImageButtonPrefab, contentPanel);
            addImageButton.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);
            var addImageButtonController =  addImageButton.GetComponent<AddImageButtonController>();
            addImageButtonController.SetField(imagePanelSwitcher, imageEditor);

            ++col;
            if(col>=4) {col=0; ++row;}
        }
        
        var imagesData = ImageStorage.GetImagesData();
        foreach(ImageData imageData in imagesData)
        {
            GameObject gameObject = Instantiate(imageItemPrefab,contentPanel);
            gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(70+340*col, -80-320*row);

            gameObject.GetComponent<ImageSample>().SetImageData(imageData);

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
