using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pullFromFirebase : MonoBehaviour
{
    public Text text1;
    public Text text2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pullFirebase();
    }

    public void pullFirebase()
    {
        Data data;
        string output;
        RestClient.Get<Data>("https://testproject-d51e9-default-rtdb.firebaseio.com/" + "Data:" + ".json").Then(response => 
        {
            data = response;
            text1.text = data.xVal;
            text2.text = data.yVal;
        });
    }
}
