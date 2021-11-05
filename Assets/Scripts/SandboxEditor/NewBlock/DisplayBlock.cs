using System;
using SandboxEditor.Data.Block;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class DisplayBlock : AbstractBlock
    {
        public Camera camera;
        public GameObject guideArea;

        public override void OnEveryFixedUpdate() { }

        public override void WhenGameStart()
        {
            base.WhenGameStart();
            ChangeMainDisplay();
        }

        private void ChangeMainDisplay()
        {
            Camera.main.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
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
            var data = new DisplayBlockData(this);
            data.SetgameObjectIDAndPosition(this);
            return data;
        }
        
        public override void LoadBlockData(BlockData blockData)
        {
            base.LoadBlockData(blockData);
            camera.orthographicSize = ((DisplayBlockData) blockData).camSize;
        }
    }
}