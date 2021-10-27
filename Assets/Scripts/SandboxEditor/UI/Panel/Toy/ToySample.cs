using GameEditor.Data;
using GameEditor.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Panel
{
    public class ToySample : MonoBehaviour
    {
        // ObjectBuilder objectBuilder;
        [SerializeField] Text typeText, nameText;
        [SerializeField] Image displayImage;
        private Vector2 thumbNailBoxSize;
        private bool initialized = false;
        private ToyData toyData;

        private void Initialize()
        {
            thumbNailBoxSize = displayImage.GetComponent<RectTransform>().rect.size;
        }

        public void SetToySample(ToyData toyData)
        {
            if (!initialized)
                Initialize();
            this.toyData = toyData;
            displayImage.sprite = ImageStorage.GetSprites(toyData.imageData)[0];
            displayImage.GetComponent<RectTransform>().sizeDelta = CalcThumbNailBoxSize();
            typeText.text = toyData.toyRecipe.toyBuildData.toyType.ToString();
            nameText.text = toyData.toyRecipe.toyBuildData.name;
        }    

        private Vector2 CalcThumbNailBoxSize()
        {
            var imageData = toyData.imageData;
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
            // Debug.Log(JsonUtility.ToJson(toyData));
            BuildToyAndPlace();
        }

        private void BuildToyAndPlace()
        {
            var newToy = ToyBuilder.BuildToy(toyData);
            PlaceToyAtViewPoint(newToy);
            Debug.Log("new Toy Placed");
        }

        private static void PlaceToyAtViewPoint(GameObject toy)
        {
            Debug.Log("TOY " + toy.ToString());
            Debug.Log("currentCam " + Camera.main.ToString());
            toy.transform.position = Vector3.Scale(Camera.main.transform.position, new Vector3(1f, 1f, 0f));
        }
    }
}
