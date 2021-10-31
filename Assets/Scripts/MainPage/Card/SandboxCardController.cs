using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainPage.Card
{
    public class SandboxCardController : CardController
    {
        [SerializeField] Image image;
        Button button;
        [SerializeField] Text titleText, authorText;
        public void SetCardData(Sprite sprite, string title, string author)
        {
            if(sprite != null)
            {
                image.sprite = sprite;
            }
            titleText.text = title;
            authorText.text = author;
        }
        public override void OnClick()
        {
        }
    }
}


