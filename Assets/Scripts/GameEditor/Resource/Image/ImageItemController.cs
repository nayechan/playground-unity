using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
이미지 선택 창 내 각각의 이미지 요소들을 관리하는 스크립트
*/
public class ImageItemController : MonoBehaviour {

    [SerializeField] private Text titleText, typeText;
    [SerializeField] private Image image;
    bool isImageLoaded = false;
    float defaultWidth, defaultHeight;
    ImageData _imageData;
    public delegate void OnClick(ImageData imageData);
    public OnClick onClick;

    //이미지가 로드되었을 때 까지 작업 (크기 계산 등)을 지연
    public void Awake()
    {
        StartCoroutine("WaitUntilImageLoad");
    }

    //이미지 데이터 설정
    public void SetImageData(ImageData data)
    {
        _imageData = data;
        if(isImageLoaded)
            RefreshUI();
    }  

    //UI 리프레시
    public void RefreshUI()
    {
        image.sprite = ImageStorage.GetSprites(_imageData)[0];

        float w = _imageData.GetHSize();
        float h = _imageData.GetVSize();


        // Debug.Log(w+" "+h);
        if(_imageData.GetIsRelativeSize())
        {
            if(image.sprite != null)
            {
                w *= image.sprite.texture.width;
                h *= image.sprite.texture.height;
            }
            
        }
        
        // Debug.Log(w+" "+h);

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
        
        // Debug.Log(w+" "+h);

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);

        // Debug.Log(_imageData.GetTitle());
        titleText.text = _imageData.GetTitle();
        typeText.text = _imageData.GetIsUsingSingleImage() ? "Single" : "Multiple";
    }

    //이미지가 로드될 때까지 작업을 지연시키기 위한 코루틴
    IEnumerator WaitUntilImageLoad()
    {
        while(image.GetComponent<RectTransform>().rect.width == 0)
        {
            yield return null;
        }

        defaultWidth = image.GetComponent<RectTransform>().rect.width;
        defaultHeight = image.GetComponent<RectTransform>().rect.height;

        // Debug.Log(defaultWidth+" "+defaultHeight);

        isImageLoaded = true;
        RefreshUI();

    }

    public void ExecuteOnClick()
    {
        // onClick(_imageData);
    }

}