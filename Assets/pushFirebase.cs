using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pushFirebase : MonoBehaviour
{

    public Text text;
    private float xVal = 0f;
    private float yVal = 0f;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Hello";
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            xVal += .5f;
            text.text = xVal.ToString();
            postFirebase();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            xVal -= .5f;
            text.text = xVal.ToString();
            postFirebase();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            yVal += .5f;
            text.text = yVal.ToString();
            postFirebase();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            yVal -= .5f;
            text.text = yVal.ToString();
            postFirebase();
        }
    }

    public void postFirebase()
    {
        Data data = new Data(xVal, yVal);
        RestClient.Put("https://testproject-d51e9-default-rtdb.firebaseio.com/"+"Data:"+".json",data);
    }
}
