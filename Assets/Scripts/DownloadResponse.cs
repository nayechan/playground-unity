using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DownloadResponse : Response{
    [SerializeField] string attachment;

    public string getAttachmentPath(){
        return attachment;
    }
}