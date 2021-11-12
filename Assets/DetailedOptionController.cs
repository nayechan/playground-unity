using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SandboxEditor.Data.Sandbox;
using System.Linq;
using MainPage.Panel;
using UnityEngine.UI;

public class DetailedOptionController : MonoBehaviour, IDeselectHandler
{
    [SerializeField] LibraryCardOptionController cardOptionController;
    [SerializeField] LibraryPanelController libraryPanel;
    private bool mouseIsOver = false;
 
    private void OnEnable() {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnDeselect(BaseEventData eventData) {
    //Close the Window on Deselect only if a click occurred outside this panel
        if(!RectTransformUtility.RectangleContainsScreenPoint(
            GetComponent<RectTransform>(),
            Input.GetTouch(0).position
        ))
        {
            Debug.Log("asdf");
            gameObject.SetActive(false);
        }
    }

    public void SelectThis(){EventSystem.current.SetSelectedGameObject(gameObject);}

    public void RemoveOperation()
    {
        if(cardOptionController.GetIsLocal())
        {
            SandboxChecker.RemoveLocalSandboxWithGameID(
                cardOptionController.GetGameID()
            );
        }
        else
        {
            SandboxChecker.RemoveRemoteSandboxWithGameID(
                cardOptionController.GetGameID()
            );
        }

        libraryPanel.UpdateComponent();
    }
}
