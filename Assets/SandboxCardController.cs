using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandboxCardController : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text titleText, authorText;
    public void SetCard(Sprite sprite, string title, string author)
    {
        if(sprite != null)
        {
            image.sprite = sprite;
        }
        titleText.text = title;
        authorText.text = author;
    }
}
