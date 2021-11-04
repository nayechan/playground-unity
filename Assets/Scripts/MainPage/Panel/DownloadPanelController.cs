using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;
using System.IO;
using MainPage;
using MainPage.Card;

namespace MainPage.Panel
{
    public class DownloadPanelController : PanelController
    {
        Response.ResponseItem currentResponseData;

        [SerializeField] Text titleText, creatorNameText, gameIDText;
        [SerializeField] Text descriptionText;

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
            titleText.text = currentResponseData.getTitle();
            creatorNameText.text = currentResponseData.getCreatorName();
            gameIDText.text = "ID : "+currentResponseData.getGameID();
            descriptionText.text = currentResponseData.getDescription();
        }
        public void SetCurrentResponseData(Response.ResponseItem responseData)
        {
            currentResponseData = responseData;
        }
    }
}

