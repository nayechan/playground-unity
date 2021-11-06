using System;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class DisplayBlock : AbstractBlock
    {
        public Camera camera;
        public GameObject guideArea;

        public override void OnEveryFixedUpdateWhenPlaying() { }

        public override void WhenGameStart()
        {
            base.WhenGameStart();
            ChangeMainDisplay();
        }

        private void ChangeMainDisplay()
        {
            Camera.main.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
            Sandbox.Camera = camera;
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
        }
    }
}