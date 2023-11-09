using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightMotion : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    public GameObject target;
    //private LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // computes path to the target and starts moving to the target
        if (agent.enabled)
        {
            int numPoints;
            agent.SetDestination(target.transform.position);
            numPoints = agent.path.corners.Length;
          //  line.positionCount = numPoints;
          //  line.SetPositions(agent.path.corners);
        }
    }

    public IEnumerator GetHitAndGetUp()
    {
        animator.SetInteger("State", 2); // fall back and get up
        yield return new WaitForSeconds(2);
        animator.SetInteger("State", 0); // idle

    }
}
