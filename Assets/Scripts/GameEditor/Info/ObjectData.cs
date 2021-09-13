using UnityEngine;
using UnityEngine.Serialization;


/*
Toy, Block, Line 의 정보를 일괄 관리하는 스크립트입니다.
*/
namespace GameEditor.Info
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
    public class IFObject
    {
        // 기본 설정값
        //Transform
        public IFTransform ifTf;
        public IFCollider ifCol;
        public IFRigidBody ifRb;
        public IFSpriteRenderer ifSr;
        public IFPhysicsMaterial ifPm;
        // Exp

        public IFObject()
        {
            ifTf = new IFTransform();
            ifCol = new IFCollider();
            ifRb = new IFRigidBody();
            ifSr = new IFSpriteRenderer();
            ifPm = new IFPhysicsMaterial();
        }
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        static public IFObject FromJson(string json)
        {
            return JsonUtility.FromJson<IFObject>(json);
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