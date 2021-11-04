using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor;
using GameEditor.Data;
using System.IO;
using MainPage;
using MainPage.Card;

namespace MainPage.Panel
{
    public class LibraryPanelController : PanelController
    {
        [SerializeField] GameObject projectCardPrefab, addObjectPrefab;
        [SerializeField] Transform contentPanel;
        //[SerializeField] SandboxManager sandboxManager;
        const int colSize = 5;

        void Awake() {

        }
        public override void OnDeactivateComponent()
        {

        }
        public override void OnActivateComponent()
        {

        }
        public override void UpdateComponent()
        {
            int col = 0, row = 0;

            //List<SandboxData> sandboxDatas = Sandbox.

            foreach(Transform t in contentPanel)
            {
                Destroy(t.gameObject);
            }

            foreach(SandboxData data in sandboxDatas)
            {
                Sprite sprite = null;
                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                string thumbnailPath = Path.Combine(
                    Application.persistentDataPath,
                    "RemoteSandboxs",
                    data.id.ToString(),
                    "thumbnail.png"
                );
                if(File.Exists(thumbnailPath))
                {
                    byte[] byteArray = System.IO.File.ReadAllBytes(thumbnailPath);
                    texture.LoadImage(byteArray);

                    sprite = Sprite.Create(
                        texture, new Rect(0, 0, texture.width, texture.height), 
                        new Vector2(0.5f,0.5f)
                    );
                }

                GameObject g = Instantiate(projectCardPrefab, contentPanel);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(col*424,-row*456);
                g.GetComponent<SandboxCardController>().SetCardData(sprite, data.title, data.creatorName);

                ++col; 
                if(col>=colSize){
                    col = 0;
                    ++row;
                }
            }
            GameObject addObject = Instantiate(addObjectPrefab, contentPanel);
            addObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                col*424, -row*456
            );

            contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (row+1)*456);

        }
    }
}

