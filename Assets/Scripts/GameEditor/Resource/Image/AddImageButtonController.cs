using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이미지 에디터에서 이미지 추가 버튼을 관리하느 스크립트입니다.
public class AddImageButtonController : MonoBehaviour
{
    public PanelSwitcher imagePanelSwitcher;
    public GameObject imageEditor;

    public void OnButton()
    {
        imagePanelSwitcher.DeactivateChildExceptIgnoreListAndActivateTarget(imageEditor.transform);
    }
}
