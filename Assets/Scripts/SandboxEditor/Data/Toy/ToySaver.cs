using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace SandboxEditor.Data.Toy
{
    public class ToySaver : MonoBehaviour
    {
        private ToyData _toyData;

        void Awake()
        {
            _toyData = new ToyData();
        }

        // UpdateToysData
        public static void UpdateToysData(GameObject gameObject)
        {
            gameObject.GetComponent<ToySaver>()?.UpdateToysData();
        }

        private void UpdateToysData()
        {
            UpdateToyData();
            foreach (Transform child in transform)
                child.GetComponent<ToySaver>()?.UpdateToysData();
        }
        
        private void UpdateToyData()
        {
            UpdateSupportedToyComponentsData();
            UpdateChildToyList();
            UpdateGameObjectInstanceID();
        }

        private void UpdateSupportedToyComponentsData()
        {
            var newToyComponentsData = new ToyComponentsDataContainer();
            var toyComponents = GetComponents<Component>();
            foreach(var toyComponent in toyComponents)
                if(ToyComponentData.IsSupportedType(toyComponent))
                    newToyComponentsData.Add(GetUpdatedToyComponentData(toyComponent));
            _toyData.toyComponentsDataContainer = newToyComponentsData;
        }

        private void UpdateChildToyList()
        {
            _toyData.childToysData = new List<ToyData>();
            foreach (Transform child in transform)
            {
                var childToyData = child.GetComponent<ToySaver>()?._toyData;
                if(childToyData != null)
                    _toyData.childToysData.Add(childToyData); 
            }
        }

        private static ToyComponentData GetUpdatedToyComponentData(Component component)
        {
            return ToyComponentData.GetToyComponentDataFromComponent(component);
        }

        private void UpdateGameObjectInstanceID()
        {
            _toyData.gameObjectInstanceID = gameObject.GetInstanceID();
        }

        public void SetToyData(ToyData toyData)
        {
            _toyData = toyData.Clone();
        }

        public ToyData GetToyData()
        {
            return _toyData;
        }

    }
    
}