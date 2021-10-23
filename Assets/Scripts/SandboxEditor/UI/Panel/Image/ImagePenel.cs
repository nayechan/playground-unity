using GameEditor.Common;
using GameEditor.Data;
using GameEditor.Storage;
using UnityEngine;

/*
이미지 뷰어를 관리하는 스크립트입니다.
*/
namespace GameEditor.Resource.Image
{
    public class ImagePenel : MonoBehaviour
    {
        [SerializeField] GameObject imageSamplePrefab;
        [SerializeField] GameObject addImageButtonPrefab;
        [SerializeField] Transform contentPanel;

        public void UpdateViewerSamples(bool needImageAddButton)
        {
            foreach(Transform sample in contentPanel)
            {
                Destroy(sample.gameObject);
            }

            int row = 0;
            int col = 0;
        
            if(needImageAddButton)
            {
                var addImageButton =
                    GameObject.Instantiate(addImageButtonPrefab, contentPanel);
                addImageButton.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(70+340*col, -80-320*row);
                // var addImageButtonController =  addImageButton.GetComponent<AddImageButtonController>();
                // addImageButtonController.SetField(imagePanelSwitcher, imageEditor);
        // 더하기 버튼을 생성하고 누르면 이미지 추가창을 띄우는 동작 수행

                ++col;
                if(col>=4) {col=0; ++row;}
            }
        
            var imagesData = ImageStorage.GetImagesData();
            foreach(ImageData imageData in imagesData)
            {
                GameObject gameObject = Instantiate(imageSamplePrefab,contentPanel);
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
}
