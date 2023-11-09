using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMotionNew : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent playerAgent;

    private GameObject target;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;


    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    //public float timeBetweenAttacks;
    public float timeRandomRangeBetweenAttacksMin;
    public float timeRandomRangeBetweenAttacksMax;
    bool alreadyAttacked;
    
    private KnightShootingAttack shootingKnight;
    
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    void Start()
    {
        this.Awake();
    }
    private void Awake()
    {

        playerAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        shootingKnight = GetComponent<KnightShootingAttack>();
    }

    private void Update()
    {
        //if agent is dead do nothing
        KnightHealthBar hb = playerAgent.GetComponent<KnightHealthBar>();
        if (!hb.isAlive())
            return;

        /*
        if (Utils.isGameOver)
        {
            Debug.Log("GAMEOVER>>>>>>>>>>>>");
            GetComponent<Animator>().SetInteger("State", 3);
            return;
        }
        */

        //Check for sight and attack range
        playerInSightRange = searchForPlayer();  
        playerInAttackRange = searchForToAttackPlayer();

        //Debug.Log("playerInSightRange:   " + playerInSightRange + " playerInAttackRange: " + playerInAttackRange);

        //if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        //Debug.Log("walkPointSet:   " + walkPointSet + " walkPoint: " + walkPoint);
        if (walkPointSet)
            playerAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        walkPointSet = true;
    }

    private bool searchForPlayer()
    {
        if (!Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
            return false;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange, whatIsPlayer);
        int rand_index = Random.Range(0, hitColliders.Length);
        target = hitColliders[rand_index].gameObject;
        return true;
    }

    private bool searchForToAttackPlayer()
    {
        if (!Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
            return false;
        target = Physics.OverlapSphere(transform.position, attackRange, whatIsPlayer)[0].gameObject;
        return true;
    }


    private void ChasePlayer()
    {
        playerAgent.SetDestination(target.transform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        playerAgent.SetDestination(this.transform.position);

        this.transform.LookAt(target.transform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            shootingKnight.shootEnemy(target); 
            ///End of attack code

            alreadyAttacked = true;
            float timeBetweenAttacks = Random.Range(timeRandomRangeBetweenAttacksMin, timeRandomRangeBetweenAttacksMax);
            //Debug.Log("<timeBetweenAttacks>: " + timeBetweenAttacks);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
