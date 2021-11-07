using System;
using SandboxEditor.Data.Toy;
using UnityEngine;

namespace SandboxEditor.Data.Resource
{
    [Serializable]
    public class AudioData
    {
        //private string uuid;
        public string fileName;
        public string type;
        public AudioClip audioClip;

        public AudioData() {}

        public AudioData(string fileName, string type)
        {
            this.fileName = fileName;
            this.type = type;
        }
        
    }
}