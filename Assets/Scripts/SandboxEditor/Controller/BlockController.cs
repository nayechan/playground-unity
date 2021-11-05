using SandboxEditor.Data.Storage;
using UnityEngine;

namespace GameEditor.EventEditor.Controller
{
    public class BlockController : MonoBehaviour
    {
        public static BlockController _blockController;
        
        public static void BlockAction()
        {
            foreach(var block in BlockStorage.Blocks)
                block.OnEveryFixedUpdate();
            Debug.Log("block FixedFrame Passed");
        }

        public static void BlockActionWhenGameStart()
        {
            foreach(var block in BlockStorage.Blocks)
                block.WhenGameStart();
        }
    }
}