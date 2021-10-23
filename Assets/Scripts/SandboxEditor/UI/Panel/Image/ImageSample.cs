using System.Collections;
using GameEditor.Data;
using GameEditor.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Resource.Image
{
    public class ImageSample : MonoBehaviour {

        [SerializeField] private Text titleText, typeText;
        [SerializeField] private UnityEngine.UI.Image imageGuide;
        bool isImageLoaded = false;
        float defaultWidth, defaultHeight;
        ImageData _containingImageData;
        public delegate void WhenImageSampleClicked(ImageData imageData);
        public WhenImageSampleClicked whenImageSampleClicked;

        public void Awake()
        {
            StartCoroutine("WaitUntilImageLoad");
        }

        public void SetImageData(ImageData imageData)
        {
            _containingImageData = imageData;
            if(isImageLoaded)
                RefreshSampleUI();
        }  

        public void RefreshSampleUI()
        {
            imageGuide.sprite = ImageStorage.GetSprites(_containingImageData)[0];
            imageGuide.GetComponent<RectTransform>().sizeDelta = GetSampleSize(_containingImageData);
            titleText.text = _containingImageData.GetTitle();
            typeText.text = _containingImageData.GetIsUsingSingleImage() ? "Single" : "Multiple";
        }

        public Vector2 GetSampleSize(ImageData imageData)
        {
            float width = imageData.GetWidth();
            float height = imageData.GetHeight();
            if(imageData.GetIsRelativeSize())
            {
                if(imageGuide.sprite != null)
                {
                    width *= imageGuide.sprite.texture.width;
                    height *= imageGuide.sprite.texture.height;
                }
            }
            if(height > width)
            {
                width = (width/height) * defaultWidth;
                height = defaultHeight;
            }
            else
            {
                height = (height/width) * defaultHeight;
                width = defaultWidth;
            }
            return new Vector2(width,height);
        }

        IEnumerator WaitUntilImageLoad()
        {
            while(imageGuide.GetComponent<RectTransform>().rect.width == 0)
            {
                yield return null;
            }

            defaultWidth = imageGuide.GetComponent<RectTransform>().rect.width;
            defaultHeight = imageGuide.GetComponent<RectTransform>().rect.height;

            isImageLoaded = true;
            RefreshSampleUI();
        }

        public void InvokeClickEvent()
        {
            whenImageSampleClicked?.Invoke(_containingImageData);
        }

    }
}