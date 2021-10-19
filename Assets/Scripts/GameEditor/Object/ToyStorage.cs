using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using GameEditor.Data;

public class ToyStorage : MonoBehaviour
{
    [SerializeField] private List<ToyData> _toysData;
    [SerializeField] private SelectObjectPanelController selectToyPanel;

    private void Awake()
    {
        _toysData = new List<ToyData>();
    }

    public void AddToyData(ToyData toyData)
    {
        _toysData.Add(toyData);
        selectToyPanel.UIRefresh(_toysData);
    }
}
