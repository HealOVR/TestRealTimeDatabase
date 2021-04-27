using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pushFirebase : MonoBehaviour
{

    public Text text;
    public static float val = 0f;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Hello";
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            val += .5f;
            text.text = val.ToString();
            postFirebase();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            val -= .5f;
            text.text = val.ToString();
            postFirebase();
        }
    }

    public void postFirebase()
    {
        Data data = new Data();
        RestClient.Put("https://testproject-d51e9-default-rtdb.firebaseio.com/"+"Data:"+".json",data);
    }
}
