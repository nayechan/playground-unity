using System;

namespace GameEditor.Data
{
    [Serializable]
    public class SpriteData : ResourceData
    {
        // 디바이스상의 이미지 Path를 리스트형태로 저장한다.
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