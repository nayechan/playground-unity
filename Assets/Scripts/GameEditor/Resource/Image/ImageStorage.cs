using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using GameEditor;

/*
이미지를 저장하기 위한 저장소입니다.
*/
public class ImageStorage : MonoBehaviour
{
    //[SerializeField] private List<ImageData> _imageDatas;
    [SerializeField] private ImageViewerController _imageViewerController;
    [SerializeField] private ImageSelectorController _imageSelectorController;
    private static ImageStorage _imageStorage;

    Dictionary<int, ImageData> _imageDatas;
    private void Awake()
    {
        SetSingletonIfUnset();
        _imageDatas = new Dictionary<int, ImageData>();
    }

    private void SetSingletonIfUnset()
    {
        if(_imageStorage == null)
        {
            _imageStorage = this;
        }
    }

    public static ImageStorage GetSingleton()
    {
       return _imageStorage;
    }

    //이미지 데이터 추가
    public void AddImageData(ImageData imageData)
    {
        if(imageData!=null && !ContainsImageData(imageData))
        {
            List<Sprite> spriteList = new List<Sprite>();

            int imagePathLength = imageData.GetImagePaths().Count;

            MoveImagePath(imageData);

            for(int i=0;i<imagePathLength;++i)
            {

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                if(imageData.GetImagePaths()[i] != "")
                {
                    byte[] byteArray = File.ReadAllBytes(imageData.GetImagePaths()[i]);
                    texture.LoadImage(byteArray);

                    Sprite s = Sprite.Create(
                        texture, new Rect(0, 0, texture.width, texture.height), 
                        new Vector2(0.5f,0.5f)
                    );
                    Debug.Log(s.pivot);
                    spriteList.Add(s);
                }
                else
                {
                    spriteList.Add(null);
                }
            }
            
            imageData.SetSprites(spriteList);

            _imageDatas.Add(imageData.GetHashCode(), imageData);
        }
        
        _imageViewerController.RefreshUI(_imageDatas, false);
        _imageSelectorController.RefreshUI(_imageDatas);
    }

    private bool ContainsImageData(ImageData imageData)
    {
        return _imageDatas.ContainsKey(imageData.GetHashCode());
    }

    // 이미지 데이터를 앱 내부 데이터 폴더로 복사합니다.
    public void MoveImagePath(ImageData data)
    {
        List<string> originalPaths = data.GetImagePaths();
        List<string> newPaths = new List<string>();

        foreach(string originalPath in originalPaths)
        {
            if(originalPath != "")
            {
                string sandboxPath = SandboxSaveLoader.GetSingleton().currentSandboxPath;
                string relativePath = System.IO.Path.GetFileName(originalPath);
                string fullPath = Path.Combine(sandboxPath, relativePath);
                Debug.Log(fullPath);
                System.IO.Directory.CreateDirectory(sandboxPath);
                System.IO.File.Copy(originalPath, fullPath, true);
                
                newPaths.Add(relativePath);
            }
            else
            {
                newPaths.Add("");
            }
        }
        data.SetImagePaths(newPaths);
    }

    public ImageData GetImageData(int uuid)
    {
        return _imageDatas[uuid];
    }

}
