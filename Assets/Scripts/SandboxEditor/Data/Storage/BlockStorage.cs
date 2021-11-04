using SandboxEditor.Data.Block;
using SandboxEditor.Data.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    public class BlockStorage : MonoBehaviour
    {
        private BlocksData _blocksData;
        private static BlockStorage _BlockStorage;
        public static int Count => _BlockStorage._blocksData.Count;
        public static BlocksData BlocksData => _BlockStorage._blocksData;

        public GameObject collisionDetectorBlock;
        public GameObject DisplayBlock;
        public GameObject TouchInputBlock;
        public GameObject ToyDestroyBlock;
        public GameObject VelocitySetterBlock;

        private void Awake()
        {
            SetSingletonIfUnset();
            _blocksData = new BlocksData();
        }

        private void SetSingletonIfUnset()
        {
            _BlockStorage ??= this;
        }

        private static BlockStorage GetSingleton()
        {
            return _BlockStorage;
        }

        public static void AddBlockData(BlockData blockData)
        {
            GetSingleton()._BlockData(blockData);
        }

        private void _BlockData(BlockData blockData)
        {
            _blocksData.Add(blockData);
        }

        public static BlocksData GetBlocksData()
        {
            return GetSingleton()._blocksData;
        }
    }
}