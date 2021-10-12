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

    Dictionary<string, ImageData> _imageDatas;
    private void Awake()
    {
        //_imageDatas = new List<ImageData>();
        _imageDatas = new Dictionary<string, ImageData>();
    }

    //이미지 데이터 추가
    public void AddImageData(ImageData data)
    {
        if(data!=null)
        {
            List<Sprite> spriteList = new List<Sprite>();

            int imagePathLength = data.GetImagePaths().Count;

            MoveImagePath(data);

            for(int i=0;i<imagePathLength;++i)
            {

                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                if(data.GetImagePaths()[i] != "")
                {
                    byte[] byteArray = File.ReadAllBytes(data.GetImagePaths()[i]);
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
            
            data.SetSprites(spriteList);

            _imageDatas.Add(data.GetUUID(), data);
        }
        
        _imageViewerController.RefreshUI(_imageDatas, false);
        _imageSelectorController.RefreshUI(_imageDatas);
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
                // 프로젝트 경로에 저장이 되도록 일부 수정하였습니다. ** 태형 **
                string sandboxPath = OnBroadSandbox.GetOSB().SandboxPath;
                string relativePath = System.IO.Path.GetFileName(originalPath);
                string fullPath = Path.Combine(sandboxPath, relativePath);
                Debug.Log(fullPath);
                System.IO.Directory.CreateDirectory(sandboxPath);
                System.IO.File.Copy(originalPath, fullPath, true);
                
                newPaths.Add(fullPath);
            }
            else
            {
                newPaths.Add("");
            }
        }
        // 저장되는 경로는 프로젝트 경로를 root로 하는 상대경로로 저장합니다.
        data.SetImagePaths(newPaths);
    }

    public ImageData GetImageData(string uuid)
    {
        return _imageDatas[uuid];
    }
}
