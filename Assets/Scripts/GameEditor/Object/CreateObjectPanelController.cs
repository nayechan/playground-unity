using System.Collections;
using System.Collections.Generic;
using GameEditor.Data;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjectPanelController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] InputField nameInputField;
    [SerializeField] Dropdown typeDropdown, colliderDropdown;
    [SerializeField] PanelSwitcher panelSwitcher;
    [SerializeField] Transform transformSelectObjectPanel, imageSelector;
    [SerializeField] ToyStorage toyStorage;
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
        var imageStorage = ImageStorage.GetSingleton();
        image.sprite = imageStorage.GetSprites(_currentImageData)[0];
    }

    public void OpenImageSelector()
    {
        imageSelector.GetComponent<ImageSelectorController>().SetOnClick(
            (ImageData imageData)=>{
                _currentImageData = imageData;
                panelSwitcher.OpenPanel(transform);
                RefreshUI();
            }
        );
        panelSwitcher.OpenPanel(imageSelector);
    }

    public ToyData BuildToyData()
    {
        var toyData = new ToyData();
        toyData.objectData = MakeObjectData();
        return toyData;
    }

    private ObjectData MakeObjectData()
    {
        var objectData = new ObjectData();
        objectData.name = nameInputField.text;
        var typeString = typeDropdown.options[typeDropdown.value].text;
        var toyType = (ToyType)ToyType.Parse(typeof(ToyType),typeString);
        objectData.toyType = toyType;
        var colliderTypeString = colliderDropdown.options[colliderDropdown.value].text;
        var colliderType = (ColliderType)ColliderType.Parse(typeof(ColliderType),colliderTypeString);
        objectData.colliderType = colliderType;
        return objectData;
    }

    public void OnCancelButtonClick()
    {
        panelSwitcher.OpenPanel(transformSelectObjectPanel);
        
        ResetPanel();            
    }

    public void OnAddButtonClick()
    {
        if(ValidateForm())
        {
<<<<<<< HEAD
            GameEditor.Data.ObjectData objectData = GenerateObjectData();

            objectStorage.AddObjectData(objectData);

            ResetPanel();            

=======
            ToyData toyData = BuildToyData();
            toyStorage.AddToyData(toyData);            
>>>>>>> gameeditor_tae
            panelSwitcher.OpenPanel(transformSelectObjectPanel);     
        }
    }

    void ResetPanel()
    {
        image.sprite = null;
        nameInputField.text = "";
        colliderDropdown.value = 0;
        typeDropdown.value = 0;
    }
}
