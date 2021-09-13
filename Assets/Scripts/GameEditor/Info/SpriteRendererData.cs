using UnityEngine;
using UnityEditor;

namespace GameEditor.Info
{
    [System.Serializable]
    public class SpriteRendererData
    {
        
        public string texturePath = "Common/Brown Stony";
        public Color color = Color.white;
        public bool visible = true;
        
         public void SetComponent(GameObject obj)
         {
             SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
             if (!sr)
             {
                 sr = obj.AddComponent<SpriteRenderer>();
             }
             sr.color = color;
             Texture2D tex = Resources.Load<Texture2D>(texturePath);
             if(tex == null) { Debug.Log("Can't find texture : " + texturePath);}
             sr.sprite = Sprite.Create(tex, new Rect(0f, 0f , tex.width, tex.height),
                 new Vector2(0.5f, 0.5f), tex.width);
         
             if(visible) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
         }   
         
         public static bool GetData(GameObject obj, SpriteRendererData srData)
         {
             SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
             if (sr is null)
             {
                 Debug.Log("ifSr is null");
                 return false;
             }

             srData.texturePath = AssetDatabase.GetAssetPath(sr);
             return true;
         }
    }
}