using System;
using System.Collections.Generic;
using GameEditor.Common;
using GameEditor.Data;
using GameEditor.Storage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using File = Tools.File;

// 이미지 에디터를 관리하기 위한 스크립트입니다.
namespace GameEditor.Resource.Image
{
    public class ImageEditorController : MonoBehaviour
    {
        //이미지 미리보기
        [SerializeField] UnityEngine.UI.Image previewImage;
        // 제목, 수평 크기, 수직 크기
        [SerializeField] InputField nameInputField, hSizeInputField, vSizeInputField;
        // 파일명을 나타내기 위한 텍스트 UI 요소
        [SerializeField] Text statusText;
        // 단일~멀티모드 선택 토글, 크기 모드 토글 (상대, 절대)
        [SerializeField] Toggle toggleSingleMode, toggleSizeMode;
        // 멀티모드 활성화시 판넬
        [SerializeField] Transform multiModePanel;
        //스프라이트들의 리스트
        List<Sprite> sprites;
        //스프라이트가 저장된 위치들의 리스트
        List<string> spritePaths;
        //현재 단일~멀티모드, 크기모드의 상태
        bool isSingleMode, isRelativeSize;
        //현재 선택된 이미지 인덱스
        int currentIndex;
        //UI의 원래 크기
        float defaultHeight, defaultWidth;
        //높이, 너비 결과값
        float h, w;
        // 정상 작동을 위해 inspector에서 sandbox를 지정해주세요.
        public PanelSwitch closeImageDataBuilderAndOpenMainPanel;
        public ImageSamplePanel imageSamplePanel;
        public Sandbox sandbox;
        // Start is called before the first frame update
        void Start()
        {
            sprites = new List<Sprite>();
            spritePaths = new List<string>();

            currentIndex = 0;

            defaultWidth = previewImage.GetComponent<RectTransform>().sizeDelta.x;
            defaultHeight = previewImage.GetComponent<RectTransform>().sizeDelta.y;

            AddImage();
        }

        //UI 리프레시
        public void RefreshUI()
        {
            isSingleMode = toggleSingleMode.isOn;
            isRelativeSize = toggleSizeMode.isOn;
            multiModePanel.gameObject.SetActive(!isSingleMode);
            toggleSizeMode.interactable = isSingleMode;
            if(!isSingleMode)
                toggleSizeMode.isOn = false;
            h = 1;
            w = 1;

            try{
                w = float.Parse(hSizeInputField.text);
                h = float.Parse(vSizeInputField.text);
                // Debug.Log(w+" "+h);
            }
            catch(Exception e)
            {

            }
       
            if(sprites.Count > 0)
            {
                previewImage.sprite = sprites[currentIndex];

                if(sprites[currentIndex] != null && isRelativeSize)
                {
                    h *= sprites[currentIndex].texture.height;
                    w *= sprites[currentIndex].texture.width;
                }
            }

            if(h > w)
            {
                w = (w/h) * defaultWidth;
                h = defaultHeight;
            }
            else
            {
                h = (h/w) * defaultHeight;
                w = defaultWidth;
            }

            previewImage.GetComponent<RectTransform>().sizeDelta = new Vector2(w,h);
            statusText.text = "Image "+(currentIndex+1)+"/"+sprites.Count;
        }

        //이전 이미지 선택
        public void PrevImage()
        {
            --currentIndex;
            if(currentIndex < 0)
            {
                currentIndex = 0;
            }
            RefreshUI();
        }

        //다음 이미지 선택
        public void NextImage()
        {
            ++currentIndex;
            if(sprites.Count == 0)
            {
                currentIndex = 0;
            }
            else if(currentIndex >= sprites.Count)
            {
                currentIndex = sprites.Count - 1;
            }
            RefreshUI();
        }

        //이미지 추가 (빈 이미지)

        //현재 이미지 인덱스에 해당하는 이미지를 배치
        public void SetCurrentImage(string path)
        {
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            byte[] byteArray = System.IO.File.ReadAllBytes(path);
            texture.LoadImage(byteArray);

            Sprite s = Sprite.Create(
                texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f,0.5f)
            );

            sprites[currentIndex] = s;
            spritePaths[currentIndex] = path;
            RefreshUI();
        }

        //현재 이미지 인덱스의 이미지 제거
        public void RemoveCurrentImage()
        {
            sprites.RemoveAt(currentIndex);
            spritePaths.RemoveAt(currentIndex);
            PrevImage();
        }

        //이미지 리스트 초기화
        public void ResetInputBoxAll()
        {
            sprites.Clear();
            spritePaths.Clear();
            currentIndex = 0;
            toggleSingleMode.isOn = true;
            toggleSizeMode.isOn = true;
            nameInputField.text = "";
            hSizeInputField.text = "";
            vSizeInputField.text = "";
            AddImage();
        }

        public void AddImage()
        {
            sprites.Add(null);
            spritePaths.Add("");
            RefreshUI();
        }
        //크기 모드 초기화
        public void ResetSizeMode()
        {
            if(toggleSingleMode.isOn)
            {
                toggleSizeMode.isOn = true;
            }
        }

        //이미지 입력 폼 검증
        public bool ValidateForm()
        {
            return true;
        }

        //이미지 데이터 생성
        public ImageData BuildImageData()
        {
            // Debug.Log("isRelativeSize : "+isRelativeSize);
            h = 1;
            w = 1;

            try{
                w = float.Parse(hSizeInputField.text);
                h = float.Parse(vSizeInputField.text);
            }
            catch(Exception e)
            {

            }
            var imageData = 
                new ImageData(isSingleMode, isRelativeSize, h, w, nameInputField.text);
            var relativePaths = File.AbsolutePathsToFileNames(spritePaths);
            imageData.SetRelativeImagePaths(relativePaths);

            return imageData;
        }


        //추가 버튼 클릭시
        public void OnAddButtonClicked()
        {
            if(ValidateForm())
            {
                // 추가하기 전 중복되는 파일 이름이 있는지 확인하는 코드 추가 필요
                CopyImagesToSandboxDirectory();
                var imageData = BuildImageData();
                ImageStorage.UpdateImagesDataAndSprites(imageData);
                imageSamplePanel.RefreshPanel();
            }
            closeImageDataBuilderAndOpenMainPanel.Apply();
            ResetInputBoxAll();
        }

        // 이미지 데이터를 앱 내부 데이터 폴더로 복사합니다.
        private void CopyImagesToSandboxDirectory()
        {
            List<string> originalPaths = spritePaths;
            foreach(string originalPath in originalPaths)
            {
                string relativePath = System.IO.Path.GetFileName(originalPath);
                string destination = SandboxChecker.MakeFullPath(sandbox, relativePath);
                System.IO.File.Copy(originalPath, destination, true);
            }
        }

        //취소 버튼/ 닫기 버튼 클릭시
        public void OnClose()
        {
            closeImageDataBuilderAndOpenMainPanel.Apply();
            ResetInputBoxAll();
        }
    }
}
