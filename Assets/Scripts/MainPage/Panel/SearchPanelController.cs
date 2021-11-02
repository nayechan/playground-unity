using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainPage;
using MainPage.Card;
using GameEditor.Data;
using System.IO;

namespace MainPage.Panel
{
    public class SearchPanelController : PanelController
    {
        [SerializeField] GameObject projectCardPrefab;
        [SerializeField] SandboxCardOnClickOperation operation;
        [SerializeField] Transform contentPanel;
        [SerializeField] QuerySandbox querySandbox;
        Response currentResponseData = null;
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

            if(currentResponseData==null)
                return;

            List<Response.ResponseItem> responseDatas = currentResponseData.GetDataList();

            foreach(Transform t in contentPanel)
            {
                Destroy(t.gameObject);
            }


            foreach(Response.ResponseItem responseData in responseDatas)
            {
                Sprite sprite = null;
                // Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
                // if(File.Exists(debugPath + "/" + data.id + "/thumbnail.png"))
                // {
                //     byte[] byteArray = System.IO.File.ReadAllBytes(debugPath + "/" + data.id + "/thumbnail.png");
                //     texture.LoadImage(byteArray);

                //     sprite = Sprite.Create(
                //         texture, new Rect(0, 0, texture.width, texture.height), 
                //         new Vector2(0.5f,0.5f)
                //     );
                // }

                GameObject g = Instantiate(projectCardPrefab, contentPanel);
                g.GetComponent<RectTransform>().anchoredPosition = new Vector2(col*424,-row*456);
                g.GetComponent<SandboxCardController>().SetCardData(
                    sprite, 
                    responseData.getTitle(),
                    responseData.getCreatorName()
                );
                g.GetComponent<SandboxCardController>().setClickOperation(operation);

                ++col; 
                if(col>=colSize){
                    col = 0;
                    ++row;
                }
            }

            contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (row+1)*456);

        }
        public void OnSearchResult(Response response)
        {
            currentResponseData = response;
            UpdateComponent();
        }
    }
}

