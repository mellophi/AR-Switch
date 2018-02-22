using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class PostVB : MonoBehaviour,IVirtualButtonEventHandler {

    private GameObject vbtn;

    // Use this for initialization
    void Start () {
        vbtn = GameObject.Find("VirtualButton");
        vbtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Button Down!");
        //player.transform.Rotate(new Vector3(0, Time.deltaTime * 1000, 0));

        string url = "http://posttestserver.com/post.php";

        WWWForm form = new WWWForm();
        form.AddField("var1", "value1");
        form.AddField("var2", "value2");
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }


    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        
    }

}
