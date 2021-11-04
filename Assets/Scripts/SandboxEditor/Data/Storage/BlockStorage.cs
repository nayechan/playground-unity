using System.Collections.Generic;
using SandboxEditor.Data.Block;
using SandboxEditor.Data.Toy;
using SandboxEditor.NewBlock;
using UnityEngine;

namespace SandboxEditor.Data.Storage
{
    public class BlockStorage : MonoBehaviour
    {
        private List<AbstractBlock> _blocks;
        private static BlockStorage _BlockStorage;
        public static int Count => _BlockStorage._blocks.Count;
        public static List<AbstractBlock> Blocks => _BlockStorage._blocks;

        private void Awake()
        {
            _BlockStorage ??= this;
            _blocks = new List<AbstractBlock>();
        }

        public static void AddBlock(AbstractBlock block)
        {
            _BlockStorage._AddBlock(block);
        }

        private void _AddBlock(AbstractBlock block)
        {
            _blocks.Add(block);
        }

        public static BlocksData GetLatestBlocksData (GameObject rootOfBlock)
        {
            var blocksData = new BlocksData();
            foreach (var block in rootOfBlock.GetComponentsInChildren<AbstractBlock>())
            {
                blocksData.Add(block.MakeBlockData()); 
            }
            return blocksData;
        }

        public static void RenewBlockList()
        {
            _BlockStorage._blocks = new List<AbstractBlock>();
        }
    }
}