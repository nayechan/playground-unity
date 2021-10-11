using System.IO;

namespace GameEditor.Data
{
    [System.Serializable]
    public class SandboxData
    {
        public string title;
        public int id;
        public bool editable;
        public bool isLocalSandbox;

        public SandboxData()
        {
            title = "New SandBox";
            id = -1;
            editable = true;
            isLocalSandbox = true;
        }

        public string GetRelativeSandboxPath()
        {
            return Path.Combine(
                isLocalSandbox ? 
                    SandboxSaveLoader.LocalSandboxDirectoryName :
                    SandboxSaveLoader.RemoteSandboxDirectoryName,
                id.ToString());
        }
    }
}