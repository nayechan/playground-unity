using System;
using System.Collections.Generic;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SandboxEditor.InputControl.InEditor
{
    public class TouchEvent : UnityEvent<Touch>{
    }

    public class TouchController : MonoBehaviour
    {
        private static TouchController _tid;
        private static Camera _cam;
        private Dictionary<int, TouchEvent> _touchAlarms; // 특정 fingerID의 터치 이벤트를 지속적으로 듣는 스크립트를 위한 이벤트.
        private TouchMode _mode;
        public static TouchMode Mode {get => _tid._mode; set => _tid._mode = value; }
        private Touch[] touches;
        void Start() {
            _cam = Camera.main;
            _touchAlarms = new Dictionary<int, TouchEvent>();
            _tid = this;
            _mode = TouchMode.CamMove;
        }

        void Update() {
            touches = Input.touches;
            AlarmAll();
            ShotRays();
            ResetOutdatedAlarm();
        }

        public static TouchController GetTID(){
            return _tid;
        }

        private static void ShotRays(){
            foreach(var touch in _tid.touches){
                // 각 터치에 대해 하나의 Raycast를 진행. Collider를 가진 객체를 검출한다.
                // 충돌한 객체를 가까운 거리순으로 정렬하고 TouchSensor 컴포넌트를 가지고 있는지 차례대로 확인한다.
                // TouchSensor 컴포넌트를 가지고 있을 경우 해당 컴포넌트에 Hit 함수를 호출해 Touch 정보를 전달하고
                // 더 이상의 신호 전달을 막을 것인지 rayIsBlocked 로 응답한다. rayIsBlocked 가 true 일경우 해당 Raycast에 대한 신호전달을 멈춘다.
                if(IsOnGUI(touch.fingerId)) break;
                var rayOrigin = _cam.ScreenToWorldPoint(touch.position);
                var hits = Physics.RaycastAll(rayOrigin, _cam.transform.forward);
                Array.Sort(hits, (h1, h2) => (int) (h1.distance - h2.distance) * 32);
                foreach(var hit in hits){
                    var sensors = hit.collider.GetComponents<AbstractSensor>();
                    var rayIsBlocked = false;
                    foreach (var sensor in sensors)
                    {
                        rayIsBlocked = TouchHandling(touch, sensor);
                        if(rayIsBlocked) break;
                    }
                    if(rayIsBlocked) break;
                }
            }
        }

        private static bool IsOnGUI(int fingerId)
        {
            return EventSystem.current.IsPointerOverGameObject(fingerId);
        }

        private static bool TouchHandling(Touch touch, AbstractSensor sensor)
        {
            var rayIsBlocked = false;
            switch(touch.phase){
                case TouchPhase.Began:
                    sensor.OnTouchBegan(touch, out rayIsBlocked);
                    break;
                case TouchPhase.Moved:
                    sensor.OnTouchMoved(touch, out rayIsBlocked);
                    break;
                case TouchPhase.Stationary:
                    sensor.OnTouchStationary(touch, out rayIsBlocked);
                    break;
                case TouchPhase.Ended:
                    sensor.OnTouchEnded(touch, out rayIsBlocked);
                    break;
                case TouchPhase.Canceled:
                    sensor.OnTouchCanceled(touch, out rayIsBlocked);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return rayIsBlocked;
        }

        public static void AlarmMe(int fingerID, AbstractSensor sensor)
        {
            _tid._AlarmMe(fingerID, sensor);
        }
        public void _AlarmMe(int fingerID, AbstractSensor sensor){
            // Debug.Log($"{Time.realtimeSinceStartup} Alarm begin : {fingerID}");
            _touchAlarms[fingerID].AddListener(sensor.CallBack);
        }

        private void AlarmAll(){        
            foreach(var touch in touches){
                if(!_touchAlarms.ContainsKey(touch.fingerId)){ //처음 들어오는 입력일 경우
                    _touchAlarms.Add(touch.fingerId, new TouchEvent());
                    continue;
                }
                _touchAlarms[touch.fingerId].Invoke(touch);
            }
        }

        private void ResetOutdatedAlarm(){
            foreach(var touch in touches)
            {
                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) continue;
                // Debug.Log($"{Time.realtimeSinceStartup} Alarm end : {touch.fingerId}");
                _touchAlarms[touch.fingerId].RemoveAllListeners();
                _touchAlarms.Remove(touch.fingerId);
            }
        }
    }
    

    public enum TouchMode
    {
        CamMove,
        CreateObject,
        DeleteObject,
        CreateBlock,
        MoveObject,
        ConnectPort
    }
}