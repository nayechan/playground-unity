using System;

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