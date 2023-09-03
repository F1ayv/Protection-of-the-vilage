using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private Vector3 _startPos;
    private float _lastSize;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _lastSize = 0.9f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Abs(_lastSize - (Screen.height * (16f / 9)) / Screen.width) < 0.001f)
            return;
        
        Debug.Log((Screen.height * (16f / 9)) / Screen.width);
        if ((Screen.height * (16f / 9)) / Screen.width > 0.9f)
        {
            _lastSize = (Screen.height * (16f / 9)) / Screen.width;
            float size = Screen.height * (16f / 9) / Screen.width-0.9f;
            transform.position = new Vector3(transform.position.x, _startPos.y + size * 90,
                _startPos.z - size * 30);
        }
        else
            transform.position = _startPos;
    }
}
