using UnityEngine;
using UnityEngine.Events;

namespace GameEditor
{
    public enum ObjectType
    {
        // 일반 오브젝트
        General,
        // 기능성 블럭
        Mover,
        Zone
    }

    public enum ColliderType
    {
        Box,
        Circle
    }

    [System.Serializable]
    public class ObjectInfo
    {
        //Transform
        public Vector3 position, rotation, scale;
        //General Settings
        public bool movable, collidable;
        //Physics Material
        public float friction, bounciness;
        //SpriteRenderer
        public string texturePath;
        public Color color;
        public float pixelsPerUnit;
        public bool visible;
        //Collider
        public ColliderType colType;
        public float colRadius;
        public Vector2 colSize;
        public bool isTrigger;
        //RigidBody
        public float weight, gravity;
        // 기본 설정값
        public ObjectInfo()
        {
            position = rotation = Vector3.zero;
            scale = Vector3.one;
            movable = false; collidable = true; visible = true; isTrigger = false;
            color = new Color(1f, 1f, 1f, 1f);
            weight = gravity = 1f;    
        }
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