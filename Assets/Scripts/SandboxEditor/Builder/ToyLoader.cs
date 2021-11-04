using System.Linq;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.NewBlock;
using Tools;
using UnityEngine;

namespace SandboxEditor.Builder
{
    public class ToyLoader
    {
        private GameObject _newToy;
        private readonly ToyData _toyData;
        private Texture texture;

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
            CreateToyAndAttachToySaver();
            AttachSpriteRendererByImageData();
            AttachToyComponent();
            return _newToy;
        }

        private void CreateToyAndAttachToySaver()
        {
            _newToy = new GameObject("ToyOnBuild", typeof(ToySaver));
            _newToy.GetComponent<ToySaver>().SetToyData(_toyData);
        }

        private void AttachSpriteRendererByImageData()
        {
            if (DontHaveImage()) return;
            _toyData.imageData.CreateSpriteRendererAndLoadSprite(_newToy);
        }


        private void AttachToyComponent()
        {
            foreach (var toyComponentData in _toyData.toyComponentsDataContainer.GetToyComponentsData())
                toyComponentData.AddDataAppliedToyComponent(_newToy);
            AdjustToyColliderSize();
            AdjustTransformSizeByImageData();
            AttachObjectSensor();
        }

        private void AdjustToyColliderSize()
        {
            var collider2D = _newToy.GetComponent<Collider2D>();
            if (collider2D == null) return;
            switch (collider2D)
            {
                case CircleCollider2D circleCollider2D :
                    circleCollider2D.radius = _toyData.GetToySpriteBoundSize().x / 2;
                    break;
                case BoxCollider2D boxCollider2D  :
                    boxCollider2D.size = _toyData.GetToySpriteBoundSize();
                    break;
            }
        }

        private void AdjustTransformSizeByImageData()
        {
            if (DontHaveImage()) return;
            texture = _newToy.GetComponent<SpriteRenderer>().sprite.texture;
            _newToy.transform.localScale = BuildTransformScale();
        }
        private bool DontHaveImage()
        {
            return _toyData.imageData.GetRelativeImagePaths().Count == 0;
        }

        private Vector3 BuildTransformScale()
        {
            return _toyData.imageData.GetIsRelativeSize() ? BuildRelativeScale() : BuildAbsoluteScale();
        }

        private Vector3 BuildAbsoluteScale()
        {
            var xMultiplier = _toyData.imageData.GetWidth() / texture.width * 100f;
            var yMultiplier = _toyData.imageData.GetHeight()/texture.height * 100f;
            return new Vector3(xMultiplier, yMultiplier, 1f);
        }
        private Vector3 BuildRelativeScale()
        {
            var xMultiplier = _toyData.imageData.GetWidth() / texture.width * 100f;
            return new Vector3(xMultiplier, xMultiplier, 1f);
        }

        private void AttachObjectSensor()
        {
            if (_newToy.GetComponent<SpriteRenderer>() == null) return;
            var toySensor = AttachObjectSensorGameObject();
            AttachObjectSensorCollider(toySensor);
        }
        
        private GameObject AttachObjectSensorGameObject()
        {
            var toySensor = new GameObject("TouchSensor", typeof(ToyBodySensor));
            Misc.SetChildAndParent(toySensor, _newToy);
            AlignSensorPositionToToy(toySensor);
            return toySensor;
        }

        private static void AlignSensorPositionToToy(GameObject toySensor)
        {
            toySensor.transform.localPosition = Vector3.zero;
        }

        private static BoxCollider AttachObjectSensorCollider(GameObject toySensor)
        {
            var sensorCollider = toySensor.AddComponent<BoxCollider>();
            sensorCollider.transform.localScale = Vector3.one;
            sensorCollider.size = Vector3.Scale(sensorCollider.size, new Vector3(1f, 1f, 0.1f));
            sensorCollider.isTrigger = true;
            return sensorCollider;
        }


        private void BuildToyChildren()
        {
            foreach (var child in _toyData.childToysData.Select(BuildToys))
                child.transform.parent = _newToy.transform;
        }

    }
}