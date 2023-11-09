using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBillboard : MonoBehaviour
{
    public Camera cam;
    
    void LateUpdate()
    {
        //transform.LookAt(transform.position + cam.transform.forward);
        //Vector3 p = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        //Vector3 p = cam.ScreenToWorldPoint(new Vector3(Screen.width*0.25f, Screen.height*0.95f, cam.nearClipPlane*3));
        Vector3 p = cam.ScreenToWorldPoint(new Vector3(Screen.width*0.35f, Screen.height*0.95f, cam.nearClipPlane*3));

        transform.position = p;
    }
}
