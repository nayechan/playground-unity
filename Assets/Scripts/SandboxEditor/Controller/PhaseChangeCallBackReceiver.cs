namespace GameEditor.EventEditor.Controller
{
    public interface PhaseChangeCallBackReceiver
    {
        public void WhenGameStart();
        public void WhenTestStart();
        public void WhenTestPause();
        public void WhenTestResume();
        public void WhenBackToEditor();
    }
}