using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class QuerySandbox : MonoBehaviour
{
    SandboxQuery query;
    Response response = null;
    [SerializeField] string url;
    public delegate void OnResponse(Response response);

    public IEnumerator SendRequest(SandboxQuery query, OnResponse onResponse)
    {
        Response response;
        string jsonResult = JsonUtility.ToJson(query, true);

        Debug.Log(jsonResult);

        /*
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        var bytesOfFile = File.ReadAllBytes(filePath);
        formData.Add(new MultipartFormFileSection("attachment", bytesOfFile, "asustuf.PNG", "multipart/form-data"));
        // formData.Add(new MultipartFormFileSection("attachment", bytesOfFile));

        UnityWebRequest www = UnityWebRequest.Post(severAddress, formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
        */

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormFileSection("sandboxData", Encoding.UTF8.GetBytes(jsonResult), "query.json", "multipart/form-data"));

        

        using (UnityWebRequest www = UnityWebRequest.Post(url,formData))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
            {
                response = null;
                Debug.Log(www.result);
            }
            else
            {
                byte[] resultBinaryData = www.downloadHandler.data;

                string resultStringData = Encoding.Default.GetString(resultBinaryData);
                Debug.Log(resultStringData);
                response = JsonUtility.FromJson<Response>(resultStringData);

            }
        }

        onResponse(response);
    }
}
