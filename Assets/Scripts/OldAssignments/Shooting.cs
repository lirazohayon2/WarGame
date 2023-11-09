using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{
    public GameObject enemy;
    public GameObject aCamera;
    private Animator animator;
    public ParticleSystem fireFlash;
    public GameObject target;
    public GameObject gun;
    public GameObject muzzle;
    private LineRenderer line;
    private NavMeshAgent agent;
 
    // Start is called before the first frame update
    void Start()
    {
        animator = enemy.GetComponent<Animator>();
        //      EnemyScript = GetComponent<KnightMotion>();
        line = gun.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource sound = gun.GetComponent<AudioSource>();
            sound.Play();
            fireFlash.Play();
            if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit))
            {
                // target is moved to the bullet hit
                target.transform.position = hit.point;
                // draw shooting line
                StartCoroutine(DrawShootingLine());


                if (hit.transform.gameObject == enemy.transform.gameObject)
                {
                    //                    animator.SetInteger("State", 2); // fall and get up
                    //                   StartCoroutine(EnemyScript.GetHitAndGetUp());
                    StartCoroutine(GetHitAndGetUp());
 //                   animator.SetInteger("State", 2); // fall back and get up
                }
            }
        }

        IEnumerator DrawShootingLine()
        {
            // 1. draw line
            line.SetPosition(0, muzzle.transform.position);
            line.SetPosition(1, target.transform.position);
            line.enabled = true;
            // 2. delay
            yield return new WaitForSeconds(0.1f);
            // 3. remove line
            line.enabled = false;
        }
    }

    public IEnumerator GetHitAndGetUp()
    {
        // stop the enemy motion
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.enabled = false;

        animator.SetInteger("State", 2); // fall back and get up
        yield return new WaitForSeconds(2);
        animator.SetInteger("State", 0); // idle

    }

}
