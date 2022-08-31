using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTeller : MonoBehaviour
{
    public GameObject timeTextObject;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTime", 0f, 10f);
    }


    void UpdateTime()
    {
        DateTime utcTime = DateTime.UtcNow;
        TimeZoneInfo japanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
        DateTime japanTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, japanTimeZone);
        timeTextObject.GetComponent<TextMeshPro>().text = japanTime.ToString("h:mm tt");
    }
}
