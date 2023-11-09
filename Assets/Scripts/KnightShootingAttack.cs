using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightShootingAttack : MonoBehaviour
{
    private Animator animator;
    public GameObject player;
    private UnityEngine.AI.NavMeshAgent playerAgent;

    private void Awake()
    {
        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void shootEnemy(GameObject target)
    {
        KnightHealthBar hb = target.GetComponent<KnightHealthBar>();
        if (hb is null || !hb.isAlive())
            return;

        //get gun by tag and then create the shot act
        GameObject gun = Utils.FindChildWithTag(player, "gun");
        GunAgentInterface gunAgent = gun.GetComponent<GunAgentInterface>();
        //Debug.Log("gunAgent: " + gunAgent);

        gunAgent.shootAct(target, hb, player.name);


    }


}
