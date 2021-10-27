using UnityEngine;

namespace GameEditor.EventEditor.Block
{
    public class ToyComponent : AbstractBlock
    {
        protected override void Start(){
            base.Start();
            GetMessage("Block");
        }

        public override void GetMessage(string message)
        {
            MyDelegate md;
            switch(message){
                case "Block":
                    md = (GameObject obj) => {
                        Rigidbody2D body = obj.AddComponent<Rigidbody2D>();
                        body.bodyType = RigidbodyType2D.Kinematic;
                        Collider2D col = obj.GetComponent<BoxCollider2D>();
                        if(col == null) {col = obj.AddComponent<BoxCollider2D>();}
                        PhysicsMaterial2D mat = new PhysicsMaterial2D("Bouncer");
                        mat.bounciness = 1f;
                        mat.friction = 0f;
                        col.sharedMaterial = mat;
                    };
                    Debug.Log("Block button Click");
                    break;
                case "Ball":
                    md = (GameObject obj) => {
                        Rigidbody2D body = obj.AddComponent<Rigidbody2D>();
                        body.bodyType = RigidbodyType2D.Dynamic;
                        CircleCollider2D col = obj.GetComponent<CircleCollider2D>();
                        if(col == null) {col = obj.AddComponent<CircleCollider2D>();}
                        col.radius *= 1.05f;
                        PhysicsMaterial2D mat = new PhysicsMaterial2D("Bouncer");
                        mat.bounciness = 1f;
                        mat.friction = 0f;
                        col.sharedMaterial = mat;
                        //처음 날아가는 부분. 나중에 따로 구현할 것
                        body.AddForce(new Vector2(200f,200f));
                        //
                    };
                    break;
                default:
                    md = (GameObject obj)=>{};
                    break;
            }
            SetAddComponentMethod(md);
        }

    }
}
