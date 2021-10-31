using UnityEngine;
using GameEditor;
using GameEditor.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MainPage.Panel;

namespace MainPage
{
    public class SandboxManager : MonoBehaviour{
        [SerializeField] PanelController panelToUpdate;
        List<SandboxData> sandboxDatas;
        string debugPath;
        private void Awake() {
            sandboxDatas = new List<SandboxData>();
            debugPath = "/Users/yechanna/Desktop/sandbox_test";
            StartCoroutine("LoadSandboxFolders");
        }

        IEnumerator LoadSandboxFolders(){
            foreach(string path in Directory.EnumerateDirectories(debugPath))
            {
                var rawSandboxData = System.IO.File.ReadAllText(path+"/SandboxData.json");

                SandboxData sandboxData = JsonUtility.FromJson<SandboxData>(rawSandboxData);

                sandboxDatas.Add(sandboxData);

                panelToUpdate.UpdateComponent();

                yield return null;
            }
        }

        public void AddSandbox(SandboxData data)
        {
            sandboxDatas.Add(data);
            panelToUpdate.UpdateComponent();
        }

        public List<SandboxData> GetSandboxDatas(){return sandboxDatas;}


    }
}