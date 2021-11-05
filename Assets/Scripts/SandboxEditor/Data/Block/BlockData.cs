using System;
using System.Collections.Generic;
using SandboxEditor.NewBlock;
using UnityEngine;

namespace SandboxEditor.Data.Block
{
    [Serializable]
    public abstract class BlockData
    {
        public int blockInstanceID;
        public Vector3 position;

        public void SetgameObjectIDAndPosition(AbstractBlock block)
        {
            blockInstanceID = block.gameObject.GetInstanceID();
            position = block.transform.position;
        }
        public void ApplyDataToBlock(AbstractBlock block)
        {
            block.LoadBlockData(this);
        }
    }

    [Serializable]
    public class BlocksData : ISerializationCallbackReceiver
    {
        [NonSerialized]
        public List<BlockData> blocksData = new List<BlockData>();
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