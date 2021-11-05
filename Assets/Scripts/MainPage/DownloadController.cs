using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;
using MainPage.Panel;
using MainPage;
using Network;

public class DownloadController : MonoBehaviour
{
    [SerializeField] string url;
    [SerializeField] DownloadPanelController downloadPanel;
    [SerializeField] SandboxInitializer sandboxInitializer;
    //[SerializeField] SandboxManager sandboxManager;

    public IEnumerator SendRequest(string gameID)
    {

        WWWForm form = new WWWForm();
        form.AddField("gameId", gameID);
        

        using (UnityWebRequest www = UnityWebRequest.Post(url,form))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.result);
            }
            else
            {
                byte[] resultBinaryData = www.downloadHandler.data;

                string resultStringData = Encoding.Default.GetString(resultBinaryData);
                Debug.Log(resultStringData);

                OnResponse(resultStringData, gameID);

            }
        }
    }

    public void StartDownload()
    {
        string gameId = downloadPanel.GetCurrentResponseData().getGameID();
        StartCoroutine(SendRequest(gameId));
    }
    public void OnResponse(string resultStringData, string gameId)
    {
        DownloadResponse response = 
        JsonUtility.FromJson<DownloadResponse>(resultStringData);

        StartCoroutine(ProcessDownload(response.getAttachmentPath(), gameId));
    }
    IEnumerator ProcessDownload(string downloadPath, string gameId)
    {
        Debug.Log(downloadPath);
        var uwr = new UnityWebRequest(downloadPath, UnityWebRequest.kHttpVerbGET);
        string path = Path.Combine(
            Application.persistentDataPath,
            "RemoteSandboxs",
            gameId,
            gameId+".zip"
        );
        Debug.Log(path);
        uwr.downloadHandler = new DownloadHandlerFile(path);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.LogError(uwr.error);
        else
            Debug.Log("File successfully downloaded and saved to " + path);

        Extractor.ExtractZip(path);

        File.Delete(path);
        //StartCoroutine(sandboxManager.LoadSandboxFolders());
        sandboxInitializer.ReloadSandbox();
    }
}
