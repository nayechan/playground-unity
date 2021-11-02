using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class SandboxUploaderTest : MonoBehaviour
{
    private const string severAddress = "http://10.32.204.40:3000/gameShare/uploadGame";
    private const string filePath = "/Users/taehyeongkim/Library/Application Support/Lighthouse Keeper/project-playground/LocalSandboxs/404/404.zip";
    private static string jsonPath = "/Users/taehyeongkim/Library/Application Support/Lighthouse Keeper/project-playground/LocalSandboxs/404/SandboxData.json";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Upload(filePath));
    }

    IEnumerator Upload(string filePath)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        var bytesOfFile = File.ReadAllBytes(filePath);
        var bytesOfJson= File.ReadAllBytes(jsonPath);
        formData.Add(new MultipartFormFileSection("attachment", bytesOfFile, "404.zip", "multipart/form-data"));
        formData.Add(new MultipartFormFileSection("sandboxData", bytesOfJson, "sandboxData.json", "multipart/form-data"));

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
