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
            id = CreateNonOverlappingLocalId();
            creatorName = "";
            createdTime = DateTime.Now;
            modifiedTime = DateTime.Now;
            description = "";
            Debug.Log("Sandbox Created");
        }

        private int CreateNonOverlappingLocalId()
        {
            int newId = new int();
            for(int i = 0; i < 100; ++i)
            {
                newId = (new System.Random()).Next(Int32.MinValue, Int32.MaxValue);
                if(SandboxChecker.isAlreadyExistId(newId, true))
                {
                    Debug.Log($"{newId} is already exist");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return newId;
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