using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class options_with_bouttens : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetInteger("State", 1); // idle fight
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetInteger("State", 0); // idle
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(GetHitAndGetUp());
            animator.SetInteger("State", 2); // fall back and get up
        }
    }

    public IEnumerator GetHitAndGetUp()
    {
        animator.SetInteger("State", 2);
        yield return new WaitForSeconds(2);
        animator.SetInteger("State", 0);
    }
}
