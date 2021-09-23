namespace GameEditor.Data
{
    public class ImageData : ResourceData
    {
        // Resources 폴더를 Root로 하는 경로 입력.
        // 예를들어 /Asset/Resources/Common/ABC.png 의 경우
        // "Common/ABC" 를 값으로 가진다.
        public string TexturePath;

        public ImageData(string tp)
        {
            TexturePath = tp;
        }
    }
}