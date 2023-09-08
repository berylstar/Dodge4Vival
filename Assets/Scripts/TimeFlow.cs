using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeFlow : MonoBehaviour
{
    bool IsRunning = true;
    public TMP_Text TimeTxt;
    float alive = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( IsRunning )
        {
            alive += Time.deltaTime;
            TimeTxt.text = alive.ToString("N2");
        }
    }
}
