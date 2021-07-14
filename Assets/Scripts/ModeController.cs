using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public Button btnAdd;
    public Button btnDel;
    public Button btnMove;
    public TouchControll camD;

    void Start(){
        btnMove.onClick.AddListener(() => camD.SetTouchMode(Enums.TouchMode.CameraDrag));
        btnAdd.onClick.AddListener(() => camD.SetTouchMode(Enums.TouchMode.TileAdd));
        btnDel.onClick.AddListener(() => camD.SetTouchMode(Enums.TouchMode.TileDel));
    }

}
