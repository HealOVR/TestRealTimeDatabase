using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class pushFirebase : MonoBehaviour
{

    public Text text;
    private float xVal = 0f;
    private float yVal = 0f;

    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Hello";
        TryInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }

        targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 axisValue);
        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggered);
        Debug.Log(axisValue);
        
        Data data = new Data(axisValue.x, axisValue.y);
        postFirebase(data);
    }

    public void postFirebase(Data data)
    {
        //Data data = new Data(xVal, yVal);
        RestClient.Put("https://testproject-d51e9-default-rtdb.firebaseio.com/"+"Data:"+".json",data);
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
