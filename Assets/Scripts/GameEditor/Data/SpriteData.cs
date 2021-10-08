using System;

namespace GameEditor.Data
{
    [Serializable]
    public class SpriteData : ResourceData
    {
        // 디바이스상의 이미지 Path를 리스트형태로 저장한다.
        // ****** ImageData 클래스로 대체되어 사용되지 않을 예정 ******
        public string TexturePath;
        public float width;
        public float height;

        public SpriteData(string tp, float width, float height)
        {
            TexturePath = tp;
            this.width = width;
            this.height = height;
        }
    }
}