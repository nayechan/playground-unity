using System.Collections;
using GameEditor.Data;
using GameEditor.ObjectEditor;
using GameEditor.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor.Panel
{
    public class ToySample : MonoBehaviour
    {
        ObjectBuilder objectBuilder;
        [SerializeField] Text typeText, nameText;
        float defaultWidth = 0, defaultHeight = 0;
        bool isImageLoaded = false;
        [SerializeField] Image displayImage;

        private void Awake() {
        
            defaultWidth = displayImage.GetComponent<RectTransform>().rect.width;
            defaultHeight = displayImage.GetComponent<RectTransform>().rect.height;

            if(objectBuilder == null)
            {
                objectBuilder = GameObject.Find("ObjectBuilder").GetComponent<ObjectBuilder>();
            }
        
            StartCoroutine("WaitUntilImageLoad");
        }

        public void SetDisplayInstanceData(ToyData toyData)
        {
            displayImage.sprite = ImageStorage.GetSprites(toyData.imageData)[0];
            typeText.text = toyData.toyBuildData.toyType.ToString();
            nameText.text = toyData.toyBuildData.name;
        }    

        public void RefreshSampleUI()
        {
            displayImage.sprite = ImageStorage.GetSprites(GetComponent<ToyDataContainer>().ImageData)[0];

            float h = GetComponent<ToyDataContainer>().ImageData.GetHeight();
            float w = GetComponent<ToyDataContainer>().ImageData.GetWidth();


            Debug.Log(h+" "+w);
            if(GetComponent<ToyDataContainer>().ImageData.GetIsRelativeSize())
            {
                if(displayImage.sprite != null)
                {
                    h *= displayImage.sprite.texture.height;
                    w *= displayImage.sprite.texture.width;
                }
            
            }
        
            Debug.Log(h+" "+w);

            if(h > w)
            {
                w = (w/h) * defaultWidth;
                h = defaultHeight;
            }
            else
            {
                h = (h/w) * defaultHeight;
                w = defaultWidth;
            }
        
            Debug.Log(h+" "+w);

            displayImage.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);


            typeText.text = GetComponent<ToyDataContainer>().ToyBuildData.toyType.ToString();
            nameText.text = GetComponent<ToyDataContainer>().ToyBuildData.name;
        }

        IEnumerator WaitUntilImageLoad()
        {
            while(displayImage.GetComponent<RectTransform>().rect.width == 0)
            {
                yield return null;
            }
            defaultWidth = displayImage.GetComponent<RectTransform>().rect.width;
            defaultHeight = displayImage.GetComponent<RectTransform>().rect.height;
            isImageLoaded = true;
            RefreshSampleUI();

        }

        public void WhenSampleClicked()
        {
            objectBuilder.SetCurrentToyData(GetComponent<ToyDataContainer>().toyData);
        }
    }
}
