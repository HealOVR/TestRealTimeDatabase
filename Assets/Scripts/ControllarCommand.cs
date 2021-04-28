using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.IO;
using System.IO.Ports;

public class ControllarCommand : MonoBehaviour
{

    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    public SerialPort serial = new SerialPort("COM7", 9600);

    bool state = false;

    //ArduinoTest arduino = new ArduinoTest();
    // Start is called before the first frame update
    void Start()
    {

        TryInitialize();
        /* List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        Debug.Log(devices.Count);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 axisValue);
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggered);
            Debug.Log(axisValue);

            if (axisValue.y >= .3 && axisValue.x > -.3 && axisValue.x < .3)
            {
                Debug.Log("Servo Increasing Angle");
                if (serial.IsOpen == false)
                {
                    serial.Open();
                }
                serial.Write("2");
            }
            else if (axisValue.y < -.3 && axisValue.x > -.3 && axisValue.x < .3)
            {
                Debug.Log("Servo Decreasing Angle : ");
                if (serial.IsOpen == false)
                {
                    serial.Open();
                }
                serial.Write("3");
            }


            else if (axisValue.x > .3 && axisValue.y > -.3 && axisValue.y < .3)
            {
                Debug.Log("Servo Increasing Angle");
                if (serial.IsOpen == false)
                {
                    serial.Open();
                }
                serial.Write("5");
            }


            else if (axisValue.x < -.3 && axisValue.y > -.3 && axisValue.y < .3)
            {
                Debug.Log("Servo Decreasing Angle");
                if (serial.IsOpen == false)
                {
                    serial.Open();
                }
                serial.Write("6");
            }

            if(triggered > .7)
            {
                serial.Write("4");
                serial.Write("7");
                if(state == false)
                {
                    serial.Write("1");
                    state = !state;
                    Debug.Log("Serial Write = 1");
                }
                else
                {
                    serial.Write("0");
                    state = !state;
                }
                

            }
        }
        
      
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        Debug.Log(devices.Count);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
}
