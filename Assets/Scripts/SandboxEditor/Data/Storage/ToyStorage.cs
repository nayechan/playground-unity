using System.Collections.Generic;
using SandboxEditor.Data.Toy;
using SandboxEditor.UI.Panel.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    public class ToyStorage : MonoBehaviour
    {
        private List<ToyData> _toysData;
        private static ToyStorage _toyStorage;
        public static int Count => _toyStorage._toysData.Count;
        public static IEnumerable<ToyData> ToysData => _toyStorage._toysData.ToArray();

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
        }

        public static List<ToyData> GetToysData()
        {
            return GetSingleton()._toysData;
        }


    }
}
