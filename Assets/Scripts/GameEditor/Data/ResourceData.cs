using UnityEngine;

namespace GameEditor.Data
{
    // 이미지 경로, 오디오 경로 등 ResourceData의 정보를 저장하는 클래스입니다.
    // 경로는 Application.persistantPath로부터의 상대경로를 저장합니다.
    // 따라서 이미지를 Import할때 Application.persistantPath로 복사가 필요합니다.
    public abstract class ResourceData
    {
        public string name;
        public string path;
        
        

    }
}