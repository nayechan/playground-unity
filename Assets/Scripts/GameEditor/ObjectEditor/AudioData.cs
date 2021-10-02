using UnityEngine;

public class AudioData
{
    //private string uuid;
    private string _path;
    private string _type;
    private AudioClip _audioClip;
    public AudioData(string path, string type)
    {
        _path = path;
        _type = type;
        _audioClip = null;
    }

    public AudioClip GetAudioClip()
    {
        return _audioClip;
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        _audioClip = audioClip;
    }

    public string GetPath()
    {
        return _path;
    }

    public void SetPath(string path)
    {
        _path = path;
    }

    public string GetType()
    {
        return _type;
    }
}