using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;
using MainPage;

namespace MainPage.Window
{
    public class CreateSandboxWindowController : WindowController
    {
        [SerializeField] InputField titleText, contentText;
        [SerializeField] SandboxManager sandboxManager;
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
            
            sandboxManager.AddSandbox(sandboxData);
            OnDeactivateComponent();
            gameObject.SetActive(false);

        }
    }
}
