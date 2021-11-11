using System;
using System.Collections.Generic;
using SandboxEditor.Block;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Sandbox;
using SandboxEditor.Data.Storage;
using Tools;
using UnityEngine;

namespace SandboxEditor.Builder
{
    public class BlockBuilder : MonoBehaviour
    {
        private GameObject selectedBlockPrefab;
        public static BlockBuilder _BlockBuilder;
        public static GameObject BlockRoot => Sandbox.RootOfBlock;
        
        public GameObject collisionDetectorBlock;
        public GameObject displayBlock;
        public GameObject toyDestroyerBlock;
        public GameObject velocitySetterBlock;
        public GameObject touchInputBlock;
        public GameObject accelerationBlock;
        public GameObject audioBlock;
        
        private void Awake()
        {
            _BlockBuilder = this;
        }

        public static void SetBlockPrefab(GameObject blockPrefab)
        {
            _BlockBuilder.selectedBlockPrefab = blockPrefab;
        }

        public static void CreateSelectedBlockAndAddOnStorage(Vector3 position)
        {
            _BlockBuilder._CreateSelectedBlock(position);
            
        }

        private void _CreateSelectedBlock(Vector3 position)
        {
            if (selectedBlockPrefab == null) return;
            var blockGameObject = Instantiate(selectedBlockPrefab);
            AddBlockOnBlockStorage(blockGameObject);
            AdjustBlockPosition(position, blockGameObject);
        }

        private static void AddBlockOnBlockStorage(GameObject block)
        {
            BlockStorage.AddBlock(block.GetComponent<AbstractBlock>());
        }

        private static void AdjustBlockPosition(Vector3 position, GameObject blockGameObject)
        {
            Misc.SetChildAndParent(blockGameObject, BlockRoot);
            position.z = 0;
            blockGameObject.transform.position = position;
        }

        public static (GameObject, Dictionary<int, GameObject>) CreateBlockRootAndUpdateBlockStorage(BlocksData blocksData)
        {
            var blockIdAndGameObjectPair = new Dictionary<int, GameObject>();
            var blockRoot = new GameObject("BlockRoot");
            foreach (var block in blocksData.blocksData)
            {
                var newBlock = CreateBlockAndAddOnStorage(block);
                Misc.SetChildAndParent(newBlock, blockRoot);
                blockIdAndGameObjectPair.Add(block.blockInstanceID, newBlock);
            }

            return (blockRoot, blockIdAndGameObjectPair);
        }

        public static GameObject CreateBlockAndAddOnStorage(BlockData blockData)
        {
            GameObject blockGameObject;
            switch (blockData)
            {
                case VelocitySetterBlockData velocitySetterBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.velocitySetterBlock);
                    break;
                case ToyDestroyerBlockData toyDestroyerBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.toyDestroyerBlock);
                    break;
                case TouchInputBlockData touchInputBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.touchInputBlock);
                    break;
                case DisplayBlockData displayBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.displayBlock);
                    break;
                case CollisionDetectorData collisionDetectorData:
                    blockGameObject = Instantiate(_BlockBuilder.collisionDetectorBlock);
                    break;
                case AccelerationBlockData accelerationBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.accelerationBlock);
                    break;
                case AudioBlockData audioBlockData:
                    blockGameObject = Instantiate(_BlockBuilder.audioBlock);
                    break;
                default:
                    return null;
            }

            var block = blockGameObject.GetComponent<AbstractBlock>();
            block.LoadBlockData(blockData);
            BlockStorage.AddBlock(block);
            return blockGameObject;
        }
        
    }

}