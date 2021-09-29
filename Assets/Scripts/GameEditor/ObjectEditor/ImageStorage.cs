using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageStorage : MonoBehaviour
{
    [SerializeField] private List<ImageData> _imageDatas;
    [SerializeField] private ImageEditorController _imageEditorController;
    private void Awake()
    {
        _imageDatas = new List<ImageData>();
    }

    public void LoadSprites(ImageData data)
    {

        List<Sprite> spriteList = new List<Sprite>();

        int imagePathLength = data.GetImagePaths().Count;

        for(int i=0;i<imagePathLength;++i)
        {
            CopyImageDataAtIndex(data, i);

            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            byte[] byteArray = File.ReadAllBytes(data.GetImagePaths()[i]);
            texture.LoadImage(byteArray);

            Sprite s = Sprite.Create(
                texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f,0.5f)
            );
            Debug.Log(s.pivot);
            spriteList.Add(s);
        }
        
        data.SetSprites(spriteList);

        _imageDatas.Add(data);
        
        _imageEditorController.RefreshUI(_imageDatas);
    }

    // 이미지 데이터를 앱 내부 데이터 폴더로 복사합니다.
    public void CopyImageDataAtIndex(ImageData data, int i)
    {
        string newPath = Application.persistentDataPath;
        newPath += ("/" + System.IO.Path.GetFileName(data.GetImagePaths()[i]));

        Debug.Log(newPath);

        System.IO.File.Copy(data.GetImagePaths()[i], newPath, true);
        data.SetImagePathAtIndex(newPath, i);
    }

    public void AddImageData(ImageData data)
    {
        Debug.Log("test");
        LoadSprites(data);
    }
}

    
//     Sprite currentSprite;
//     int currentImageIndex = -1;

//     float defaultWidth, defaultHeight;
//     private void Start() {
//         sprites = new List<Sprite>();
//         spritePaths = new List<string>();
        
//         defaultWidth = 
//         displayTarget.GetComponent<RectTransform>().rect.width;
        
//         defaultHeight = 
//         displayTarget.GetComponent<RectTransform>().rect.height;
//     }
//     public List<Sprite> GetSpriteList()
//     {
//         return sprites;
//     }
//     public List<string> GetSpritePathList()
//     {
//         return spritePaths;
//     }
//     public void SetSpriteList(List<Sprite> sprites)
//     {
//         if(sprites.Count > 0)
//         {
//             this.sprites = sprites;
//             currentImageIndex = 0;
//             UpdateDisplay();
//         }
//     }
//     public void SetSpritePathList(List<string> spritePaths)
//     {
//         this.spritePaths = spritePaths;
//     }
//     public Sprite GetCurrentSprite()
//     {
//         return currentSprite;
//     }
//     public void SetCurrentSprite(Sprite sprite)
//     {
//         currentSprite = sprite;
//         sprites[currentImageIndex] = currentSprite;
//     }

//     public void UpdateDisplay()
//     {
//         currentSprite = sprites[currentImageIndex];
//         displayTarget.sprite = currentSprite;
//         float h = displayTarget.sprite.texture.height;
//         float w = displayTarget.sprite.texture.width;
//         if(h > w)
//         {
//             w = (w/h) * defaultWidth;
//             h = defaultHeight;
//         }
//         else
//         {
//             h = (h/w) * defaultHeight;
//             w = defaultWidth;
//         }
//         displayTarget.GetComponent<RectTransform>().sizeDelta =
//         new Vector2(w,h);
//         displayText.text = 
//         "Image\n"+(currentImageIndex+1)+"/"+sprites.Count;
//     }

//     public void SelectNext()
//     {
//         if(sprites == null) return;
//         ++currentImageIndex;
//         if(currentImageIndex >= sprites.Count)
//             currentImageIndex = sprites.Count;
//         UpdateDisplay();
//     }

//     public void SelectPrev()
//     {
//         if(sprites == null) return;
//         --currentImageIndex;
//         if(currentImageIndex < 0)
//             currentImageIndex = 0;
//         UpdateDisplay();
//     }
    

//     public void ResetComponent()
//     {
//         sprites.Clear();
//         currentSprite = null;
//         currentImageIndex = -1;
//         displayTarget.GetComponent<RectTransform>().sizeDelta =
//         new Vector2(360, 360);
//         displayTarget.sprite = null;
//         displayText.text = 
//         "Image\n"+(currentImageIndex+1)+"/"+sprites.Count;
//     }
// }
