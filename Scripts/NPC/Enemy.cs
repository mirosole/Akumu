using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;
    
    // Attacking
    public float timeBetweenAttacks;
    public bool isAttacking;

    // States
    public float attackRange, seeingAngle;
    public bool playerInAttackRange;
    
    // Size changes
    public float scaleSpeed;
    private Vector3 originalScale;
    public Vector3 attackScale = new Vector3(1.5f, 1.5f, 1.5f);
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Check if player is in sight or attack range
        playerInAttackRange = IsPlayerInSight(attackRange);
        if (!playerInAttackRange)
        {
            ChasePlayer();
            ScaleToOriginalSize();
        }
        else
        {
            Attack();
            ScaleToAttackSize();
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // Set Animator parameters based on state
        animator.SetBool("isChasing", !playerInAttackRange); // Chase
        animator.SetBool("isAttacking", playerInAttackRange); // Attack
    }

    private bool IsPlayerInSight(float range)
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        
        // Check if the player is within the specified range
        if (Vector3.Distance(transform.position, player.position) <= range)
        {
            // Check if the player is within the vision angle
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= seeingAngle)
            {
                // Check if there's a clear line of sight to the player
                if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, range))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    // Method to scale the enemy to its original size
    private void ScaleToOriginalSize()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);
    }

// Method to scale the enemy to the attack size
    private void ScaleToAttackSize()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, attackScale, Time.deltaTime * scaleSpeed);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        // Vector3 playerPosition = player.position;
        // Vector3 enemyPosition = agent.transform.position;
        // // Adjust the target position to match the enemy's Y-coordinate (height)
        // Vector3 lookAtTarget = new Vector3(playerPosition.x, enemyPosition.y, playerPosition.z);
        // // Make the enemy look at the adjusted target position
        // transform.LookAt(lookAtTarget);
    }

    private void Attack()
    {
        agent.SetDestination(agent.transform.position);
        Vector3 playerPosition = player.position;
        Vector3 enemyPosition = agent.transform.position;
        // Adjust the target position to match the enemy's Y-coordinate (height)
        Vector3 lookAtTarget = new Vector3(playerPosition.x, enemyPosition.y, playerPosition.z);
        // Make the enemy look at the adjusted target position
        transform.LookAt(lookAtTarget);

        // Start attack
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true); 
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        
    }
#endif
}
