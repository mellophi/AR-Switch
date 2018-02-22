using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBScript : MonoBehaviour,IVirtualButtonEventHandler {

    public GameObject virtualBtn;
    public Animator btnAnime;


    // Use this for initialization
    void Start () {
        virtualBtn = GameObject.Find("VirtualButton");
        virtualBtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        btnAnime.GetComponent<Animator>();
	}

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        btnAnime.Play("cube_animation");
        Debug.Log("Button Pressed!!");
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        btnAnime.Play("none");
        Debug.Log("Button Released!!");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
