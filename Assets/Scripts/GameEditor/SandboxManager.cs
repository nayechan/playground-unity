using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameEditor
{
    // 역할
    // - 리소스를 Json 으로부터 불러와 Storage 에 적재한다.
    // - 샌드박스명, 샌드박스 경로를 갖고 함수 호출시 반환한다.
    
    public class SandboxManager : MonoBehaviour
    {
        private string Path {get; set;}
        private string Title {get; set;}
        private int Id {get; set;}
        private bool Editable{get; set;}
        private string Origin{get; set;}
        private static SandboxManager SB;

        public static SandboxManager GetSM()
        {
            return SB;
        }
        // 단 하나의 샌드박스매니저를 만들기 위한 동작입니다.
        void Awake()
        {
            if(SB == null)
            {
                SB = this;
            }
            Id = -1;
            Title = "NoNamed";
            Editable = true;
        }

        // 샌드박스 정보를 가진 JObject 를 받아 리소스 경로와
        // 샌드박스 이름, 아이디 넘버 샌드박스 정보를 불러옵니다.
        public void SetSandbox(JObject jObj)
        {
            SandboxManager sb = SandboxManager.GetSM();
            Title = (string)jObj["Title"]; 
            Editable = (bool)jObj["Editable"];
            Origin = (string)jObj["Origin"];
            Id = (int)jObj["Id"];
            Path = Application.persistentDataPath + "/" + Id;
        }

    }
}