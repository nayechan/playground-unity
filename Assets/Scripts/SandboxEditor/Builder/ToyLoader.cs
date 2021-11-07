using System.Collections.Generic;
using System.Linq;
using SandboxEditor.Data;
using SandboxEditor.Data.Block.Register;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using SandboxEditor.NewBlock;
using Unity.VisualScripting;
using UnityEngine;
using static Tools.Misc;

namespace SandboxEditor.Builder
{
    public class ToyLoader
    {
        private GameObject _newToy;
        private GameObject _toySensor;
        private readonly ToyData _toyData;
        private Texture texture;
        private Dictionary<int, GameObject> _toyIDToyObjectPair;

        public static GameObject BuildToys(string toyJsonData, ref Dictionary<int, GameObject> toyIDToyObjectPair)
        {
            return new ToyLoader(toyJsonData, ref toyIDToyObjectPair).BuildToys();
        }

        private ToyLoader(string toyJsonData, ref Dictionary<int, GameObject> toyIDToyObjectPair)
        {
            _toyData = JsonUtility.FromJson<ToyData>(toyJsonData);
            _toyIDToyObjectPair = toyIDToyObjectPair;
        }
        
        public static GameObject BuildToys(ToyData toyData, ref Dictionary<int, GameObject> toyIDToyObjectPair)
        {
            return new ToyLoader(toyData, ref toyIDToyObjectPair).BuildToys();
        }
        private ToyLoader(ToyData toyData, ref Dictionary<int, GameObject> toyIDToyObjectPair)
        {
            _toyData = toyData;
            _toyIDToyObjectPair = toyIDToyObjectPair;
        }
        
        private GameObject BuildToys()
        {
            BuildToy();
            UpdateIDObjectPair();
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
            AttachObjectSensorGameObject();
            AttachObjectSensorCollider();
            AttachToyPortAndCollisionSensor();
        }
        
        private void AttachObjectSensorGameObject()
        {
            _toySensor = new GameObject("TouchSensor", typeof(ToyBodySensor), typeof(BlockPort));
            SetChildAndParent(_toySensor, _newToy);
            AlignSensorPositionToToy(_toySensor);
        }

        private static void AlignSensorPositionToToy(GameObject toySensor)
        {
            toySensor.transform.localPosition = Vector3.zero;
        }

        private BoxCollider AttachObjectSensorCollider()
        {
            var sensorCollider = _toySensor.AddComponent<BoxCollider>();
            sensorCollider.transform.localScale = Vector3.one;
            sensorCollider.size = Vector3.Scale(sensorCollider.size, new Vector3(1f, 1f, 0.1f));
            sensorCollider.isTrigger = true;
            return sensorCollider;
        }

        private void AttachToyPortAndCollisionSensor()
        {
            var port = _toySensor.GetComponent<BlockPort>();
            InitializeToyPort(port);
            var sensor = _newToy.AddComponent<ToyCollisionSensor>();
            sensor.port = port;
        }


        private void UpdateIDObjectPair()
        {
            _toyIDToyObjectPair.Add(_toyData.gameObjectInstanceID, _newToy);
        }

        private void InitializeToyPort(BlockPort port)
        {
            port.portData = new PortData(0, PortType.ToySender, port);
            port.register = new ToyRegister(_newToy);
        }

        private void BuildToyChildren()
        {
            foreach (var childGameObject in _toyData.childToysData.Select(child => BuildToys(child, ref _toyIDToyObjectPair)))
            {
                childGameObject.transform.parent = _newToy.transform;
            }
        }

    }
}