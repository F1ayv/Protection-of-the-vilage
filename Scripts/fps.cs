using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps : MonoBehaviour
{
    public TextMeshProUGUI textFps;
    public TextMeshProUGUI textFps2;
 
    // Update is called once per frame
    private float _timer = 0.1f;
    private float _time;
    private float _timer2 = 5f;
    private float _time2;
    private int _value;
    public int fpS = 123;
    void Start()
    {
       // Application.targetFrameRate = 60;
    }
    
    void Update()
    {
        
        Application.targetFrameRate = fpS;  // убрать
        _value++;
        _time += Time.deltaTime;
        _time2+= Time.deltaTime;
        
        if (_time >= _timer)
        {
            _time = 0;
            textFps.text = ((int)(1 / Time.deltaTime)).ToString();
        }
        
        if (_time2 >= _timer2)
        {
            textFps2.text = ((int)(_value/_time2)).ToString();
            _time2 = 0;
            _value = 0;
        }
    }
}
