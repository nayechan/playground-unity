using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateObjectPanelController : MonoBehaviour
{
    [SerializeField] ImageStorage imageStorage;
    [SerializeField] SelectMainController selectMain;
    [SerializeField] InputField nameInput, horizontalSizeInput, verticalSizeInput;
    [SerializeField] Dropdown typeDropdown;

    public bool ValidateForm()
    {
        return false;
    }

    void ResetComponent()
    {
        
    }

    public void Cancel(){
        
    }

    public void Add(){
        
    }
}
