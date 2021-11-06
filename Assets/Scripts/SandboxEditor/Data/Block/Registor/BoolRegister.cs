using System;

namespace SandboxEditor.Data.Block.Register
{
    public class BoolRegister : AbstractRegister
    {
        public bool Data
        {
            get => (bool) data;
            private set => Data = value;
        }

        public BoolRegister()
        {
            data = false;
        }

        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case BoolRegister anotherBoolRegister:
                    Data = anotherBoolRegister.Data || Data;
                    break;
                case VectorRegister anotherVectorRegister:
                    Data = anotherVectorRegister.Data != 0f || Data;
                    break;
                case ToyRegister anotherToyRegister:
                    Data = anotherToyRegister.Data != null || Data;
                    break;
                case null:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}