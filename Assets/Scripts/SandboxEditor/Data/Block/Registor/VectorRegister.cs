using System;

namespace SandboxEditor.Data.Block.Register
{
    public class VectorRegister: AbstractRegister
    {
        public float Data
        {
            get => (float) data;
            private set => data = value;
        }
        
        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case BoolRegister anotherBoolRegister:
                    var receive = anotherBoolRegister.Data ? 1f : 0f;
                    data = receive;
                    break;
                case VectorRegister anotherVectorRegister:
                    data = anotherVectorRegister.Data;
                    break;
                case ToyRegister _:
                    break;
                case null:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void InitializeData()
        {
            Data = 0f;
        }
    }
}