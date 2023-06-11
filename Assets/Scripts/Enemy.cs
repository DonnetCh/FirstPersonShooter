using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public Transform playerTrans;
    public Transform gunTrans;
    public GameObject projectile;
    public LayerMask whatisGround, whatisPlayer;

    //Patrullar

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Atacar
    public float timeBetweenAttacks;
    public bool alreadyAttack;

    //Estados
    public float sightRange, attackRange;
    public bool inSight,inAttackRange;

    //public Transform puntoA;
    //public Transform puntoB;
    public GameObject Cop;
    
    public int Health = 5;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {

        inSight = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);

        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!inSight && !inAttackRange)
        {
            Patrolling();
        }
        if (inSight && !inAttackRange)
        {
            ChasePlayer();
        }
        if (inSight && inAttackRange)
        {
            AttackPlayer();
        }
        if (Health <= 0)
        {
            
            player.GetComponent<PlayerMov>().CopsKilled++;
            Destroy(gameObject);
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
        {
            walkPointSet = true;
            //Debug.Log("walkpointset");
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(playerTrans.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(playerTrans.position);

        if (!alreadyAttack)
        {
            Rigidbody rb = Instantiate(projectile, gunTrans.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 15f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 1f, ForceMode.Impulse);
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }
    
   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
}
