using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이미지 에디터에서 이미지 추가 버튼을 관리하느 스크립트입니다.
public class AddImageButtonController : MonoBehaviour
{
    Transform imagePanel;
    [SerializeField] string target;
    private void Awake() {
        imagePanel = transform.parent.parent.parent.parent;
        //Dirty Code 주의 (추후 변경바람)
    }    
    
    public void OnButton()
    {
        imagePanel.GetComponent<PanelSwitcher>().OpenPanel(imagePanel.Find(target));
    }
}
