using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    float aspectRatio;
    float size;

    public Transform player;

    [SerializeField]
    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        SetTheScreenSize();
    }

    void LateUpdate()
    {

        Follow();
    }

    private void SetTheScreenSize()
    {
        aspectRatio = (float)Screen.height / (float)Screen.width;
        if (aspectRatio > (16.0f / 9.0f))
        {
            size = aspectRatio * 3f;
            gameObject.GetComponent<Camera>().orthographicSize = size;
        }

    }

    private void Follow()
    {
        Vector3 temporary = transform.position;
        temporary.x = player.position.x;
        temporary.y = player.position.y;

        transform.position = temporary;

        transform.position = new Vector3(Mathf.Clamp(temporary.x, minX, maxX), Mathf.Clamp(temporary.y, minY, maxY), transform.position.z);
    }
}
