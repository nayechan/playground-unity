using System;
using System.Collections.Generic;
using GameEditor.EventEditor.Controller;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SandboxEditor.InputControl.InEditor
{
    public class TouchEvent : UnityEvent<Touch>{
    }

    public class TouchInEditor : MonoBehaviour, PhaseChangeCallBackReceiver
    {
        public static TouchInEditor _TouchInEditor { get; private set; }
        private static Camera _cam;
        private Dictionary<int, TouchEvent> _touchAlarms; // 특정 fingerID의 터치 이벤트를 지속적으로 듣는 스크립트를 위한 이벤트.
        private TouchMode _mode;
        public static TouchMode Mode {get => _TouchInEditor._mode; set => _TouchInEditor._mode = value; }
        private Touch[] touches;
        private bool _isInGameMode = false;
        
        // 211117 WebGL 터치 여러번 입력되는 버그를 수정하기 위한 임시 멤버. 추후 구조 개선 필요
        private float _leftTouchCoolTime = 0f;
        private const float _touchCoolTime = 0.2f;

        private void Awake()
        {
            _TouchInEditor = this;
        }

        private void Start() {
            _cam = Camera.main;
            _touchAlarms = new Dictionary<int, TouchEvent>();
            _mode = TouchMode.CamMove;
        }

        private void Update()
        {
            if (_isInGameMode) return;
            touches = Input.touches;
            DecreaseLeftCoolTime();
            AlarmAll();
            ShotRays();
            ResetOutdatedAlarm();
        }

        private void DecreaseLeftCoolTime()
        {
            _leftTouchCoolTime -= Time.deltaTime;
        }

        private void RewindTimer()
        {
            _leftTouchCoolTime = _touchCoolTime;
        }

        public static TouchInEditor GetTID(){
            return _TouchInEditor;
        }

        private static void ShotRays(){
            foreach(var touch in _TouchInEditor.touches){
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
                    if (!rayIsBlocked) continue;
                    _TouchInEditor.RewindTimer();
                    break;
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
                    if (_TouchInEditor._leftTouchCoolTime > 0f) break;
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
            _TouchInEditor._AlarmMe(fingerID, sensor);
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

        public void WhenGameStart()
        {
            _TouchInEditor._isInGameMode = true;
        }

        public void WhenTestStart()
        {
            _TouchInEditor._isInGameMode = true;
        }

        public void WhenTestPause() { }

        public void WhenTestResume() { }

        public void WhenBackToEditor()
        {
            _TouchInEditor._isInGameMode = false;
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