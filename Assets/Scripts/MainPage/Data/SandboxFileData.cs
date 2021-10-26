using GameEditor.Data;

namespace MainPage.Data
{
    public class SandboxFileData{
        SandboxData data;
        string _filePath;
        public SandboxFileData(string filePath)
        {
            _filePath = filePath;
        }
        public void LoadSandboxData()
        {
            data = new SandboxData();
        }
        public string getFilePath(){return _filePath;}
        public SandboxData GetSandboxData(){return data;}
    }
}