using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TouchInputDeliverer : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Touch[] touches = Input.touches;
        foreach(Touch touch in touches){
            // 각 터치에 대해 하나의 Raycast를 진행. Collider를 가진 객체를 검출한다.
            // 충돌한 객체를 가까운 거리순으로 정렬하고 TouchSensor 컴포넌트를 가지고 있는지 차례대로 확인한다.
            // TouchSensor 컴포넌트를 가지고 있을 경우 해당 컴포넌트에 Hit 함수를 호출해 Touch 정보를 전달하고
            // 더 이상의 신호 전달을 막을 것인지 rayIsBlocked 로 응답한다. rayIsBlocked 가 true 일경우 해당 Raycast에 대한 신호전달을 멈춘다.
            Vector3 rayOrigin = cam.ScreenToWorldPoint(touch.position);
            RaycastHit[] hits = Physics.RaycastAll(rayOrigin, cam.transform.forward);
            Array.Sort<RaycastHit>(hits, delegate(RaycastHit h1, RaycastHit h2){return (int)(h1.distance - h2.distance)*32;});
            foreach(RaycastHit hit in hits){
                bool rayIsBlocked = false;
                TouchSensor sensor = hit.collider.GetComponent<TouchSensor>();
                if(sensor == null) continue;
                sensor.Hit(touch, out rayIsBlocked);
                if(rayIsBlocked) break;
            }
        }
    }


    
}
