using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class MultipleVB : MonoBehaviour, IVirtualButtonEventHandler
{
    //Allocates the serial port
    public static SerialPort sp = new SerialPort("COM3", 9600);
    public string message2;
    
    // Private fields to store the models
    private GameObject btn_1;
    private GameObject btn_2;
    
    /// Called when the scene is loaded
    void Start()
    {

        // Search for all Children from this ImageTarget with type VirtualButtonBehaviour
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++i)
        {
            // Register with the virtual buttons TrackableBehaviour
            vbs[i].RegisterEventHandler(this);
        }
        //Starts the connection
        OpenConnection();
        // Find the models based on the names in the Hierarchy


        btn_1 = transform.Find("btn1").gameObject;
        btn_2 = transform.Find("btn2").gameObject;
        
        // We don't want to show Jin during the startup
        btn_1.SetActive(true);
        btn_2.SetActive(true);
    }

    public void postins(string str)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Button Down!");
        //player.transform.Rotate(new Vector3(0, Time.deltaTime * 1000, 0));

        string url = "http://posttestserver.com/post.php";

        WWWForm form = new WWWForm();
        form.AddField("var1", str);
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

    /// <summary>
    /// Called when the virtual button has just been pressed:
    /// </summary>
    
    void update()
    {
        message2 = sp.ReadLine();
        print(message2);
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log(vb.VirtualButtonName);
        Debug.Log("Button pressed!");

        if (vb.VirtualButtonName == "btn1")
        {
            btn_1.SetActive(false);
            btn_2.SetActive(true);
            postins("btn1");
            sp.Write("y");
        }

        else if (vb.VirtualButtonName == "btn2")
        {
            btn_1.SetActive(true);
            btn_2.SetActive(false);
            postins("btn2");
            sp.Write("n");
        }
  
                //   default:
                //       throw new UnityException("Button not supported: " + vb.VirtualButtonName);
                //           break;
    }

    /// Called when the virtual button has just been released:
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        Debug.Log("Button released!");
    }

    public void OpenConnection()
    {
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();  // opens the connection
                sp.ReadTimeout = 16;  // sets the timeout value before reporting error
                print("Port Opened!");
                //		message = "Port Opened!";
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }
}