using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class QueryVote : MonoBehaviour
{
    [SerializeField] string url;
    public delegate void OnResponse();

    public IEnumerator SendRequest(AttributeUpdateQuery query, OnResponse onResponse)
    {

        string jsonData = JsonUtility.ToJson(query);

        Debug.Log(jsonData);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        
        formData.Add(new MultipartFormFileSection(
            "sandboxData", 
            Encoding.UTF8.GetBytes(jsonData), 
            "query.json", 
            "multipart/form-data"
        ));

        

        using (UnityWebRequest www = UnityWebRequest.Post(url,formData))
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

            }
        }
        onResponse();
    }
}
