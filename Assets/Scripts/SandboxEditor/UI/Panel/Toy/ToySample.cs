using System.Collections;
using GameEditor.Data;
using GameEditor.ObjectEditor;
using GameEditor.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Panel
{
    public class ToySample : MonoBehaviour
    {
        // ObjectBuilder objectBuilder;
        [SerializeField] Text typeText, nameText;
        [SerializeField] Image displayImage;
        private Vector2 thumbNailBoxSize;
        private ToyDataContainer _toyDataContainer;
        private bool initialized = false;

        private void Initialize()
        {
            thumbNailBoxSize = displayImage.GetComponent<RectTransform>().rect.size;
            _toyDataContainer = GetComponent<ToyDataContainer>();
        }

        public void SetToySample(ToyData toyData)
        {
            if (!initialized)
                Initialize();
            _toyDataContainer.toyData = toyData;
            displayImage.sprite = ImageStorage.GetSprites(toyData.imageData)[0];
            displayImage.GetComponent<RectTransform>().sizeDelta = CalcThumbNailBoxSize();
            typeText.text = toyData.toyBuildData.toyType.ToString();
            nameText.text = toyData.toyBuildData.name;
        }    

        private Vector2 CalcThumbNailBoxSize()
        {
            var imageData = _toyDataContainer.ImageData;
            var toyHeight = imageData.GetHeight();
            var toyWidth = imageData.GetWidth();
            if(imageData.GetIsRelativeSize())
            {
                if(displayImage.sprite != null)
                {
                    toyHeight *= displayImage.sprite.texture.height;
                    toyWidth *= displayImage.sprite.texture.width;
                }
            }
            if(toyHeight > toyWidth)
            {
                toyWidth = (toyWidth/toyHeight) * thumbNailBoxSize.x;
                toyHeight = thumbNailBoxSize.y;
            }
            else
            {
                toyHeight = (toyHeight/toyWidth) * thumbNailBoxSize.y;
                toyWidth = thumbNailBoxSize.x;
            }
            return new Vector2(toyWidth,toyHeight);
        }
        public void WhenSampleClicked()
        {
            // objectBuilder.SetCurrentToyData(GetComponent<ToyDataContainer>().toyData);
        }
    }
}
