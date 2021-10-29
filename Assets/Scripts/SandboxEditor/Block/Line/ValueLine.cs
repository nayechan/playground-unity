using GameEditor.EventEditor.Block;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Line
{
    public class ValueLine : MonoBehaviour
    {
        protected delegate void Runer();
        protected AbstractBlock _giver, _reciver;
        protected Runer _Run;
        private int _giverPort, _reciverPort;
        private float _signal;
        private BlockPort[] _ports;

        void Start(){
            _Run = ()=>{};
        }
        void Update()
        {
            _Run();
        }

        public void ReciveSignal(){
            _signal = _giver.getOutput(_giverPort);
        }

        public void SendSignal(){
            _reciver.setInput(_signal, _reciverPort);
        }

        public virtual void SetLine(BlockPort giver, BlockPort reciver){
            _ports = new BlockPort[2];
            this._giver = giver.body;
            this._reciver = reciver.body;
            this._giverPort = giver.portNum;
            this._reciverPort = reciver.portNum;
            Debug.Log("settig, "+ giver.ToString() + reciver.ToString());
            _ports[0] = giver;
            _ports[1] = reciver;
        }

        public void ReRendering(){
            LineRenderer rend = GetComponent<LineRenderer>();
            rend.SetPosition(0, _ports[0].transform.position);
            rend.SetPosition(1, _ports[1].transform.position);
        }

        void OnDestroy(){
            _reciver.setInput(0f, _reciverPort);
        }

        public virtual void PlayStart(){
            _Run += ReciveSignal;
            _Run += SendSignal;
            LineRenderer lr = GetComponent<LineRenderer>();
            if(lr != null) lr.enabled = false;
        }
    }
}
