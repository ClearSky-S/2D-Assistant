using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var currTime = DateTime.Now;
        timeText.text = currTime.ToString("h:mm");
    }
}
