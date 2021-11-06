using System;
using UnityEngine;

namespace SandboxEditor.Data.Block.Register
{
    public class ToyRegister : AbstractRegister
    {
        public GameObject Data
        {
            get => (GameObject) data;
            private set => Data = value;
        }
        
        public ToyRegister()
        {
            data = null;
        }
        
        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case ToyRegister anotherToyRegister:
                    data ??= anotherToyRegister.Data;
                    break;
                case BoolRegister _:
                    break;
                case VectorRegister _:
                    break;
                case null:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}