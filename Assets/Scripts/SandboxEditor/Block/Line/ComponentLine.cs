using GameEditor.EventEditor.UI.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Line
{
    public class ComponentLine : ValueLine
    {
        public override void SetLine(BlockPort giver, BlockPort reciver){
            base.SetLine(giver, reciver);
            reciver.body.SetAddComponentMethod(giver.body.GetAddComponentMethod());
        }

        void OnDestroy(){
            _reciver.SetAddComponentMethod((GameObject obj)=>{});
        }

        public override void PlayStart()
        {
            LineRenderer lr = GetComponent<LineRenderer>();
            if(lr != null) lr.enabled = false;
            _reciver.RunAddComponentMethod();
        }
    }
}