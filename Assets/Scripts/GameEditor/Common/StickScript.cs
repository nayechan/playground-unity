using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StickScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image _bgImg;
    private Image _joystickImg;
    private Vector2 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        inputVector = Vector2.zero;
        _bgImg = transform.GetChild(0).GetComponent<Image>();
        _joystickImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData ped){
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(),
        ped.position, ped.pressEventCamera,out pos)){            
            pos.x = (pos.x / _bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x*2, pos.y*2);
            inputVector = (inputVector.magnitude > 1.0f)?inputVector.normalized:inputVector;
            // Debug.Log(inputVector);

            _joystickImg.rectTransform.anchoredPosition = new Vector2(
                inputVector.x * _bgImg.rectTransform.sizeDelta.x/3, 
                inputVector.y * _bgImg.rectTransform.sizeDelta.y/3);
        }

    }
    public void OnPointerDown(PointerEventData ped){
        OnDrag(ped);
    }
    public void OnPointerUp(PointerEventData ped){
        _joystickImg.rectTransform.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }

    public float OutPutX(){
        return inputVector.x;
    }

    public Vector2 GetInputVector(){
        return inputVector;
    }
}
