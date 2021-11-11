using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using SandboxEditor.UI.Panel.Image;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxEditor.UI.Panel.Toy
{
    public class ToySampleBuildPanel : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Image toySampleImage;
        [SerializeField] private InputField nameInputField;
        [SerializeField] private Dropdown toyTypeDropdown, colliderDropdown;
        [SerializeField] private Toggle movableToggle;
        [SerializeField] private ImageSamplePanel toyImagePanel;
        private ImageData _selectedImageData;
        public PanelSwitch closeImageSelectorAndOpenToyBuilder;

        public void OnAddButtonClick()
        {
            var toyRecipe = BuildToyRecipe();
            if (!IsValid(toyRecipe)) return;
            var newToyData = new ToyData(toyRecipe);
            ToyPrefabDataStorage.AddToyRecipeData(newToyData);
            ResetInputBox();
        }

        private ToyRecipe BuildToyRecipe()
        {
            return new ToyRecipe()
            {
                toyBuildData = BuildToyBuildDataFromInput(),
                imageData = _selectedImageData
            };
        }
        private static bool IsValid(ToyRecipe toyRecipe)
        {
            return toyRecipe.imageData != null;
        }
        
        private void SendImageDataToToyPanel(ImageData imageData)
        {
            toySampleImage.sprite = ImageStorage.GetSprites(imageData)[0];
            closeImageSelectorAndOpenToyBuilder.Apply();
            _selectedImageData = imageData;
        }

        public void OpenToyImageSelector()
        {
            toyImagePanel.SetBehaviorWhenImageSampleClicked(SendImageDataToToyPanel);
        }


        private ToyBuildData BuildToyBuildDataFromInput()
        {
            var toyBuildData = new ToyBuildData
            {
                name = nameInputField.text,
                isFixed = !movableToggle.isOn
            };
            var typeString = toyTypeDropdown.options[toyTypeDropdown.value].text;
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
            toyTypeDropdown.value = 0;
            _selectedImageData = null;
        }
    }
}