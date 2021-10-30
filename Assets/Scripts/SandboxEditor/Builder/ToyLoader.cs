using System.Linq;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
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
            CreateToyAndAddToySaver();
            AttachSpriteRendererByImageData();
            AttachComponents();
            AdjustColliderSize();
            AdjustTransformSizeByImageData();
            return _newToy;
        }

        private void CreateToyAndAddToySaver()
        {
            _newToy = new GameObject("ToyOnBuild", typeof(ToySaver));
            _newToy.GetComponent<ToySaver>().SetToyData(_toyData);
        }

        private void AttachSpriteRendererByImageData()
        {
            if (DontHaveImage()) return;
            _toyData.imageData.CreateSpriteRendererAndLoadSprite(_newToy);
        }


        private void AttachComponents()
        {
            foreach (var toyComponentData in _toyData.toyComponentsDataContainer.GetToyComponentsData())
                toyComponentData.AddDataAppliedToyComponent(_newToy);
        }

        private void AdjustColliderSize()
        {
            var collider2D = _newToy.GetComponent<Collider2D>();
            switch (collider2D)
            {
                case CircleCollider2D circleCollider2D :
                    circleCollider2D.radius = GetToySpriteBoundSize().x / 2;
                    break;
                case BoxCollider2D boxCollider2D  :
                    boxCollider2D.size = GetToySpriteBoundSize();
                    break;
            }
        }
        
        private Vector2 GetToySpriteBoundSize()
        {
            return ImageStorage.GetSprites(_toyData.imageData)[0].bounds.size;
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


        private void BuildToyChildren()
        {
            foreach (var child in _toyData.childToysData.Select(BuildToys))
                child.transform.parent = _newToy.transform;
        }

    }
}