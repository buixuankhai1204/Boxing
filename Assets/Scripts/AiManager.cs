using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AiManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public Transform player;
    public float health;

    //Patroling
    public Vector3 walkingPoint;
    private bool walkingPointSet;
    public float walkingPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    
    //State
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    
    //store food and vegetable
    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag(Tags.PlayerTag).GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkingPointSet) SearchPoint();
        if (walkingPointSet) agent.SetDestination(walkingPoint);

        Vector3 distanceToWlkPoint = walkingPoint - transform.position;
        if (distanceToWlkPoint.magnitude < 1)
        {
            walkingPointSet = false;
        }
    }

    public void SearchPoint()
    {
        float randomZ = Random.Range(-walkingPointRange, walkingPointRange);
        float randomX = Random.Range(-walkingPointRange, walkingPointRange);
        walkingPoint = new Vector3(transform.position.x + randomX, transform.position.y,
            transform.position.z + randomZ);
        if (Physics.Raycast(walkingPoint, Vector3.up, 2f, whatIsGround))
        {
            walkingPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //make sure the enemy don't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(resetAttack), timeBetweenAttacks);
            
        }
    }

    private void resetAttack()
    {
        alreadyAttacked = false;
        
    }

    public void TakeDamage()
    {
        health -= 15;
        if(health < 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}