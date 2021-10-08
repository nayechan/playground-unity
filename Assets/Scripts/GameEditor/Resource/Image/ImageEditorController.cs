using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageEditorController : MonoBehaviour
{
    [SerializeField] Image previewImage;

    [SerializeField] InputField nameInputField, hSizeInputField, vSizeInputField;

    [SerializeField] Text statusText;

    [SerializeField] Toggle toggleSingleMode, toggleSizeMode;

    [SerializeField] Transform multiModePanel;
    [SerializeField] ImageStorage imageStorage;

    List<Sprite> sprites;
    List<string> spritePaths;

    bool isSingleMode, isRelativeSize;

    int currentIndex;
    float defaultHeight, defaultWidth;
    float h, w;

    // Start is called before the first frame update
    void Start()
    {
        sprites = new List<Sprite>();
        spritePaths = new List<string>();

        currentIndex = 0;

        defaultWidth = previewImage.GetComponent<RectTransform>().sizeDelta.x;
        defaultHeight = previewImage.GetComponent<RectTransform>().sizeDelta.y;

        AddImage();
    }
    public void RefreshUI()
    {

        isSingleMode = toggleSingleMode.isOn;
        
        isRelativeSize = toggleSizeMode.isOn;

        multiModePanel.gameObject.SetActive(!isSingleMode);
        toggleSizeMode.interactable = isSingleMode;

        if(!isSingleMode)
        {
            toggleSizeMode.isOn = false;
        }

        h = 1;
        w = 1;

        try{
            w = float.Parse(hSizeInputField.text);
            h = float.Parse(vSizeInputField.text);
        }
        catch(Exception e)
        {

        }
       
        if(sprites.Count > 0)
        {
            previewImage.sprite = sprites[currentIndex];

            if(sprites[currentIndex] != null && isRelativeSize)
            {
                h *= sprites[currentIndex].texture.height;
                w *= sprites[currentIndex].texture.width;
            }
        }

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

        previewImage.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);

        statusText.text = "Image "+(currentIndex+1)+"/"+sprites.Count;
    }
    public void PrevImage()
    {
        --currentIndex;
        if(currentIndex < 0)
        {
            currentIndex = 0;
        }
        RefreshUI();
    }
    public void NextImage()
    {
        ++currentIndex;
        if(sprites.Count == 0)
        {
            currentIndex = 0;
        }
        else if(currentIndex >= sprites.Count)
        {
            currentIndex = sprites.Count - 1;
        }
        RefreshUI();
    }
    public void AddImage()
    {
        sprites.Add(null);
        spritePaths.Add("");
        RefreshUI();
    }
    public void SetCurrentImage(string path)
    {
        Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        byte[] byteArray = File.ReadAllBytes(path);
        texture.LoadImage(byteArray);

        Sprite s = Sprite.Create(
            texture, new Rect(0, 0, texture.width, texture.height), 
            new Vector2(0.5f,0.5f)
        );

        sprites[currentIndex] = s;
        spritePaths[currentIndex] = path;
        RefreshUI();
    }
    public void RemoveCurrentImage()
    {
        sprites.RemoveAt(currentIndex);
        spritePaths.RemoveAt(currentIndex);
        PrevImage();
    }
    public void ResetAll()
    {
        sprites.Clear();
        spritePaths.Clear();

        currentIndex = 0;

        toggleSingleMode.isOn = true;
        toggleSizeMode.isOn = true;

        nameInputField.text = "";
        hSizeInputField.text = "";
        vSizeInputField.text = "";

        AddImage();
    }

    public void ResetSizeMode()
    {
        if(toggleSingleMode.isOn)
        {
            toggleSizeMode.isOn = true;
        }
    }

    public bool ValidateForm()
    {
        return true;
    }

    public ImageData GenerateImageData()
    {
        Debug.Log("isRelativeSize : "+isRelativeSize);
        h = 1;
        w = 1;

        try{
            w = float.Parse(hSizeInputField.text);
            h = float.Parse(vSizeInputField.text);
        }
        catch(Exception e)
        {

        }
        ImageData imageData = 
        new ImageData(isSingleMode, isRelativeSize, h, w, nameInputField.text);

        imageData.SetImagePaths(spritePaths);

        return imageData;
    }

    public void OnAddButtonClicked()
    {
        ImageData imageData;
        if(ValidateForm())
        {
            imageData = GenerateImageData();
            Debug.Log(imageData);
            imageStorage.AddImageData(imageData);
        }

        OnClose();
    }

    public void OnClose()
    {
        ResetAll();
    }
}
