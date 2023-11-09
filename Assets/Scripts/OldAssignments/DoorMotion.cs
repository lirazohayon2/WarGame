using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMotion : MonoBehaviour
{
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("DoorOpens", true);
        sound.PlayDelayed(1.2f);
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("DoorOpens", false);
        sound.PlayDelayed(1.2f);
    }

}
