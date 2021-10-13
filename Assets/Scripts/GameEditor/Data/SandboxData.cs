using System.Net.Mime;
using System;
using System.IO;
using UnityEngine;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SandboxData
    {
        public string title;
        public int id;
        public bool isLocalSandbox;
        public string creatorName;
        public DateTime createdTime;
        public DateTime modifiedTime;
        public string description;

        public SandboxData()
        {
            title = "New SandBox";
            id = -1;
            isLocalSandbox = true;
        }

    }
}