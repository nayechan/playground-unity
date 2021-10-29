using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxEditor.UI.Panel.Image
{
    public class ImageSample : MonoBehaviour {

        [SerializeField] private Text titleText, typeText;
        [SerializeField] private UnityEngine.UI.Image imageGuide;
        private float defaultWidth, defaultHeight;
        ImageData _imageData;
        public delegate void WhenImageSampleClicked(ImageData imageData);
        public WhenImageSampleClicked whenImageSampleClicked;
        private bool initialized = false;

        public void Initialize()
        {
            defaultWidth = imageGuide.GetComponent<RectTransform>().rect.width;
            defaultHeight = imageGuide.GetComponent<RectTransform>().rect.height;
        }

        public void SetImageDataAndRefreshThumbnail(ImageData imageData)
        {
            if(!initialized)
                Initialize();
            _imageData = imageData;
            RefreshSampleUI();
        }

        private void RefreshSampleUI()
        {
            imageGuide.sprite = ImageStorage.GetSprites(_imageData)[0];
            imageGuide.GetComponent<RectTransform>().sizeDelta = GetSampleSize(_imageData);
            titleText.text = _imageData.GetTitle();
            typeText.text = _imageData.GetIsUsingSingleImage() ? "Single" : "Multiple";
        }

        private Vector2 GetSampleSize(ImageData imageData)
        {
            var (width, height) = (imageData.GetWidth(), imageData.GetHeight());
            if(imageData.GetIsRelativeSize())
            {
                width *= imageGuide.sprite.texture.width;
                height *= imageGuide.sprite.texture.height;
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

        public void InvokeClickEvent()
        {
            whenImageSampleClicked?.Invoke(_imageData);
        }

    }
}