using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Storage;
using SandboxEditor.Data.Toy;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxEditor.UI.Panel.Toy
{
    public class ToySample : MonoBehaviour
    {
        // ObjectBuilder objectBuilder;
        [SerializeField] Text typeText, nameText;
        [SerializeField] UnityEngine.UI.Image displayImage;
        private Vector2 thumbNailBoxSize;
        private bool initialized = false;
        private ToyData _toyData;

        private void Initialize()
        {
            thumbNailBoxSize = displayImage.GetComponent<RectTransform>().rect.size;
        }

        public void SetToySample(ToyData toyData)
        {
            if (!initialized)
                Initialize();
            _toyData = toyData.Clone();
            displayImage.sprite = ImageStorage.GetSprites(_toyData.imageData)[0];
            displayImage.GetComponent<RectTransform>().sizeDelta = CalcThumbNailBoxSize();
            typeText.text = _toyData.toyRecipe.toyBuildData.toyType.ToString();
            nameText.text = _toyData.toyRecipe.toyBuildData.name;
        }    

        private Vector2 CalcThumbNailBoxSize()
        {
            var imageData = _toyData.imageData;
            var toyHeight = imageData.GetHeight();
            var toyWidth = imageData.GetWidth();
            if(imageData.GetIsRelativeSize())
            {
                if(displayImage.sprite != null)
                {
                    toyHeight *= displayImage.sprite.texture.height;
                    toyWidth *= displayImage.sprite.texture.width;
                }
            }
            if(toyHeight > toyWidth)
            {
                toyWidth = (toyWidth/toyHeight) * thumbNailBoxSize.x;
                toyHeight = thumbNailBoxSize.y;
            }
            else
            {
                toyHeight = (toyHeight/toyWidth) * thumbNailBoxSize.y;
                toyWidth = thumbNailBoxSize.x;
            }
            return new Vector2(toyWidth,toyHeight);
        }
        public void WhenSampleClicked()
        {
            BuildToyAndPlace();
        }

        private void BuildToyAndPlace()
        {
            var newToy = Sandbox.BuildToyOnSandbox(_toyData);
            PlaceToyAtViewPoint(newToy);
            Debug.Log("new Toy Placed");
        }

        private static void PlaceToyAtViewPoint(GameObject toy)
        {
            if (Camera.main is null) return;
            toy.transform.position = Vector3.Scale(Camera.main.transform.position, new Vector3(1f, 1f, 0f));
            Debug.Log(toy.transform.position);
        }
    }
}
