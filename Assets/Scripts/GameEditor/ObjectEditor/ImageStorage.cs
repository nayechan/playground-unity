using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageStorage : MonoBehaviour
{
    [SerializeField] Image displayTarget;
    [SerializeField] Text displayText;
    List<Sprite> sprites;
    Sprite currentSprite;
    int currentImageIndex = -1;

    float defaultWidth, defaultHeight;
    private void Start() {
        sprites = new List<Sprite>();
        
        defaultWidth = 
        displayTarget.GetComponent<RectTransform>().rect.width;
        
        defaultHeight = 
        displayTarget.GetComponent<RectTransform>().rect.height;
    }
    public List<Sprite> GetSpriteList()
    {
        return sprites;
    }
    public void SetSpriteList(List<Sprite> sprites)
    {
        if(sprites.Count > 0)
        {
            this.sprites = sprites;
            currentImageIndex = 0;
            UpdateDisplay();
        }
    }
    public Sprite GetCurrentSprite()
    {
        return currentSprite;
    }
    public void SetCurrentSprite(Sprite sprite)
    {
        currentSprite = sprite;
        sprites[currentImageIndex] = currentSprite;
    }

    public void UpdateDisplay()
    {
        currentSprite = sprites[currentImageIndex];
        displayTarget.sprite = currentSprite;
        float h = displayTarget.sprite.texture.height;
        float w = displayTarget.sprite.texture.width;
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
        displayTarget.GetComponent<RectTransform>().sizeDelta =
        new Vector2(w,h);
        displayText.text = 
        "Image\n"+(currentImageIndex+1)+"/"+sprites.Count;
    }

    public void SelectNext()
    {
        if(sprites == null) return;
        ++currentImageIndex;
        if(currentImageIndex >= sprites.Count)
            currentImageIndex = sprites.Count;
        UpdateDisplay();
    }

    public void SelectPrev()
    {
        if(sprites == null) return;
        --currentImageIndex;
        if(currentImageIndex < 0)
            currentImageIndex = 0;
        UpdateDisplay();
    }
    

    public void ResetComponent()
    {
        sprites.Clear();
        currentSprite = null;
        currentImageIndex = -1;
        displayTarget.GetComponent<RectTransform>().sizeDelta =
        new Vector2(360, 360);
        displayTarget.sprite = null;
        displayText.text = 
        "Image\n"+(currentImageIndex+1)+"/"+sprites.Count;
    }
}
