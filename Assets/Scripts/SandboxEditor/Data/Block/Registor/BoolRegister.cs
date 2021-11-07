using System;

namespace SandboxEditor.Data.Block.Register
{
    public class BoolRegister : AbstractRegister
    {
        public bool Data
        {
            get => (bool) data;
            private set => data = value;
        }

        public override void ReceiveData(AbstractRegister FromThisRegister)
        {
            switch (FromThisRegister)
            {
                case BoolRegister anotherBoolRegister:
                    data = anotherBoolRegister.Data;
                    break;
                case VectorRegister anotherVectorRegister:
                    data = anotherVectorRegister.Data != 0f;
                    break;
                case ToyRegister anotherToyRegister:
                    Data = anotherToyRegister.Data != null;
                    break;
                case null:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override void InitializeData()
        {
            data = false;
        }
    }
}