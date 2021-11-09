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

            Debug.Log(sandboxData.creatorName);
            try{
                SandboxSaveLoader.SaveSandbox(sandboxData, null, null);
            }
            catch{
                
            }

            // string jsonData = JsonUtility.ToJson(sandboxData);

            // Debug.Log(jsonData);


            // string writePath = SandboxChecker.MakeFullPath(sandboxData, Names.JsonNameOfSandboxData);

            // if(!Directory.Exists(SandboxChecker.GetSandboxPath(sandboxData)))
            // {
            //     Directory.CreateDirectory(SandboxChecker.GetSandboxPath(sandboxData));
            // }

            // System.IO.File.WriteAllText(
            //     writePath,
            //     jsonData
            // );

            libraryPanel.UpdateComponent();

            OnDeactivateComponent();
            gameObject.SetActive(false);

        }
    }
}
