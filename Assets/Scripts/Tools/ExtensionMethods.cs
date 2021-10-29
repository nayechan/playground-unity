using GameEditor.Data;
using UnityEngine;

namespace Tools
{
    public static class ExtensionMethods
    {
        public static T Clone<T>(this T toyDataToClone)
        {
            var jsonToyData = JsonUtility.ToJson(toyDataToClone, true);
            return JsonUtility.FromJson<T>(jsonToyData);
        }
    }
}