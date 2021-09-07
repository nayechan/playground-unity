using UnityEngine;
using UnityEngine.Events;


/*
Toy, Block, Line 의 정보를 일괄 관리하는 스크립트입니다.
*/
namespace GameEditor
{
    public enum ObjectType
    {
        // 일반 오브젝트
        Toy,
        // 기능성 블럭
        Mover,
        Zone,
        // Line
        Line
    }

    public enum ColliderType
    {
        Box,
        Circle
    }

    [System.Serializable]
    public class ObjectInfo
    {
        // 기본 설정값
        //Transform
        public Vector3 position = Vector3.zero, rotation = Vector3.zero, scale = Vector3.one;
        //General Settings
        public bool movable = false, collidable = true;
        //Physics Material
        public float friction = 0.4f, bounciness = 0f;
        //SpriteRenderer
        public string texturePath = "Common/Brown Stony";
        public Color color = Color.white;
        public float pixelsPerUnit = 100f;
        public bool visible = true;
        //Collider
        public ColliderType colType = ColliderType.Box;
        public float colRadius = 0.5f;
        public Vector2 colSize = Vector2.one;
        public bool isTrigger = false;
        //RigidBody
        public float weight = 1f, gravity = 1f;
        
        string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        ObjectInfo FromJson(string json)
        {
            return JsonUtility.FromJson<ObjectInfo>(json);
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