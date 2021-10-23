using System.Collections.Generic;
using GameEditor.Data;
using GameEditor.Object;
using UnityEngine;

namespace GameEditor.Storage
{
    public class ToyStorage : MonoBehaviour
    {
        [SerializeField] private List<ToyData> _toysData;
        [SerializeField] private ToySampleSelectPanel toySampleSelectToyPanel;
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
            toySampleSelectToyPanel.RefreshPanel(_toysData);
        }

        public static List<ToyData> GetToysData()
        {
            return GetSingleton()._toysData;
        }


    }
}
