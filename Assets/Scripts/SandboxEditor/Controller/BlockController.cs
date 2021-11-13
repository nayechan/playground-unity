using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class BlockController : MonoBehaviour
    {
        public static BlockController _blockController;
        
        public static void OnEveryFixedUpdateWhenPlaying()
        {
            foreach(var block in BlockStorage.Blocks)
                block.OnEveryFixedUpdateWhenPlaying();
        }

        public static void WhenBegin()
        {
            foreach(var block in BlockStorage.Blocks)
                block.WhenGameStart();
        }
        
        public static void WhenBackToEditor()
        {
            foreach(var block in BlockStorage.Blocks)
                block.WhenBackToEditor();
        }
    }
}