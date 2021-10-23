using UnityEngine;

namespace GameEditor.GameEditorMain
{
    public class GameEditorBasicComponent : MonoBehaviour
    {
        private static GameEditorBasicComponent _GEBC;

        private void Awake() {
            DontDestroyOnLoad(gameObject);   
            if(_GEBC == null) _GEBC = this;
        }

        public static GameEditorBasicComponent GetGEBC(){
            return _GEBC;
        }

        public GameObject GetLoot(){
            if(_GEBC == null){
                return null;
            }
            return _GEBC.gameObject;
        }
    }
}
