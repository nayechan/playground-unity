using System;

namespace SandboxEditor.Data.Block.Register
{
    public class VectorRegister: AbstractRegister
    {
        public float Data
        {
            get => (float) data;
            private set => Data = value;
        }

        public VectorRegister()
        {
            Data = 0f;
        }
        
        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case BoolRegister anotherBoolRegister:
                    var receive = anotherBoolRegister.Data ? 1f : 0f;
                    if (receive >= Data)
                        Data = receive;
                    break;
                case VectorRegister anotherVectorRegister:
                    if(anotherVectorRegister.Data >= Data)
                        Data = anotherVectorRegister.Data;
                    break;
                case ToyRegister _:
                    // Wrong Connection
                    break;
                case null:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}