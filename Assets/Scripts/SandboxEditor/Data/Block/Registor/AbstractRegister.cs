
namespace SandboxEditor.Data.Block.Register
{
    // 블럭간 값 전달에 사용하는 클래스로 전달하는 값의 조합별로 받는 동작을 구현합니다.
    // 현재 구조로는 입력포트에 2개의 신호가 들어가는 경우 교란이 생길 수 있습니다.
    // 추후 큐 기반의 신호처리로 변환해야 합니다.
    public abstract class AbstractRegister
    {
        public object data;

        protected AbstractRegister()
        {
            InitializeData();
        }
        
        // 레지스터에 한 프레임에 두번이상의 ReceiveData가 호출되는 걸 고려해서 구현할 것.
        public abstract void ReceiveData(AbstractRegister FromThisRegister);

        public abstract void InitializeData();
    }
}