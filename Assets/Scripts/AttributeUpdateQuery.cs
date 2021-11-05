using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class AttributeUpdateQuery{
    public AttributeUpdateQuery(
        KeyData Key, AttributeUpdateData AttributeUpdates
    )
    {
        this.Key = Key;
        this.AttributeUpdates = AttributeUpdates;
    }
    
    [Serializable]
    public class AttributeUpdateData{
        [Serializable]
        public class VoteData{
            [SerializeField] string Action;
            [SerializeField] int Value;
            public VoteData(string Action, int Value)
            {
                this.Action = Action;
                this.Value = Value;
            }
        }
        public AttributeUpdateData(VoteData voteData)
        {
            upvote = voteData;
        }
        [SerializeField] VoteData upvote;
    }
    [Serializable]
    public class KeyData{
        [SerializeField] string dataType;
        [SerializeField] string gameId;
        public KeyData(string dataType, string gameId)
        {
            this.dataType = dataType;
            this.gameId = gameId;
        }
    }
    [SerializeField] AttributeUpdateData AttributeUpdates;
    [SerializeField] KeyData Key;

}