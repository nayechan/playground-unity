using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class BaseQuery{
    public BaseQuery(KeyCondition keyCondition)
    {
        this.keyCondition = keyCondition;
    }

    [SerializeField] KeyCondition keyCondition;

    [Serializable]
    public class KeyCondition{
        public KeyCondition(QueryData<string> dataType, QueryData<string> gameId)
        {
            this.dataType = dataType;
            this.gameId = gameId;
        }
        [SerializeField] QueryData<string> dataType;
        [SerializeField] QueryData<string> gameId;
    }

}