using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Networking;

public class formRegistry : MonoBehaviour
{
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    [SerializeField]
    private string BaseURL = "https://testproject-d51e9-default-rtdb.firebaseio.com.json";

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
            //targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 axisValue);
            //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggered);
            Vector2 axisValue = new Vector2();
            axisValue.x = 3.7f;
            axisValue.y = 2.8f;
            StartCoroutine(Post(axisValue.x, axisValue.y));
        }
    }

     IEnumerator Post(float x, float y)
     {
         WWWForm form = new WWWForm();
         form.AddField("x-val", x.ToString());
         form.AddField("y-val", y.ToString());
         byte[] rawData = form.data;

         Debug.Log("X = "+x+" Y = "+y);

         WWW www = new WWW(BaseURL, rawData);
         yield return www;
     }

    /*public void SendPostRequest(string x, string y)
    {
        string Message = x + "  " + y;
        StartCoroutine(SendPR(Message));
    }

    public void SendGetRequest()
    {
        string Message = GetInput.text;
        StartCoroutine(SendGR(Message));
    }

    IEnumerator SendPR(string Message)
    {
        WWWForm form = new WWWForm();
        form.AddField("unitypost", Message);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/tutorials/unityphppostget/", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response Text from the server = " + responseText);
            }
        }
    }

    IEnumerator SendGR(string Message)
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/tutorials/unityphppostget/?unityget=" + Message))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response Text from the server = " + responseText);
            }
        }
    }*/


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
