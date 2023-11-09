using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalAction : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SceneSwitch()); // run in parallel
     }

    IEnumerator SceneSwitch()
    {
        // play fade in animation
        animator.SetBool("StartFadeIn", true);
        // delay
        yield return new WaitForSeconds(2);
        // switch scene
       if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(0);

    }


}
