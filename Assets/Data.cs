using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Data
{
    public string xVal;
    public string yVal;

    public Data(float xData, float yData)
    {
        xVal = xData.ToString();
        yVal = yData.ToString();
    }

    //public string allValues = "X-value: " + xVal + "Y-Value: " + yVal;
}
