using System;
using System.Collections.Generic;
using SandboxEditor.NewBlock;
using UnityEngine;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public class BlockData
    {
        public int blockInstanceID;
        public string blockType;
        public Vector3 position;
    }

    [Serializable]
    public class BlocksData
    {
        private List<BlockData> blocksData = new List<BlockData>();
        public List<string> types;
        public List<string> serializedData;

        public int Count => blocksData.Count;
        
        public void Add(BlockData blockData)
        {
            blocksData.Add(blockData);
        }
        
        public void OnBeforeSerialize()
        {
            serializedData = new List<string>();
            types = new List<string>();
            foreach(var blockData in blocksData)
            {
                serializedData.Add(JsonUtility.ToJson(blockData, true));
                types.Add(blockData.GetType().ToString());
            }
        }

        public void OnAfterDeserialize()
        {
            for(var i = 0; i < serializedData.Count ; ++i)
            {
                var type = Type.GetType(types[i]);
                blocksData.Add((BlockData)JsonUtility.FromJson(serializedData[i], type));
            }
        }

        public List<BlockData> GetToyComponentsData()
        {
            return blocksData;
        }
    }
}