using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public GameObject originalCrossHair;
    public GameObject touchCrossHair;
    public GameObject aCamera;
    public GameObject drawer;
    public Text DrawerText;
    private bool DrawerIsClosed = true;
    private Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if the sight touches the chest of drawers
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
        {
            // cross hair switch
            if ((hit.transform.gameObject == this.gameObject || // is the hit objet a chest
                hit.transform.gameObject == drawer.gameObject) && hit.distance < 20)
            {
                if (!touchCrossHair.gameObject.activeSelf)
                {
                    originalCrossHair.SetActive(false);
                    touchCrossHair.SetActive(true);
                }
            }
            else
            {
                if (touchCrossHair.gameObject.activeSelf)
                {
                    originalCrossHair.SetActive(true);
                    touchCrossHair.SetActive(false);
                }

            }
             // open/close drawer
            if (hit.transform.gameObject == drawer.gameObject && hit.distance < 20)
            {
                if (!DrawerText.IsActive())
                    DrawerText.gameObject.SetActive(true);


                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(OpenCloseDrawer());
                }
            }
            else // the focus is not on drawer
            {
                if (DrawerText.IsActive())
                    DrawerText.gameObject.SetActive(false);

            }
       }
    }

    IEnumerator OpenCloseDrawer()
    {
        animator.SetBool("Open", DrawerIsClosed);
        DrawerIsClosed = !DrawerIsClosed;
        sound.PlayDelayed(0.7f);
        yield return new WaitForSeconds(2);

        if (DrawerIsClosed)
            DrawerText.text = "Press [E] to open";
        else
            DrawerText.text = "Press [E] to close";

    }
}
