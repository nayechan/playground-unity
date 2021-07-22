using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjectPanelController : MonoBehaviour
{
    [SerializeField] ImageStorage imageStorage;
    [SerializeField] SelectObjectPanelController selectObjectPanel;
    [SerializeField] SelectMainController selectMain;
    [SerializeField] InputField nameInput, horizontalSizeInput, verticalSizeInput;
    [SerializeField] Dropdown typeDropdown;

    public bool ValidateForm()
    {
        bool result = true;
        float h, v;
        result &= float.TryParse(horizontalSizeInput.text, out h);
        result &= float.TryParse(verticalSizeInput.text, out v);
        if(h<0 || v<0) result=false;
        return result;
    }

    void ResetComponent()
    {
        nameInput.text = "";
        horizontalSizeInput.text = "";
        verticalSizeInput.text = "";
        typeDropdown.value = 0;
        imageStorage.ResetComponent();
    }

    public void Cancel(){
        ResetComponent();
        selectMain.SetCurrentMode("Select");
    }

    public void Add(){
        if(ValidateForm())
        {
            Sprite[] sprites = imageStorage.GetSpriteList().ToArray();
            selectObjectPanel.AddObject(
                new ObjectPrimitiveData(
                    sprites,
                    nameInput.text,
                    typeDropdown.options[typeDropdown.value].text,
                    float.Parse(horizontalSizeInput.text),    //width
                    float.Parse(verticalSizeInput.text)       //height
                )
            );
            ResetComponent();
            selectMain.SetCurrentMode("Select");
        }
        else{
            horizontalSizeInput.text="Check your";
            verticalSizeInput.text="input";
        }
    }
}
