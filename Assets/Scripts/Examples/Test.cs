using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<AudioClip> ac;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var clip in Resources.LoadAll<AudioClip>(File.BackgroundMusicPath))
            ac.Add(clip);
        foreach(var clip in Resources.LoadAll<AudioClip>(File.EffectSoundPath))
            ac.Add(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

}

public class GO
{
    public List<HAHA> list = new List<HAHA>();
}

public class HAHA
{
    public int a;
}

