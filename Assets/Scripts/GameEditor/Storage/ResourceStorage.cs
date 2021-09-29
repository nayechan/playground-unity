using System;
using System.Collections.Generic;
using GameEditor.Data;
using UnityEngine;

namespace GameEditor.Storage
{
    public class ResourceStorage
    {
        private static ResourceStorage _rs;
        private Dictionary<string, Sprite> _dictSprites;
        private Dictionary<string, AudioClip> _dictAudioClips;
        private string _ap; 

        public static ResourceStorage GetRs() 
        {
            if (_rs != null)
            {
                return _rs;
            }
            _rs = new ResourceStorage
            {
                _ap = Application.persistentDataPath,
                _dictSprites = new Dictionary<string, Sprite>(),
                _dictAudioClips = new Dictionary<string, AudioClip>()
            };
            return _rs;
        }
        
        public Sprite GetSprite(string path)
        {
            _dictSprites.TryGetValue(path, out var res);
            return res;
        }
        
        public AudioClip GetAudioClips(string path)
        {
            _dictAudioClips.TryGetValue(path, out var res);
            return res;
        }

        // 경로를 읽어 Sprite를 사전에 추가하는 함수입니다.
        public void AddSprite(string path)
        {
             var tex = Resources.Load<Texture2D>(path);
             try
             {
                 var sp = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height),
                     new Vector2(0.5f, 0.5f), tex.width);
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 throw;
             }
             
             
        }
    }
}