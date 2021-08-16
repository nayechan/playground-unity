using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlockController : MonoBehaviour
{
    /* 씬 안에 있는 Block들 및 SignalLine 들의 정보를 저장하고 관리하는 클래스입니다*/
    public GameObject signalLineFab, guideText;
    private static EventBlockController _ebc;
    private TouchSensor_BlockPort _selectedOutputPort;
    private GameObject _signalLines, _blocks, _objects, _tiles;
    private mode _mode;
    private enum mode
    {
        Editing,
        SignalLineConnecting,
        ComponentLineConnecting
    };

    void Start()
    {
        _mode = mode.Editing;
        _ebc = this;
        _signalLines = new GameObject("SignalLines");
        _blocks = new GameObject("Blocks");
    }

    static public EventBlockController GetEBC()
    {
        return _ebc;
    }

    public bool PortTouched(TouchSensor_BlockPort port)
    {
        // 연결작업이 진행되었을 시 true 반환. 에디터 모드를 LineConnecting으로 변경.
        if (port.portType == "Output" && _mode == mode.Editing)
        {
            _selectedOutputPort = port;
            _mode = mode.SignalLineConnecting;
            guideText.SetActive(true);
            return true;
        }
        if (port.portType == "Input" && _mode == mode.SignalLineConnecting)
        {
            ConnectLine(port, "Signal");
            _mode = mode.Editing;
            guideText.SetActive(false);
            return true;
        }
        if (port.portType == "PropertyOutput" && _mode == mode.Editing)
        {
            _selectedOutputPort = port;
            _mode = mode.ComponentLineConnecting;
            guideText.SetActive(true);
        }
        if (port.portType == "PropertyInput" && _mode == mode.ComponentLineConnecting)
        {
            ConnectLine(port, "Component");
            _mode = mode.Editing;
            guideText.SetActive(false);
            return true;
        }
        return false;
    }

    public void ConnectLine(TouchSensor_BlockPort inputPort, string type)
    {
        // 선 객체를 만든다.
        GameObject LineObj = Instantiate(signalLineFab, Vector3.zero, Quaternion.identity, _signalLines.transform);
        SignalLine signalLine;
        if (type == "Signal") signalLine = LineObj.AddComponent<SignalLine>();
        else signalLine = LineObj.AddComponent<ComponentLine>();
        signalLine.SetLine(_selectedOutputPort, inputPort);
        signalLine.ReRendering();
        _selectedOutputPort.body.AddLine(signalLine);
        inputPort.body.AddLine(signalLine);
        Debug.Log("New SignalLine constructed");
    }

    public void GenerateBlockInstance(GameObject blockFab)
    {
        Vector3 setPos = Vector3.Scale(Camera.main.transform.position, new Vector3(1f,1f,0));
        GenerateBlockInstance(blockFab, setPos);
    }

    public void GenerateBlockInstance(GameObject blockFab, Vector3 pos)
    {
        GenerateBlockInstance(blockFab, pos, null);
    }

    public void GenerateBlockInstance(GameObject blockFab, Vector3 pos, GameObject attached)
    {
        GameObject ins = Instantiate(blockFab, pos - pos.z * Vector3.forward, Quaternion.identity, _blocks.transform);
        ins.GetComponent<BlockProperty>()._attachedObject = attached;
    }
    public void DestroyBlock(BlockProperty block)
    {
        Destroy(block.gameObject);
    }

    public void CancelLineConnecting()
    {
        _mode = mode.Editing;
        guideText.SetActive(false);
        _selectedOutputPort = null;
    }

    public void PlaySart()
    {
        // 시작 초기 환경관련. 추후 다른 컴포넌트서 설정 가능
        Physics2D.gravity = Vector2.zero;
        // 모든 SignalLine 컴포넌트를 활성화
        foreach (Transform line in _signalLines.GetComponentInChildren<Transform>())
        {
            line.GetComponent<SignalLine>().PlayStart();
        }
        // 모든 Tile 오브젝트에 컴포넌트(collider, rigidBody 추가)
        if (_tiles)
        {
            foreach (Transform t in _tiles.GetComponentsInChildren<Transform>())
            {
                GameObject obj = t.gameObject;
                Rigidbody2D body = obj.AddComponent<Rigidbody2D>();
                body.bodyType = RigidbodyType2D.Static;
                Collider2D col = obj.GetComponent<BoxCollider2D>();
                if (col == null) { col = obj.AddComponent<BoxCollider2D>(); }
                PhysicsMaterial2D mat = new PhysicsMaterial2D("Bouncer");
                mat.bounciness = 1f;
                mat.friction = 0f;
                col.sharedMaterial = mat;
            }
        }
        // 모든 시작시 동작하는 블럭 효과를 수행 (모든 BlockProperty 블록의 특정함수 수행) (예를 들면 컴포넌트를 부여)
        foreach (BlockProperty bp in GameObject.FindObjectsOfType<BlockProperty>())
        {
            bp.PlayStart();
            foreach (Transform childT in bp.transform)
            {
                childT.gameObject.SetActive(false);
            }
        }
        // 타일의 알파값, 오브젝트 알파값 재조정
        TemporaryGameEditorDataManager tge = TemporaryGameEditorDataManager.GetDM();
        if (tge)
        {
            tge.FetchOurObjects(new GameObject("_objs"));
            tge.FetchOurTiles(new GameObject("_tiles"));
        }
        // 인풋 카메라 재조정
        // UserInputController.GetUserInputController().ResetCamera();
        // 툴 바, Raycasting disable, 그리드 disable
        GameObject.Find("Canvas").SetActive(false);
        GameObject.FindObjectOfType<TouchInputDeliverer>().enabled = false;
        GameObject.Find("Grid").SetActive(false);
    }

    public void SetRoots(GameObject objs, GameObject tiles)
    {
        _objects = objs;
        _tiles = tiles;
    }
}
