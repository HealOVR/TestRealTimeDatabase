using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class formRegistry : MonoBehaviour
{
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    [SerializeField]
    private string BaseURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfO7ExTbUAt3yblEGpZryQZlg53F4IZE8UWz7GTy8jjPWIJlQ/formResponse";

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
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

            StartCoroutine(Post(axisValue.x, axisValue.y));
        }
    }

    IEnumerator Post(float x, float y)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1464834321", x.ToString());
        form.AddField("entry.701621815", y.ToString());
        byte[] rawData = form.data;

        Debug.Log("X = "+x+" Y = "+y);

        WWW www = new WWW(BaseURL, rawData);
        yield return www;
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
