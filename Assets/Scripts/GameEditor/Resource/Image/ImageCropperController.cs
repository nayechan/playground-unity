using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
이미지 자르기 도구를 관리하기 위한 스크립트입니다.
*/
public class ImageCropperController : MonoBehaviour
{
    [SerializeField] bool isOval, isZoom;

    private void Awake() {
    }
    public void OpenImageCropper(Image image)
    {
        if(image.sprite == null) return;
        Debug.Log(ImageCropper.Instance);
        ImageCropper.Instance.Show(
            image.sprite.texture,
            ( bool result, Texture originalImage, Texture2D croppedImage ) =>
            {
                Debug.Log(originalImage.width+" "+originalImage.height);
                Debug.Log(croppedImage.width+" "+croppedImage.height);
                if(!result) return;
                image.sprite =
                Sprite.Create(croppedImage, 
                new Rect(
                    0, 
                    0, 
                    croppedImage.width, 
                    croppedImage.height), 
                    new Vector2(0.5f,0.5f)
                );
            },
            settings: new ImageCropper.Settings()
            {
                ovalSelection = isOval,
                autoZoomEnabled = isZoom,
                imageBackground = Color.clear
            }
        );
    }
}