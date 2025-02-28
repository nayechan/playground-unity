using System.Collections.Generic;
using SandboxEditor.Data.Storage;
using SandboxEditor.UI.Panel.Image;
using UnityEngine;

/*
이미지 선택 화면을 관리하는 스크립트입니다.
*/
namespace Deprecation
{
    public class ToyImageSelectPanel : MonoBehaviour
    {
        [SerializeField] GameObject imageSamplePrefab;
        [SerializeField] Transform imageSelectorPanel;
        List<ImageSample> _containingImagesSample;
    
        public void RefreshUI()
        {
            foreach(Transform imageSample in imageSelectorPanel)
            {
                Destroy(imageSample.gameObject);
            }

            int row = 0;
            int col = 0;
            BuildSamplesAndUpdateContainer(ref row, ref col);
            AdjustPanelSize(row);
        }

        private void BuildSamplesAndUpdateContainer(ref int row, ref int col)
        {
            var imagesData = ImageStorage.GetImagesData();
            _containingImagesSample = new List<ImageSample>();
            foreach(var imageData in imagesData)
            {
                var newImageSample = Instantiate(imageSamplePrefab, imageSelectorPanel);
                newImageSample.GetComponent<ImageSample>().SetImageDataAndRefreshThumbnail(imageData);
                _containingImagesSample.Add(newImageSample.GetComponent<ImageSample>());

                newImageSample.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(70+340*col, -80-320*row);

                ++col;
                if(col>=4) {col=0; ++row;}
            }
        }

        private void AdjustPanelSize(int row)
        {
            float sizeX = imageSelectorPanel.GetComponent<RectTransform>().sizeDelta.x;
            imageSelectorPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
                sizeX,
                400 + row*320
            );
        }

        public void SetBehavior(ImageSample.WhenImageSampleClicked WhenImageSampleClicked)
        {
            foreach(var imageSample in _containingImagesSample)
            {
                imageSample.whenImageSampleClicked += WhenImageSampleClicked;
            }
        }
    }
}
