using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebGLFileUploader;
using System.IO;

public class ImageUploader : MonoBehaviour
{
    [SerializeField] private ImageStorage imageStorage;
    public void OnUploadButtonClicked()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer
        && !Application.isEditor)
        {
            WebGLFileUploadManager.SetAllowedFileName("\\.(png|jpe?g)$");
            WebGLFileUploadManager.SetImageEncodeSetting(false);
            WebGLFileUploadManager.PopupDialog();
            WebGLFileUploadManager.onFileUploaded += OnFileUploaded;
        }


    }

    private void OnFileUploaded(UploadedFileInfo[] result)
    {
        List<Sprite> sprites = new List<Sprite>();
        if(result.Length == 0) {
            Debug.Log("File upload Error!");
        }else{
            Debug.Log("File upload success! (result.Length: " + result.Length + ")");
        }

        foreach(UploadedFileInfo file in result){
            if(file.isSuccess){

                Debug.Log("file.filePath: " + file.filePath + " exists:" + File.Exists(file.filePath));

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

                byte[] byteArray = File.ReadAllBytes(file.filePath);
                
                texture.LoadImage(byteArray);
                if(System.IO.Path.GetExtension(file.filePath) == "png")
                {
                    Texture2D texturePNG = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
                    texturePNG.SetPixels(texture.GetPixels());
                    texturePNG.Apply();
                    Sprite s = Sprite.Create(texturePNG, 
                    new Rect(
                        0, 
                        0, 
                        texturePNG.width, 
                        texturePNG.height), 
                        Vector2.zero, 
                        1f
                    );
                    sprites.Add(s);
                }
                else
                {
                    Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 1f);
                    sprites.Add(s);
                }
                
            }
        }

        imageStorage.GetSpriteList(sprites);
    }

    private void OnDestroy() {
        WebGLFileUploadManager.onFileUploaded -= OnFileUploaded;
    }
}
