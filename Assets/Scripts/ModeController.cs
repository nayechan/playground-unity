using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public Button btnAdd;
    public Button btnDel;
    public Button btnMove;
    public TouchControll touchContorller;

    void Start(){
        btnMove.onClick.AddListener(() => touchContorller.SetTouchMode(Enums.TouchMode.CameraDrag));
        btnAdd.onClick.AddListener(() => touchContorller.SetTouchMode(Enums.TouchMode.TileAdd));
        btnDel.onClick.AddListener(() => touchContorller.SetTouchMode(Enums.TouchMode.TileDel));
    }

}
