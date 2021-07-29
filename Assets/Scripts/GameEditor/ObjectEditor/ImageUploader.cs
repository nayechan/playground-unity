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
            WebGLFileUploadManager.onFileUploaded += OnFileUploadedWebGL;
        }
        else
        {
            bool isSuccess = true;
            SimpleFileBrowser.FileBrowser.ShowSaveDialog(
                (string [] paths)=>{
                    OnFileUploaded(paths);
                },
                ()=>{
                    isSuccess = false;
                },
                SimpleFileBrowser.FileBrowser.PickMode.Files,
                true
            );
        }


    }

    private void OnFileUploadedWebGL(UploadedFileInfo[] result)
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
                Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
                Debug.Log(s.pivot);
                sprites.Add(s);
                
            }
        }

        imageStorage.SetSpriteList(sprites);
    }

    public void OnFileUploaded(string [] paths)
    {
        List<Sprite> sprites = new List<Sprite>();
        if(paths.Length == 0) {
            Debug.Log("File upload Error!");
        }else{
            Debug.Log("File upload success! (paths.Length: " + paths.Length + ")");
        }

        foreach(string file in paths){
            string extension = Path.GetExtension(file);
            Debug.Log(extension);
            if(extension == ".png" || extension == ".jpg" || extension == ".jpeg"){

                Debug.Log("file.filePath: " + file + " exists:" + File.Exists(file));

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

                byte[] byteArray = File.ReadAllBytes(file);
                
                texture.LoadImage(byteArray);
                
                Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f));
                Debug.Log(s.pivot);
                sprites.Add(s);
                
            }
        }

        imageStorage.SetSpriteList(sprites);
    }

    private void OnDestroy() {
        if(Application.platform == RuntimePlatform.WebGLPlayer
        && !Application.isEditor)
        {
            WebGLFileUploadManager.onFileUploaded -= OnFileUploadedWebGL;
        }
        else
        {

        }
    }
}
