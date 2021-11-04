using System;
using SandboxEditor.Data.Block;
using UnityEngine;

namespace SandboxEditor.NewBlock
{
    public class DisplayBlock : AbstractBlock
    {
        public Camera camera;
        public GameObject guideArea;

        protected override void BlockAction()
        {
            // This block doesn't use Port;
        }

        public override BlockData MakeBlockData()
        {
            var data = new DisplayBlockData(this);
            data.SetIDAndPosition(this);
            return data;
        }
        
        public override void SetBlock(BlockData blockData)
        {
            base.SetBlock(blockData);
            camera.orthographicSize = ((DisplayBlockData) blockData).camSize;
        }
        
        protected override void OnGameStart()
        {
            ChangeMainDisplay();
            DisableRenderer();
        }


        private void DisableRenderer()
        {
            foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
                spriteRenderer.enabled = false;
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
                meshRenderer.enabled = false;
        }

        private void ChangeMainDisplay()
        {
            Camera.main.enabled = false;
            camera.enabled = true;
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
        
        protected override void InitializeBlockValue()
        {
        }
    }
}