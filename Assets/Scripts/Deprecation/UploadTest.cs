using System.IO;
using ExternalScript.WebGLFileUploader;
using UnityEngine;

namespace Deprecation
{
    public class UploadTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            WebGLFileUploadManager.PopupDialog();
            WebGLFileUploadManager.onFileUploaded += OnFileUploaded;
        }

        private void OnFileUploaded(UploadedFileInfo[] result)
        {
            if(result.Length == 0) {
                Debug.Log("File upload Error!");
            }else{
                Debug.Log("File upload success! (result.Length: " + result.Length + ")");
            }

            foreach(UploadedFileInfo file in result){
                if(file.isSuccess){
                    Debug.Log("file.filePath: " + file.filePath + " exists:" + File.Exists(file.filePath));
                    break;
                }
            }
        }
    }
}
