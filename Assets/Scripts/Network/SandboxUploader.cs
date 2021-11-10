using System.Collections;
using System.Collections.Generic;
using System.IO;
using Network;
using SandboxEditor.Data.Sandbox;
using Tools;
using UnityEngine;
using UnityEngine.Networking;
using File = System.IO.File;

public class SandboxUploader : MonoBehaviour
{
    private const string severAddress = "http://10.8.0.2:5000/gameShare/uploadGame";
    public Sandbox sandbox;
    private string SandboxPath => SandboxChecker.GetSandboxPath(sandbox._sandboxData);
    private static string ZipFilePath => Path.Combine(SandboxChecker.ApplicationPath, Names.TemporaryFolderName,  Names.CompressedSandboxName);
    private string SandboxDataJsonPath => Path.Combine(SandboxPath, Names.JsonNameOfSandboxData);

    public void CompressAndUploadSandbox()
    {
        Compressor.CreateZip(SandboxPath, ZipFilePath);
        StartCoroutine(Upload());
    }

    private IEnumerator Upload()
    {
        var formData = new List<IMultipartFormSection>();
        var bytesOfFile = File.ReadAllBytes(ZipFilePath);
        var bytesOfJson= File.ReadAllBytes(SandboxDataJsonPath);
        formData.Add(new MultipartFormFileSection("attachment", bytesOfFile, "Sandbox.zip", "multipart/form-data"));
        formData.Add(new MultipartFormFileSection("sandboxData", bytesOfJson, "sandboxData.json", "multipart/form-data"));

        var www = UnityWebRequest.Post(severAddress, formData);
        yield return www.SendWebRequest();

        Debug.Log(www.result != UnityWebRequest.Result.Success ? www.error : "Form upload complete!");
    }
}
