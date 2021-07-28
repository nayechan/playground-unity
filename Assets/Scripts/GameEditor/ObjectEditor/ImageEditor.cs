using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//namespace ImageCropperNamespace
//{
	public class ImageEditor : MonoBehaviour
	{
        [SerializeField] private ImageStorage imageStorage;
        [SerializeField] bool isOval, isZoom;

        private void Awake() {
        }
        public void OpenImageCropper()
        {
            Sprite sprite = imageStorage.GetCurrentSprite();
            if(sprite == null) return;
            Debug.Log(ImageCropper.Instance);
            ImageCropper.Instance.Show(
                imageStorage.GetCurrentSprite().texture,
                ( bool result, Texture originalImage, Texture2D croppedImage ) =>
                {
                    Debug.Log(originalImage.width+" "+originalImage.height);
                    Debug.Log(croppedImage.width+" "+croppedImage.height);
                    if(!result) return;
                    imageStorage.SetCurrentSprite(
                        Sprite.Create(croppedImage, 
                        new Rect(
                            0, 
                            0, 
                            croppedImage.width, 
                            croppedImage.height), 
                            new Vector2(0.5f,0.5f)
                        )
                    );
                    imageStorage.UpdateDisplay();
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
//}