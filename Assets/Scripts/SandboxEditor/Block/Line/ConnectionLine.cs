using System;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Line
{
    public sealed class ConnectionLine : MonoBehaviour
    {
        private AbstractSensor source;
        private AbstractSensor destination;
        public LineRenderer lineRenderer;
        private bool isSet = false;

        private void Awake()
        {
            var lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            if (isSet)
                ReRender();
        }

        public void SetLine(AbstractSensor source, AbstractSensor destination)
        {
            isSet = true;
            this.source = source;
            this.destination = destination;
        }

        private void ReRender()
        {
            lineRenderer.enabled = false;
            if (!isPortOnHide(source) && !isPortOnHide(destination)) return;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, source.transform.position);
            lineRenderer.SetPosition(1, destination.transform.position);
        }

        private static bool isPortOnHide(Component port)
        {
            return port.gameObject.activeSelf;
        }
    }
}
