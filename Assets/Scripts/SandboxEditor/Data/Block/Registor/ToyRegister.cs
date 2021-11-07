using System;
using UnityEngine;

namespace SandboxEditor.Data.Block.Register
{
    public class ToyRegister : AbstractRegister
    {
        public GameObject Data
        {
            get => (GameObject) data;
            private set => data = value;
        }
        
        // base Constructor는 자동으로 호출된다.
        public ToyRegister(GameObject toy)
        {
            data = toy;
        }
        
        public ToyRegister() { }
        
        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case ToyRegister anotherToyRegister:
                    data = anotherToyRegister.Data;
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

        public override void InitializeData()
        {
            Data = null;
        }
    }
}