using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class uploader1 : MonoBehaviour
{
    public static string severAddress = "http://10.32.204.40:3000/gameShare/uploadLocal";
    // public static string filePath = "/Users/taehyeongkim/Downloads/asustuf.PNG";
    public static string filePath = "/Users/taehyeongkim/Downloads/sandbox.zip";
    void Start()
    {
        StartCoroutine(Upload(filePath));
    }

    IEnumerator Upload(string filePath)
    {
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
    }
}
