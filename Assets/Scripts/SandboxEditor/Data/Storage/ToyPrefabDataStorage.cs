using System.Collections.Generic;
using System.Linq;
using SandboxEditor.Data.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    public class ToyPrefabDataStorage : MonoBehaviour
    {
        private ToysData _toysData;
        private static ToyPrefabDataStorage _toyPrefabDataStorage;
        public static int Count => _toyPrefabDataStorage._toysData.Count;
        public static ToysData ToysData => _toyPrefabDataStorage._toysData;

        private void Awake()
        {
            _toyPrefabDataStorage = this;
            _toysData = new ToysData();
        }

        private static ToyPrefabDataStorage GetSingleton()
        {
            return _toyPrefabDataStorage;
        }

        public static void AddToyRecipeData(ToyData toyData)
        {
            GetSingleton()._AddToyRecipeData(toyData);
        }

        private void _AddToyRecipeData(ToyData toyData)
        {
            _toysData.Add(toyData);
        }
    }
}
