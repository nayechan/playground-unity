using System.Collections.Generic;
using SandboxEditor.Data;
using SandboxEditor.InputControl.InEditor.Sensor;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class CollisionInEveryFrame : MonoBehaviour
    {
        private List<Collision2D> collisions2D;
        private static CollisionInEveryFrame _CollisionInEveryFrame;
        private static IEnumerable<Collision2D> Collisions2D => _CollisionInEveryFrame.collisions2D;
        private Dictionary<GameObject, GameObject> hitToyAndOther;
        static public Dictionary<GameObject, GameObject> HitToyAndOther => _CollisionInEveryFrame.hitToyAndOther;
        private void Awake()
        {
            _CollisionInEveryFrame = this;
            collisions2D = new List<Collision2D>();
            hitToyAndOther = new Dictionary<GameObject, GameObject>();
        }

        public static void RecordeToyCollision()
        {
            foreach (var collision in Collisions2D)
            {
                if (collision.gameObject == null) continue;
                var hitGameObject = collision.gameObject;
                var hitPort = hitGameObject.GetComponentInChildren<BlockPort>();
                var otherGameObject = collision.otherCollider.gameObject;
                var otherPort = otherGameObject.GetComponentInChildren<BlockPort>();
                if(IsToySender(hitPort) && !HitToyAndOther.ContainsKey(hitGameObject))
                    HitToyAndOther.Add(hitGameObject, otherGameObject);
                if(IsToySender(otherPort) && !HitToyAndOther.ContainsKey(otherGameObject))
                    HitToyAndOther.Add(otherGameObject, hitGameObject);
            }
        }

        private static bool IsToySender(BlockPort port)
        {
            if (port == null) return false;
            return port.Type == PortType.ToySender;
        }

        public static void AddCollision2D(Collision2D collision2D)
        {
            if(SandboxPhaseChanger.IsGameStarted)
                _CollisionInEveryFrame.collisions2D.Add(collision2D);
        }

        public static void RenewCollisions2D()
        {
            _CollisionInEveryFrame.collisions2D = new List<Collision2D>();
            _CollisionInEveryFrame.hitToyAndOther = new Dictionary<GameObject, GameObject>();
        }
    }
}