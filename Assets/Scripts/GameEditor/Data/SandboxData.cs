namespace GameEditor.Data
{
    [System.Serializable]
    public class SandboxData
    {
        public string title;
        public int id;
        public bool editable;
        public bool isOnServer;

        public SandboxData()
        {
            title = "New SandBox";
            id = -1;
            editable = true;
            isOnServer = false;
        }
    }
}