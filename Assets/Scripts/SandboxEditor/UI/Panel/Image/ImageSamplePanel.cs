using System.Collections.Generic;
using System.Linq;
using GameEditor.Data;
using GameEditor.Storage;
using UnityEngine;

/*
이미지 뷰어를 관리하는 스크립트입니다.
*/
namespace GameEditor.Resource.Image
{
    public class ImageSamplePanel : MonoBehaviour
    {
        [SerializeField] GameObject imageSamplePrefab;
        public GameObject addImageButton;
        [SerializeField] Transform contentPanel;
        private const int COL_MAX = 4;
        private const int SAMPLE_MAX = 100;
        private List<GameObject> imageSamples;
        private bool initialized = false;
        public bool includeAddButton;

        private void Initialize()
        {
            PreSetImageSamples();
            PreSetPanelSize();
            SetPlusButton(includeAddButton);
            initialized = true;
        }

        private void PreSetImageSamples()
        {
            imageSamples = new List<GameObject>();
            for (var i = 0; i < SAMPLE_MAX; ++i)
            {
                imageSamples.Add(Instantiate(imageSamplePrefab, contentPanel));
                imageSamples[i].GetComponent<RectTransform>().anchoredPosition = GetNthAnchoredPosition(i);
            }
        }

        private void PreSetPanelSize()
        {
            contentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(
                contentPanel.GetComponent<RectTransform>().sizeDelta.x,
                400 + SAMPLE_MAX/COL_MAX*320
            );
            ConcealSamples();
        }

        public void RefreshPanel()
        {
            if(!initialized)
                Initialize();
            ConcealSamples();
            RefreshSamples();
            SetPlusButton(includeAddButton);
        }
        private void ConcealSamples()
        {
            foreach(var sample in imageSamples)
                sample.SetActive(false);
        }

        private void RefreshSamples()
        {
            var imagesData = ImageStorage.GetImagesData();
            for (var i = 0; i < imagesData.Count; ++i)
                RefreshSample(imageSamples[i], imagesData[i]);
        }

        private static void RefreshSample(GameObject sample, ImageData imageData)
        {
            sample.GetComponent<ImageSample>().SetImageDataAndRefreshThumbnail(imageData);
            sample.SetActive(true);
        }
        
        private void SetPlusButton(bool includeButton)
        {
            addImageButton.SetActive(includeButton);
            addImageButton.GetComponent<RectTransform>().anchoredPosition
                = GetNthAnchoredPosition(ImageStorage.ImageDataCount);
        }
        

        public static Vector2 GetNthAnchoredPosition(int n)
        {
            var col = n % COL_MAX;
            var row = n / COL_MAX;
            return new Vector2(70 + 340 * col, -80 - 320 * row);
        }
        
        public void WhenImageSampleClicked(ImageSample.WhenImageSampleClicked behavior)
        {
            for(var i = 0; i < ImageStorage.ImageDataCount; i++)
            {
                var imageSample = imageSamples[i].GetComponent<ImageSample>();
                imageSample.whenImageSampleClicked = behavior;
            }
        }
    }
}
