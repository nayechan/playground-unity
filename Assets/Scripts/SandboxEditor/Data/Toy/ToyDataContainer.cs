using UnityEngine;

namespace GameEditor.Data
{
    public class ToyDataContainer : MonoBehaviour
    {
        public ToyData toyData;
        public ImageData ImageData { get {return toyData.imageData;}}
        public ToyBuildData ToyBuildData { get {return toyData.toyBuildData;}}

    }
}
