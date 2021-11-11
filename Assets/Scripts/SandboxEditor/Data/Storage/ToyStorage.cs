using System.Collections.Generic;
using System.Linq;
using SandboxEditor.Data.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    public class ToyStorage : MonoBehaviour
    {
        private ToysData _toysData;
        private static ToyStorage _toyStorage;
        public static int Count => _toyStorage._toysData.Count;
        public static ToysData ToysData => _toyStorage._toysData;

        private void Awake()
        {
            _toyStorage = this;
            _toysData = new ToysData();
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

        public static ToysData GetToysData()
        {
            return GetSingleton()._toysData;
        }
    }
}
