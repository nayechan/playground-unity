using System.Diagnostics;
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
            isLocalSandbox = true;
            id = -1;
            creatorName = "";
            createdTime = DateTime.Now;
            modifiedTime = DateTime.Now;
            description = "";
        }

        public override int GetHashCode()
        {
            return (title.ToString()?? "").GetHashCode()
                    ^ id.GetHashCode()
                    ^ isLocalSandbox.GetHashCode()
                    ^ creatorName.GetHashCode()
                    ^ createdTime.GetHashCode()
                    ^ modifiedTime.GetHashCode() 
                    ^ description.GetHashCode();
        }

    }
}