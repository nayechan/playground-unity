using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*

프로그램 시작시 Resources에 있는 이미지 파일들을 자동으로 로드하여 
유저가 생성할 수 있도록 Canvas에 추가하고,
이후에 추가되는 Toy 또한 Canvas에 추가하는 역할을 합니다.

*/
namespace GameEditor
{
    public class PrefabManager : MonoBehaviour
    {
        public GameObject TileCanvas;
        private bool[] _isTaken;
        void Start()
        {
            //필요한 변수 초기화 합니다.
            _isTaken = new bool[100];
            for(int i=0; i<100; ++i)
            {
                _isTaken[i] = new bool();
                _isTaken[i] = false;
            }
            // Asset/Resources/Common 폴더의 파일을 불러오는 과정입니다.
            string path = Application.dataPath + "/Resources/Common";
            var files = new System.IO.DirectoryInfo(path).GetFiles();
            foreach(var file in files)
            {
                //png 파일만을 대상으로 Canvas 추가를 진행합니다.
                if(file.Extension == ".png")
                {
                    string s = file.ToString();
                    string[] words = s.Split('\\');
                    bool a = false;
                    string ns = "";
                    foreach(var word in words)
                    {
                        if(a == true)
                        {
                            ns += word + '/';
                        }
                        if(word == "Resources")
                        {
                            a = true;
                        }
                    }
                    // 확장자를 제거해줍니다.
                    ns = ns.Remove(ns.Length - 5);
                    var ts = Resources.Load<Texture2D>(ns);
                    var info = new ObjectInfo();
                    info.texturePath = ns;
                    AddNewPrefab(ts, file.Name, info);
                }
            }

        }


        void AddNewPrefab(Texture2D ts, string name, ObjectInfo info)
        {
            int num = FindEmpty();
            _isTaken[num] = true;
            GameObject Box = new GameObject("Box " + num.ToString());
            Box.transform.parent = TileCanvas.transform;
            SetBox(Box, num);
            SetButton(Box, name, ts, info);
            // SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            // sr.sprite =  Sprite.Create(ts, new Rect(0f, 0f, ts.width, ts.height), new Vector2(0.5f, 0.5f), ts.width);
        }

        // 캔버스 내에 비어있는 위치를 찾는 함수입니다. 
        int FindEmpty()
        {
            for(int i=0; i<100; ++i)
            {
                if(_isTaken[i] == false)
                {
                    return i;
                }
            }
            Debug.Log("ERROR : Not Enough place to contain.");
            return -1;
        }
        
        void SetBox(GameObject Box, int num)
        {
            // 배경 박스를 추가함.
            var rect = Box.AddComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(40f + 280f*(num%4), -40f + -320f*(num/4));
            rect.sizeDelta = new Vector2(240f, 240f);
            rect.pivot = rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.localScale = Vector2.one;
            Box.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
            Box.AddComponent<Image>().color = new Color(0f, 0.5f, 1f , 1f);
        }

        void SetButton(GameObject Box, string name, Texture2D ts, ObjectInfo info)
        {
            // 타일 이미지를 추가함.
            GameObject tile = new GameObject(name);
            tile.transform.parent = Box.transform;
            var rect = tile.AddComponent<RectTransform>();
            rect = tile.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(120f, 120f);
            rect.pivot = rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition3D = Vector3.zero;
            rect.localScale = Vector3.one;
            tile.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
            var img = tile.AddComponent<Image>();
            img.color = Color.white;
            img.sprite = Sprite.Create(ts, new Rect(0f,0f,ts.width,ts.height), new Vector2(0.5f, 0.5f), ts.width);
            var but = Box.AddComponent<Button>();
            but.targetGraphic = img;
            but.onClick.AddListener( delegate 
                {
                    var om = ObjectManager.GetOM();
                    om.selectedObjectInfo = info;
                    Debug.Log("Info selected : " + info.texturePath);
                }
            );
        }
    }
}
