using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjectPanelController : MonoBehaviour
{
    [SerializeField] ImageStorage imageStorage;
    [SerializeField] ObjectDataManager objectDataManager;
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
            string[] spritePaths = imageStorage.GetSpritePathList().ToArray();
            ObjectPrimitiveData data = new ObjectPrimitiveData(
                sprites,
                spritePaths,
                nameInput.text,
                typeDropdown.options[typeDropdown.value].text,
                float.Parse(horizontalSizeInput.text),    //width
                float.Parse(verticalSizeInput.text)       //height
            );
            objectDataManager.AddObject(data);
            Debug.Log(data.GetObjectName());
            ResetComponent();
            selectMain.SetCurrentMode("Select");
        }
        else{
            horizontalSizeInput.text="Check your";
            verticalSizeInput.text="input";
        }
    }
}
