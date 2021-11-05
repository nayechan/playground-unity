using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class BlockController : MonoBehaviour
    {
        public static BlockController _blockController;
        
        public static void OnEveryFixedUpdate()
        {
            foreach(var block in BlockStorage.Blocks)
                block.OnEveryFixedUpdate();
        }

        public static void WhenGameStart()
        {
            foreach(var block in BlockStorage.Blocks)
                block.WhenGameStart();
        }
    }
}