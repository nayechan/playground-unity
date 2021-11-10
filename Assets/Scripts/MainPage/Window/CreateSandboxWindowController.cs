using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SandboxEditor.Data.Sandbox;
using GameEditor;
using MainPage;
using MainPage.Panel;
using System.IO;
using Tools;

namespace MainPage.Window
{
    public class CreateSandboxWindowController : WindowController
    {
        [SerializeField] InputField titleText, contentText;
        [SerializeField] LibraryPanelController libraryPanel;
        //[SerializeField] SandboxManager sandboxManager;
        public override void OnActivateComponent()
        {
            base.OnActivateComponent();
        }   

        public override void OnDeactivateComponent()
        {
            base.OnDeactivateComponent();
            titleText.text = "";
            contentText.text = "";
        }   

        public override void UpdateComponent()
        {

        }  

        public void OnSubmit()
        {
            SandboxData sandboxData = new SandboxData();
            sandboxData.title = titleText.text;
            sandboxData.description = contentText.text;
            sandboxData.id = SandboxChecker.CreateNonOverlappingLocalId();
            sandboxData.creatorName = PlayerPrefs.GetString("myNickName","Playground");

            if(sandboxData.title == "") {sandboxData.title = "No title";}
            if(sandboxData.description == ""){sandboxData.description = "No Description";}
            if(sandboxData.creatorName == ""){sandboxData.description = "Unknown Creator";}

            // 기존 저장 함수를 삭제하고 이 함수로 대체해주세요.
            SandboxSaveLoader.InitializeLocalSandbox(sandboxData);

            libraryPanel.UpdateComponent();

            OnDeactivateComponent();
            gameObject.SetActive(false);

        }
    }
}
