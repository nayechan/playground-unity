using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjectPanelController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] InputField nameInputField;
    [SerializeField] Dropdown typeDropdown, colliderDropdown;
    [SerializeField] PanelSwitcher panelSwitcher;
    [SerializeField] Transform transformSelectObjectPanel, imageSelector;
    [SerializeField] ObjectStorage objectStorage;
    ImageData _currentImageData = null;

    public bool ValidateForm()
    {
        return true;
    }

    void ResetComponent()
    {
        
    }

    public void RefreshUI()
    {
        image.sprite = _currentImageData.GetSprites()[0];
    }

    public void OpenImageSelector()
    {
        Debug.Log("asdf");
        imageSelector.GetComponent<ImageSelectorController>().SetOnClick(
            (ImageData imageData)=>{
                _currentImageData = imageData;
                panelSwitcher.OpenPanel(transform);
                RefreshUI();
            }
        );
        panelSwitcher.OpenPanel(imageSelector);
    }

    public GameEditor.Data.ObjectData GenerateObjectData()
    {
        GameEditor.Data.ObjectData objectData = new GameEditor.Data.ObjectData();

        objectData.name = nameInputField.text;
        
        string typeString = typeDropdown.options[typeDropdown.value].text;
        object toyType = GameEditor.Data.ToyType.Parse(
            typeof(GameEditor.Data.ToyType),typeString
        );
        objectData.toyType = (GameEditor.Data.ToyType)toyType;

        string colliderTypeString = colliderDropdown.options[colliderDropdown.value].text;
        object colliderType = GameEditor.Data.ColliderType.Parse(
            typeof(GameEditor.Data.ColliderType),colliderTypeString
        );
        objectData.colliderType = (GameEditor.Data.ColliderType)toyType;

        objectData.imageDataUUID = _currentImageData.GetUUID();

        return objectData;
    }

    public void OnCancelButtonClick()
    {
        panelSwitcher.OpenPanel(transformSelectObjectPanel);
    }

    public void OnAddButtonClick()
    {
        if(ValidateForm())
        {
            GameEditor.Data.ObjectData objectData = GenerateObjectData();

            objectStorage.AddObjectData(objectData);            

            panelSwitcher.OpenPanel(transformSelectObjectPanel);     
        }
    }
}
