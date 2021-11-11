using System;
using System.IO;
using ExternalScript.WebGLFileUploader;
using SandboxEditor.Data.Resource;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Storage;
using UnityEngine;
using UnityEngine.UI;

/*
AudioUploader

설명 : 
플랫폼에 따라 WebGLFileUploaderManager 외부 라이브러리 또는 SimpleFileBrowser로 부터 오디오를 받아와서
AudioStorage 클래스에 오디오를 지정합니다.

외부 참조 라이브러리 :
WebGLFileUploader
SimpleFileBrowser

비고 :
*/

namespace SandboxEditor.UI.Panel.Audio
{
    public class AudioUploader : MonoBehaviour
    {
        [SerializeField] private AudioStorage _audioStorage;

        [SerializeField] private Dropdown _typeDropdown;
        [SerializeField] private Text _fileStatusInputField;

        public Sandbox sandbox;

        private string _currentPath;

        //클릭시
        public void OnUploadButtonClicked()
        {
            // WebGL 플랫폼의 경우
            if(Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
            {

                // 선택 가능한 파일 형식을 지정합니다.
                WebGLFileUploadManager.SetAllowedFileName("\\.(wav|mp3)$");

                // 파일의 인코딩여부를 지정합니다. (이미지가 아니기때문에 false)
                WebGLFileUploadManager.SetImageEncodeSetting(false);

                // 파일 선택 창을 엽니다.
                WebGLFileUploadManager.PopupDialog();

                // 파일이 선택된 후의 동작을 지정합니다.
                WebGLFileUploadManager.onFileUploaded += OnFileUploadedWebGL;
            }
        
            // 기타 플랫폼의 경우
            else
            {
                bool isSuccess = true;
            
                // 파일을 선택하는 창을 엽니다. 선택시 첫번째 delegate, Cancel시 두번째 delegate를 실행합니다.
                // 3번째 인자는 선택 모드에 대한 설정이고, 4번째 인자는 다중 선택 여부에 대한 설정입니다.
                SimpleFileBrowser.FileBrowser.ShowLoadDialog(
                    (string [] paths) => { OnFileUploaded(paths); },
                    () => { isSuccess = false; },
                    SimpleFileBrowser.FileBrowser.PickMode.Files,
                    false
                );

                if(!isSuccess)
                {
                    // 실패시 처리
                }
            }


        }

        //WebGL 플랫폼일 경우 업로드 후 다음 메소드를 실행합니다.
        private void OnFileUploadedWebGL(UploadedFileInfo[] result)
        {
            AudioClip audioClip = null;
            string audioPath;

            if(result.Length == 0) 
            {
                Debug.Log("File upload Error!");
            }
            else
            {
                Debug.Log("File upload success! (result.Length: " + result.Length + ")");
            }
        
            if (result.Length == 0) return;
            UploadedFileInfo file = result[0];
            {
                if(file.isSuccess)
                {
                    string debugString = "";
                    debugString += ("file.filePath: " + file.filePath);
                    debugString += (" exists:" + File.Exists(file.filePath));
                    Debug.Log(debugString);
                
                
                    UpdatePath(file.filePath);
                }
                else
                {
                    Debug.LogError("Error Occured : File "+file.name+" load failed");
                }
            }
        }

        //기타 플랫폼일 경우 다음 메소드를 업로드 후 실행합니다.
        public void OnFileUploaded(string [] paths)
        {
            AudioClip audioClip = null;
            string audioPath;

            if(paths.Length == 0) 
            {
                Debug.Log("File upload Error!");
            }
            else
            {
                Debug.Log("File upload success! (paths.Length: " + paths.Length + ")");
            }
        
            if (paths.Length == 0) return;
            string file = paths[0];
            {
                string debugString = "";
                debugString += ("file: " + file);
                debugString += (" exists:" + File.Exists(file));
                Debug.Log(debugString);

                UpdatePath(file);
            }
        }

        // 해당 씬을 벗어나거나, AudioUploader가 제거 되었을때 다음 메소드를 실행합니다.
        private void OnDestroy() {
            if(Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
            {
                WebGLFileUploadManager.onFileUploaded -= OnFileUploadedWebGL;
            }
        }

        private void UpdatePath(string path)
        {
            _currentPath = path;
            _fileStatusInputField.text = path;
        }

        public void OnConfirmButtonClicked()
        {
            var audioType = _typeDropdown.options[_typeDropdown.value].text;
            CopyAudioData(_currentPath);
            _audioStorage.AddAudioData(new AudioData(Path.GetFileName(_currentPath), audioType));
        }
        
        private void CopyAudioData(string originalPath)
        {
            var fileName = Path.GetFileName(originalPath);
            var sandboxAudioPath = SandboxChecker.MakeFullPath(sandbox, fileName);
            File.Copy(originalPath, sandboxAudioPath, true);
        }
    }
}
