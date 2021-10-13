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
        // ingame instances
        [NonSerialized]public GameObject rootOfToy;
        [NonSerialized]public GameObject rootOfBlock;

        public SandboxData()
        {
            var sandboxSaveLoader = SandboxSaveLoader.GetSingleton();
            title = "New SandBox";
            isLocalSandbox = true;
            id = CreateNonOverlappingLocalId();
            creatorName = "";
            createdTime = DateTime.Now;
            modifiedTime = DateTime.Now;
            description = "";
        }

        public void SetRootGameObjects(GameObject rootOfToy, GameObject rootOfBlock)
        {
            this.rootOfToy = rootOfToy;
            this.rootOfBlock = rootOfBlock;
        }

        private int CreateNonOverlappingLocalId()
        {
            int newId;
            do
            {
                newId = (new System.Random()).Next(Int32.MinValue, Int32.MaxValue);
            }
            while(!SandboxSaveLoader.isAlreadyExistId(newId, true));
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