using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.Block
{
    public class DisplayBlock : AbstractBlock
    {
        public Camera camera;
        public AudioListener audioListener;
        public GameObject guideArea;

        public override void OnEveryFixedUpdateWhenPlaying() { }

        protected override void InitializePortRegister() { }

        protected override void Awake()
        {
            base.Awake();
            camera.enabled = false;
        }

        public override void WhenGameStart()
        {
            base.WhenGameStart();
            Sandbox.EditorCamera.enabled = false;
            Sandbox.EditorCamera.GetComponent<AudioListener>().enabled = false;
            camera.enabled = true;
            audioListener.enabled = true;
        }

        public override void WhenBackToEditor()
        {
            Sandbox.EditorCamera.enabled = true;
            Sandbox.EditorCamera.GetComponent<AudioListener>().enabled = true;
            camera.enabled = false;
            audioListener.enabled = false;
        }

        public override void MessageCallBack(string message)
        {
            var height = camera.orthographicSize;
            var variant = int.Parse(message);
            ChangeSizeOfCameraAndGuideArea(height + variant);
        }
        
        
        private void ChangeSizeOfCameraAndGuideArea(float height){
            camera.orthographicSize = height;
            guideArea.transform.localScale = new Vector3(2f*(camera.aspect*height),2f*height,1f);
        }
        
        public override BlockData SaveBlockData()
        {
            return new DisplayBlockData(this);
        }
        
        public override void LoadBlockData(BlockData blockData)
        {
            base.LoadBlockData(blockData);
            camera.orthographicSize = ((DisplayBlockData) blockData).camSize;
            ChangeSizeOfCameraAndGuideArea(((DisplayBlockData) blockData).camSize);
        }
    }
}