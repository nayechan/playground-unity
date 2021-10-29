using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace GameEditor.Data
{
    public class ToyLoader
    {
        private GameObject _newToy;
        private readonly ToyData _toyData;

        public static GameObject BuildToys(string toyJsonData)
        {
            return new ToyLoader(toyJsonData).BuildToys();
        }

        private ToyLoader(string toyJsonData)
        {
            _toyData = JsonUtility.FromJson<ToyData>(toyJsonData);
        }
        
        public static GameObject BuildToys(ToyData toyData)
        {
            return new ToyLoader(toyData).BuildToys();
        }
        private ToyLoader(ToyData toyData)
        {
            _toyData = toyData;
        }
        
        private GameObject BuildToys()
        {
            BuildToy();
            BuildToyChildren();
            return _newToy;
        }
        
        private GameObject BuildToy()
        {
            CreateToyAndAddToySaver();
            AttachComponents();
            ApplyImageData();
            return _newToy;
        }

        private void CreateToyAndAddToySaver()
        {
            _newToy = new GameObject("ToyOnBuild", typeof(ToySaver));
            _newToy.GetComponent<ToySaver>().SetToyData(_toyData);
        }

        private void AttachComponents()
        {
            foreach (var toyComponentData in _toyData.toyComponentsDataContainer.GetToyComponentsData())
                toyComponentData.AddDataAppliedToyComponent(_newToy);
        }
        
        private void ApplyImageData()
        {
            if (DontHaveImage()) return;
            _toyData.imageData.BuildAndAttachSpriteRendererAndAdjustScale(_newToy);
        }

        private bool DontHaveImage()
        {
            return _toyData.imageData.GetRelativeImagePaths().Count == 0;
        }


        private void BuildToyChildren()
        {
            foreach (var child in _toyData.childToysData.Select(BuildToys))
                child.transform.parent = _newToy.transform;
        }

    }
}