using System;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Toy;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace SandboxEditor.Builder
{
    public class ObjectBuilder : MonoBehaviour
    {
        private ToyData currentToyData;
        private GameObject _newToy;
        public Transform rootObject;
        public bool isSnap;
        private static ObjectBuilder _ObjectBuilder;
        public static bool IsSnap => _ObjectBuilder.isSnap;

        private void Awake()
        {
            _ObjectBuilder = this;
        }

        public static void BuildAndPlaceToy(Vector3 cursorPosition)
        {
            _ObjectBuilder._BuildAndPlaceToy(cursorPosition);
        }

        private void _BuildAndPlaceToy(Vector3 cursorPosition)
        {
            _newToy = Sandbox.BuildSelectedToyOnToyRoot();
            if (_newToy is null) return;
            var newPosition = isSnap ? AdjustedPositionForSnapFunction(cursorPosition) : cursorPosition;
            newPosition.z = 0;
            _newToy.transform.position = newPosition;
        }

        private Vector3 AdjustedPositionForSnapFunction(Vector3 cursorPosition)
        {
            var toySize = _newToy.GetComponent<SpriteRenderer>().bounds.size;
            var newPosition = cursorPosition;
            var pivotAmount = toySize;
            pivotAmount.x *= 0.5f;
            pivotAmount.y *= -0.5f;
            pivotAmount.z = 10;
            newPosition-=pivotAmount;
            newPosition.x = Mathf.Round(newPosition.x);
            newPosition.y = Mathf.Round(newPosition.y);
            newPosition+=pivotAmount;
            return newPosition;
        }

        public static void SetCurrentToyData(ToyData ToyData)
        {
            _ObjectBuilder.currentToyData = ToyData;
        }
        
        public void ToggleSnap()
        {
            isSnap = !isSnap;
        }
    }
}
