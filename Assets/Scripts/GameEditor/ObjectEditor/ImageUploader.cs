using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebGLFileUploader;
using System.IO;

/*
ImageUploader

설명 : 
플랫폼에 따라 WebGLFileUploaderManager 외부 라이브러리 또는 SimpleFileBrowser로 부터 이미지를 받아와서
ImageStorage 클래스에 이미지를 지정합니다.

외부 참조 라이브러리 :
WebGLFileUploader
SimpleFileBrowser

비고 :
*/

public class ImageUploader : MonoBehaviour
{
    [SerializeField] private ImageStorage imageStorage;

    public void OnUploadButtonClicked()
    {
        // WebGL 플랫폼의 경우
        if(Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
        {

            // 선택 가능한 파일 형식을 지정합니다.
            WebGLFileUploadManager.SetAllowedFileName("\\.(png|jpe?g)$");

            // 이미지의 인코딩여부를 지정합니다.
            WebGLFileUploadManager.SetImageEncodeSetting(false);

            // 이미지 선택 창을 엽니다.
            WebGLFileUploadManager.PopupDialog();

            // 파일이 선택된 후의 동작을 지정합니다.
            WebGLFileUploadManager.onFileUploaded += OnFileUploadedWebGL;
        }
        
        // 기타 플랫폼의 경우
        else
        {
            bool isSuccess = true;
            
            // 파일을 선택하는 창을 엽니다. 선택시 첫번째 delegate, Cancel시 두번째 delegate를 실행합니다.
            // 3번째 인자는 선택 모드에 대한 설정이고, 4번째 인자는 다중 선택 여부에 대한 설정입니다.
            SimpleFileBrowser.FileBrowser.ShowSaveDialog(
                (string [] paths) => { OnFileUploaded(paths); },
                () => { isSuccess = false; },
                SimpleFileBrowser.FileBrowser.PickMode.Files,
                true
            );

            if(!isSuccess)
            {
                // 실패시 처리
            }
        }


    }

    //WebGL 플랫폼일 경우 업로드 후 다음 메소드를 실행합니다.
    private void OnFileUploadedWebGL(UploadedFileInfo[] result)
    {
        //실제 스프라이트들의 리스트
        List<Sprite> sprites = new List<Sprite>();

        //스프라이트가 저장된 경로들의 리스트
        List<string> spritePaths = new List<string>();

        if(result.Length == 0) 
        {
            Debug.Log("File upload Error!");
        }
        else
        {
            Debug.Log("File upload success! (result.Length: " + result.Length + ")");
        }

        // 각각의 이미지 파일을 통해 Sprite를 생성하고 리스트에 추가합니다.
        foreach(UploadedFileInfo file in result)
        {
            if(file.isSuccess)
            {
                string debugString = "";
                debugString += ("file.filePath: " + file.filePath);
                debugString += (" exists:" + File.Exists(file.filePath));
                Debug.Log(debugString);

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                byte[] byteArray = File.ReadAllBytes(file.filePath);
                texture.LoadImage(byteArray);

                Sprite s = Sprite.Create(
                    texture, new Rect(0, 0, texture.width, texture.height), 
                    new Vector2(0.5f,0.5f)
                );
                Debug.Log(s.pivot);

                sprites.Add(s);
                spritePaths.Add(file.filePath);
            }
            else
            {
                Debug.LogError("Error Occured : File "+file.name+" load failed");
            }
        }

        // ImageStorage에 가져온 스프라이트들과 스프라이트 경로들을 지정합니다.
        imageStorage.SetSpriteList(sprites);
        imageStorage.SetSpritePathList(spritePaths);
    }

    //기타 플랫폼일 경우 다음 메소드를 업로드 후 실행합니다.
    public void OnFileUploaded(string [] paths)
    {
        //실제 스프라이트들의 리스트
        List<Sprite> sprites = new List<Sprite>();
        
        //스프라이트가 저장된 경로들의 리스트
        List<string> spritePaths = new List<string>();


        if(paths.Length == 0) 
        {
            Debug.Log("File upload Error!");
        }
        else
        {
            Debug.Log("File upload success! (paths.Length: " + paths.Length + ")");
        }

        // 각각의 이미지 파일 경로들을 통해 Sprite를 생성하고 리스트에 추가합니다.
        foreach(string file in paths)
        {
            string extension = Path.GetExtension(file);
            Debug.Log(extension);
            if(extension == ".png" || extension == ".jpg" || extension == ".jpeg")
            {

                Debug.Log("file.filePath: " + file + " exists:" + File.Exists(file));

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);

                byte[] byteArray = File.ReadAllBytes(file);
                
                texture.LoadImage(byteArray);
                
                Sprite s = Sprite.Create(
                    texture, 
                    new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f,0.5f)
                );
                Debug.Log(s.pivot);
                
                sprites.Add(s);
                spritePaths.Add(file);
            }
            else
            {
                Debug.LogError("Error : Unsupported type \'"+extension+"\'.");
            }
        }

        // ImageStorage에 가져온 스프라이트들과 스프라이트 경로들을 지정합니다.
        imageStorage.SetSpriteList(sprites);
        imageStorage.SetSpritePathList(spritePaths);
    }

    // 해당 씬을 벗어나거나, ImageUploader가 제거 되었을때 다음 메소드를 실행합니다.
    private void OnDestroy() {
        if(Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
        {
            WebGLFileUploadManager.onFileUploaded -= OnFileUploadedWebGL;
        }
    }
}
