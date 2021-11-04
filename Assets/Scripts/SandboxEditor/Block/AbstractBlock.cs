using System;
using System.Collections.Generic;
using GameEditor.EventEditor.Line;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Block
{
    public class AbstractBlock : MonoBehaviour
    {
        protected delegate void MyListener();
        public delegate void MyDelegate(GameObject obj);
        // protected GameObject _attachedObject;
        public GameObject _attachedObject;
        protected float[] _inputs, _outputs;
        private MyDelegate _AddComponentMethod;
        private List<ValueLine> _connectedLines;
        private BlockPort[] _ports;
    
        protected virtual void Start(){
            _connectedLines = new List<ValueLine>();
            _ports = GetComponentsInChildren<BlockPort>();
        }

        protected virtual void FixedUpdate(){
            BlockAction();
        }


        virtual protected void BlockAction(){}

        void OnDestroy(){
            foreach(var line in _connectedLines){
                if(line != null){
                    Destroy(line.gameObject);
                }
            }
        }

        public void AddLine(ValueLine line){
            _connectedLines.Add(line);
        }

        public float getOutput(int portNum){
            return _outputs[portNum];
        }

        public MyDelegate GetAddComponentMethod(){
            return _AddComponentMethod;
        }

        public virtual void RunAddComponentMethod(){
            if(_AddComponentMethod == null) return;
            _AddComponentMethod(_attachedObject);
        }

        public virtual void SetAddComponentMethod(MyDelegate Method){
            _AddComponentMethod = Method;
        }

        public void setInput(float val, int portNum){
            _inputs[portNum] = val;
        }

        public virtual void MessageCallBack(string message){ }

        public virtual void PlayStart()
        {
            HideBlock();
        }
        
        
        private void HideBlock()
        {
            transform.Translate(Vector3.back * 100f);
        }
    }
}
