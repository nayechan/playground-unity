// using UnityEngine;
// using GameEditor;
// using GameEditor.Data;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using MainPage.Panel;

// namespace MainPage
// {
//     public class SandboxManager : MonoBehaviour{
//         [SerializeField] PanelController panelToUpdate;
//         List<SandboxData> sandboxDatas;
//         string sandboxPath;
//         private void Awake() {
//             sandboxPath = Path.Combine(
//                 Application.persistentDataPath,
//                 "RemoteSandboxs"
//             );
//             sandboxDatas = new List<SandboxData>();
//             StartCoroutine("LoadSandboxFolders");
//         }

//         public IEnumerator LoadSandboxFolders(){
//             foreach(string path in Directory.EnumerateDirectories(sandboxPath))
//             {
//                 var rawSandboxData = System.IO.File.ReadAllText(path+"/SandboxData.json");

//                 SandboxData sandboxData = JsonUtility.FromJson<SandboxData>(rawSandboxData);

//                 sandboxDatas.Add(sandboxData);

//                 yield return null;
//             }

//             panelToUpdate.UpdateComponent();
//         }

//         public void AddSandbox(SandboxData data)
//         {
//             sandboxDatas.Add(data);
//             panelToUpdate.UpdateComponent();
//         }

//         public List<SandboxData> GetSandboxDatas(){return sandboxDatas;}


//     }
// }