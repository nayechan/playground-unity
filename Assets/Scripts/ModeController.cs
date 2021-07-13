using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeController : MonoBehaviour
{
    public Button btnAdd;
    public Button btnDel;
    public Button btnMove;
    enum Mode{
        TileAdd,
        TileDelete,
        CameraMove
    }
    private Mode _mode;
    // Start is called before the first frame update

    public void onMoveMode(){
        _mode = Mode.CameraMove;
        Debug.Log("on Move mode");
    }

    void onTileDelete(){
        _mode = Mode.TileDelete;
    }
    void onTileAdd(){
        _mode = Mode.TileAdd;
    }

}
