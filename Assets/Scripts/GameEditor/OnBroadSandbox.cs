using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using GameEditor.Data;
using System.IO;

namespace GameEditor
{
    // 역할
    // - 리소스를 Json 으로부터 불러와 Storage 에 적재한다.
    // - 샌드박스명, 샌드박스 경로를 갖고 함수 호출시 반환한다.
    
    public class OnBroadSandbox : MonoBehaviour
    {
        private SandboxData sandboxData;
        public string SandboxPath {get; private set;}
        private static OnBroadSandbox OSB;

        public static OnBroadSandbox GetOSB()
        {
            return OSB;
        }
        // 단 하나의 샌드박스매니저를 만들기 위한 동작입니다.
        void Awake()
        {
            if(OSB == null)
            {
                OSB = this;
            }
            sandboxData = new SandboxData();
            UpdateField(sandboxData);
        }

        // 샌드박스 정보를 가진 JObject 를 받아 리소스 경로와
        // 샌드박스 이름, 아이디 넘버 샌드박스 정보를 불러옵니다.
        public void SetSandbox(JObject jObj)
        {
            try
            {
                sandboxData = jObj.ToObject<SandboxData>();
            }
            catch
            {
                Debug.Log("Can't Load SandboxData");
                return;
            }
            UpdateField(this.sandboxData);
        }

        public void SetSandbox(SandboxData sandboxData)
        {
            this.sandboxData = sandboxData;
            UpdateField(this.sandboxData);
        }

        private void UpdateField(SandboxData sandboxData)
        {
            SandboxPath = Application.persistentDataPath + '/' + sandboxData.id;
        }
        
        public string GetFullPath(string reletivePath)
        {
            return Path.Combine(SandboxPath, reletivePath);
        }

    }
}