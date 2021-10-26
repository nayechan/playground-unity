using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MainPage.Data;
using MainPage.Panel;

namespace MainPage
{   
    public class SandboxManager : MonoBehaviour {
        List<SandboxFileData> sandboxFileDataList;
        [SerializeField] MenuController menuController;
        
        //LoadSandboxList()

        private void Awake() {
            StartCoroutine("LoadSandbox");
            
        }

        IEnumerator LoadSandbox()
        {
            sandboxFileDataList = new List<SandboxFileData>();

            SandboxFileData sandboxFileData = 
            new SandboxFileData("D:\\test\\test_project1");

            sandboxFileDataList.Add(sandboxFileData);
            yield return null;
            OnFinishInit();
        }

        void OnFinishInit()
        {
            menuController.UpdateCurrentPanel();
        }

        public List<SandboxFileData> GetSandboxFileDatas()
        {
            return sandboxFileDataList;
        }
    }    
}