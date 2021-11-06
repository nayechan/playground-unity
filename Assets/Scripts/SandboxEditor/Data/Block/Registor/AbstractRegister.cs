
namespace SandboxEditor.Data.Block.Register
{
    // 블럭간 값 전달에 사용하는 클래스로 전달하는 값의 조합별로 받는 동작을 구현합니다.
    public abstract class AbstractRegister
    {
        protected object data;
        
        // 레지스터에 한 프레임에 두번이상의 ReceiveData가 호출되는 걸 고려해서 구현할 것.
        public abstract void ReceiveData(AbstractRegister FromThisRegister);
    }
}