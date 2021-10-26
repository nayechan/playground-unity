using System;
using GameEditor.Common;
using GameEditor.Data;
using GameEditor.Resource.Image;
using GameEditor.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Object
{
    public class ToySampleBuildPanel : MonoBehaviour
    {
        [SerializeField] Image toySampleImage;
        [SerializeField] InputField nameInputField;
        [SerializeField] Dropdown typeDropdown, colliderDropdown;
        [SerializeField] ImageSamplePanel toyImagePanel;
        private ImageData _selectedImageData;
        public PanelSwitch closeImageSelectorAndOpenToyBuilder;

        public void OnAddButtonClick()
        {
            var toyData = BuildToyData();
            if (!IsValid(toyData)) return;
            ToyStorage.AddToyData(toyData);
            ResetInputBox();
        }

        private ToyData BuildToyData()
        {
            var toyData = new ToyData
            {
                toyBuildData = MakeToyBuildData(),
                imageData = _selectedImageData
            };
            return toyData;
        }
        private static bool IsValid(ToyData toyData)
        {
            return toyData.imageData != null;
        }
        

        public void OpenToyImageSelector()
        {
            toyImagePanel.WhenImageSampleClicked(SendImageDataToToyPanel);
        }

        private void SendImageDataToToyPanel(ImageData imageData)
        {
            toySampleImage.sprite = ImageStorage.GetSprites(imageData)[0];
            closeImageSelectorAndOpenToyBuilder.Apply();
            _selectedImageData = imageData;
        }

        private ToyBuildData MakeToyBuildData()
        {
            var toyBuildData = new ToyBuildData
            {
                name = nameInputField.text
            };
            var typeString = typeDropdown.options[typeDropdown.value].text;
            var toyType = (ToyType)ToyType.Parse(typeof(ToyType),typeString);
            toyBuildData.toyType = toyType;
            var colliderTypeString = colliderDropdown.options[colliderDropdown.value].text;
            var colliderType = (ColliderType)ColliderType.Parse(typeof(ColliderType),colliderTypeString);
            toyBuildData.colliderType = colliderType;
            return toyBuildData;
        }

        public void OnCancelButtonClick()
        {
            ResetInputBox();            
        }

        private void ResetInputBox()
        {
            toySampleImage.sprite = null;
            nameInputField.text = "";
            colliderDropdown.value = 0;
            typeDropdown.value = 0;
            _selectedImageData = null;
        }
    }
}