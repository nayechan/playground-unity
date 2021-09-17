using UnityEngine;

namespace GameEditor.Data
{
    public interface ComponentData
    {
        // 인자로 받은 오브젝트의 컴포넌트를 Data의 값으로 Set합니다.
        public void SetComponent(GameObject obj);
        // 오브젝트의 Component를 복사해 Data파일을 생성하는 생성자입니다.
        public Component AddComponent(GameObject obj);
        // Data를 받은 오브젝트의 값대로 업데이트시켜주는 메소드입니다.
        public void SetData(GameObject obj);
    }
}