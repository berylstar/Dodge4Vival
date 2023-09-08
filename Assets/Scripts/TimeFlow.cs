using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlow : MonoBehaviour
{
    private bool _isRunning = true;
    public TMP_Text timeTxt;
    private float _alive = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( _isRunning )
        {
            _alive += Time.deltaTime;
            timeTxt.text = _alive.ToString("N2");
        }
    }
}
