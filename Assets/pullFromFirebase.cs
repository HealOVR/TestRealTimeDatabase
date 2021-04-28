using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class pullFromFirebase : MonoBehaviour
{
    public Text text1;
    public Text text2;
    Data data;

    public SerialPort serial = new SerialPort("COM7", 9600);
    

    // Update is called once per frame
    void Update()
    {
        pullFirebase();
        Vector2 axisValue;
        //axisValue.x = data.xVal;
        axisValue.x = (float)Convert.ToDouble(data.xVal);
        axisValue.y = (float)Convert.ToDouble(data.yVal);
        //axisValue.y = data.yVal;

        //Inititalize Condition 
        text1.text = "Nothing Done";
        text2.text = "Nothing Done";
        if (serial.IsOpen == false)
        {
            serial.Open();
        }
        //serial.Write("");

        if (axisValue.y >= .3 && axisValue.x > -.3 && axisValue.x < .3)
        {
            text1.text = "Y up";
            //text2.text = data.yVal;
            if (serial.IsOpen == false)
            {
                serial.Open();
            }
            serial.Write("2");
        }
        else if (axisValue.y < -.3 && axisValue.x > -.3 && axisValue.x < .3)
        {
            text1.text = "Y down";
            //text2.text = data.yVal;
            if (serial.IsOpen == false)
            {
                serial.Open();
            }
            serial.Write("3");
        }


        else if (axisValue.x > .3 && axisValue.y > -.3 && axisValue.y < .3)
        {
            //text1.text = data.xVal;
            text2.text = "X up";
            if (serial.IsOpen == false)
            {
                serial.Open();
            }
            serial.Write("5");
        }


        else if (axisValue.x < -.3 && axisValue.y > -.3 && axisValue.y < .3)
        {
            //text1.text = data.xVal;
            text2.text = "X down";
            if (serial.IsOpen == false)
            {
                serial.Open();
            }
            serial.Write("6");
        }
    }

    public void pullFirebase()
    {
        
        string output;
        RestClient.Get<Data>("https://testproject-d51e9-default-rtdb.firebaseio.com/" + "Data:" + ".json").Then(response => 
        {
            data = response;
            //text1.text = data.xVal;
            //text2.text = data.yVal;
        });
    }
}
