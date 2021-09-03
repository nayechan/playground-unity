using UnityEngine;
using UnityEngine.Events;

namespace GameEditor
{
    public enum Type
    {
        General,
        Block,
        Zone
    }

    [System.Serializable]
    public class ObjectInfo
    {
        public Type type;
        public Vector3 _position, _rotation, _scale;
        public Transform _trans;
        // private bool _movable, _collidable, _hidden, _isTrigger;
        // private float _weight;

    }
    
    /* Transform
     * - Position (3V)
     * - Rotation (3V)
     * - Scale (3V)
     * Sprite Renderer
     * - Sprite
     * - Color
     * - Material
     * 
     * Rigidbody 2D's properties
     * - BodyType {Kinematic, Dynamic}
     * - Material (Physics Mat)
     *  - Friction (float)
     *  - Bounciness (float)
     * - Mass (float)
     * - Gravity Scale ( 1 : float)
     * Collider 2D's
     * - IsTrigger (bool)
     * - Offset (X, Y, Radius : float)
     * 
     */
}