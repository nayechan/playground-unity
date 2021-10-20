using System;
using GameEditor.Data;
using UnityEngine;

public class AudioData
{
    //private string uuid;
    private string _path;
    private string _type;
    private AudioClip _audioClip;
    [NonSerializedAttribute] public ToyData toyData;

    public AudioData() {}

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

    public void SetRelativePath(string path)
    {
        _path = path;
    }

    public string GetAudioType()
    {
        return _type;
    }
}