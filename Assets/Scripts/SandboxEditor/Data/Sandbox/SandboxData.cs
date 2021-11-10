using System;
using Tools;

namespace SandboxEditor.Data.Sandbox
{
    [System.Serializable]
    public class SandboxData
    {
        public string title;
        public string id;
        public bool isLocalSandbox;
        public string creatorName;
        public DateTime createdTime;
        public DateTime modifiedTime;
        public string description;

        public string SandboxPath => SandboxChecker.GetSandboxPath(this);
        public string SandboxDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfSandboxData);
        public string ToyDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfToyData);
        public string ToyStorageDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfToyStorageData);
        public string ImageDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfImageStorageData);
        public string ConnectionDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfConnectionData);
        public string BlockDataPath => SandboxChecker.MakeFullPath(this, Names.JsonNameOfBlockData);
        
        

        public SandboxData()
        {
            title = "New SandBox";
            isLocalSandbox = true;
            id = "1234";
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