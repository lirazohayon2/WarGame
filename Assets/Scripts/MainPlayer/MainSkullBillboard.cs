using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSkullBillboard : MonoBehaviour
{
    public Camera cam;
    
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.transform.forward);
        //Vector3 p = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        //Vector3 p = cam.ScreenToWorldPoint(new Vector3(Screen.width*0.25f, Screen.height*0.95f, cam.nearClipPlane*3));
        Vector3 p = cam.ScreenToWorldPoint(new Vector3(Screen.width*0.5f, Screen.height*0.3f, cam.nearClipPlane*8));

        transform.position = p;
    }
}
