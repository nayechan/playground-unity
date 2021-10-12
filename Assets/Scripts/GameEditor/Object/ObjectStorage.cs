using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using GameEditor.Data;

public class ObjectStorage : MonoBehaviour
{
    [SerializeField] private List<ObjectData> _objectDatas;
    [SerializeField] private SelectObjectPanelController selectObjectPanel;

    private void Awake()
    {
        _objectDatas = new List<ObjectData>();
    }

    public void AddObjectData(ObjectData data)
    {
        _objectDatas.Add(data);
        selectObjectPanel.UIRefresh(_objectDatas);
    }
}
