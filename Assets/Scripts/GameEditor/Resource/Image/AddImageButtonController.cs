using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 이미지 에디터에서 이미지 추가 버튼을 관리하느 스크립트입니다.
public class AddImageButtonController : MonoBehaviour
{
    public PanelSwitcher imagePanelSwitcher;
    public GameObject imageEditor;

    public void SetField(PanelSwitcher imagePanelSwitcher, GameObject imageEditor)
    {
        this.imagePanelSwitcher = imagePanelSwitcher;
        this.imageEditor = imageEditor;
        gameObject.GetComponent<Button>().onClick.AddListener(
            ()=>{imagePanelSwitcher.DeactivateChildExceptIgnoreListAndActivateTarget(imageEditor.transform);});
    }
}
