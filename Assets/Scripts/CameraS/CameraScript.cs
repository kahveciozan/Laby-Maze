using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float aspectRatio;
    float size;

    void Awake()
    {
        SetTheScreenSize();
    }

    private void SetTheScreenSize()
    {
        aspectRatio = (float)Screen.height / (float)Screen.width;
        if (aspectRatio > (16.0f / 9.0f))
        {
            size = aspectRatio * 2.9f;
            gameObject.GetComponent<Camera>().orthographicSize = size;
        }

    }
}
