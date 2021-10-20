using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using GameEditor.Data;

public class ToyStorage : MonoBehaviour
{
    [SerializeField] private List<ToyData> _toysData;
    [SerializeField] private SelectToySamplePanelController selectToyPanel;
    private static ToyStorage _toyStorage;

    private void Awake()
    {
        SetSingletonIfUnset();
        _toysData = new List<ToyData>();
    }

    private void SetSingletonIfUnset()
    {
        if(_toyStorage == null)
        {
            _toyStorage = this;
        }
    }

    private static ToyStorage GetSingleton()
    {
       return _toyStorage;
    }

    public static void AddToyData(ToyData toyData)
    {
        GetSingleton()._AddToyData(toyData);
    }

    private void _AddToyData(ToyData toyData)
    {
        _toysData.Add(toyData);
        selectToyPanel.UIRefresh(_toysData);
    }

    public static List<ToyData> GetToysData()
    {
        return GetSingleton()._toysData;
    }


}
