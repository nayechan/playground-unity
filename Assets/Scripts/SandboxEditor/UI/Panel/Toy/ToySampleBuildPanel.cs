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
        [SerializeField] Transform imageSelector;
        private ImageData _selectedImageData;
        public PanelSwitch closeToyBuilderAndOpenImageSelector;
        public PanelSwitch closeImageSelectorAndOpenToyBuilder;
        public PanelSwitch closeToyBuilderAndOpenToyMainPanel;

        public void OnAddButtonClick()
        {
            var toyData = BuildToyData();
            if (!IsValid(toyData)) return;
            ToyStorage.AddToyData(toyData);
            closeToyBuilderAndOpenToyMainPanel.Apply();
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
            imageSelector.GetComponent<ToyImageSelectPanel>().SetBehavior(WhenImageSampleClicked);
            closeToyBuilderAndOpenImageSelector.Apply();
        }

        private void WhenImageSampleClicked(ImageData imageData)
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
            closeToyBuilderAndOpenToyMainPanel.Apply();
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