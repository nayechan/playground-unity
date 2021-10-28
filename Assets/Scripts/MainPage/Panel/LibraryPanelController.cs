using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameEditor.Data;

namespace MainPage.Panel
{
    public class LibraryPanelController : PanelController
    {
        [SerializeField] GameObject projectCardPrefab, addObjectPrefab;
        [SerializeField] Transform contentPanel;
        [SerializeField] SandboxManager sandboxManager;
        const int colSize = 5;

        void Awake() {

        }
        public override void DeactivatePanel()
        {

        }
        public override void ActivatePanel()
        {

        }
        public override void UpdatePanel()
        {
            int col = 0, row = 0;

            string debugPath = "/Users/yechanna/Desktop/sandbox_test";

            List<SandboxData> sandboxDatas = sandboxManager.GetSandboxDatas();

            foreach(Transform t in contentPanel)
            {
                Destroy(t.gameObject);
            }

            foreach(SandboxData data in sandboxDatas)
            {
                Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                byte[] byteArray = System.IO.File.ReadAllBytes(debugPath + "/" + data.id + "/thumbnail.png");
                texture.LoadImage(byteArray);

                Sprite sprite = Sprite.Create(
                    texture, new Rect(0, 0, texture.width, texture.height), 
                    new Vector2(0.5f,0.5f)
                );

                GameObject g = Instantiate(projectCardPrefab, contentPanel);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(col*424,row*456);
                g.GetComponent<SandboxCardController>().SetCard(sprite, data.title, data.creatorName);

                ++col;
                if(col>=colSize){
                    col = 0;
                    ++row;
                }
            }
            GameObject addObject = Instantiate(addObjectPrefab, contentPanel);
            addObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                col*424, row*456
            );

            contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, row*456);

        }
    }
}

