using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Animator animator;
    public bool isDead = false;
    public int maxHealth = 100;
    public int currentHealth;
    public EnemyHealth healthBar;

    public AudioSource enemyDieSource;
    public AudioClip enemyDieClip;
    bool deadSoundPlayed;

    // AI
    NavMeshAgent navMeshAgent;
    public GameObject[] waypoints;
    int currentWaypoint;
    bool canAttack;

    void Start()
    {
        animator = GetComponent<Animator>();

        isDead = false;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        navMeshAgent = GetComponent<NavMeshAgent>();
        currentWaypoint = 0;

        canAttack = true;

        deadSoundPlayed = false;
    }

    void Update()
    {
        if(currentHealth <= 0) {
            isDead = true;
            navMeshAgent.isStopped = true;
            animator.SetBool("IsAttacking", false);
            animator.SetTrigger("Dying");
            if(gameObject.CompareTag("Zombie")) {
                Invoke(nameof(DisablingObject), 3f);
            } else {
                Invoke(nameof(DisablingObject), 10f);
            }
        }

        if(isDead && !deadSoundPlayed) {
            enemyDieSource.PlayOneShot(enemyDieClip);
            deadSoundPlayed = true;
        }

        if(gameObject.CompareTag("Zombie")) {
            if(Player.isPlayerInTheWoods) {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
                animator.SetBool("IsWalking", true);
                Attacking(5f);
            } else {
                navMeshAgent.isStopped = true;
                animator.SetBool("IsWalking", false);
            }
        } 
        else {
            if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 15 && !isDead) {
                navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
                Attacking(2.5f);
            } else {
                Patrolling();
            }
        }
    }

    IEnumerator ResetCooldown(float attackCooldown) {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void Patrolling() {
        if(waypoints.Length > 0) {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].transform.position) < 3) {
                if(currentWaypoint == waypoints.Length - 1) {
                    currentWaypoint = 0;
                } else {
                    currentWaypoint += 1;
                }
            } 

            navMeshAgent.SetDestination(waypoints[currentWaypoint].transform.position);
        }
    }

    void Attacking(float attackCooldown) {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 1 && canAttack) {
            canAttack = false;
            animator.SetBool("IsAttacking", true);
            int random = Random.Range(1, 3);
            int damage = Random.Range(5, 11);
            if(random == 1) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakingDamage(damage);
            }

            StartCoroutine(ResetCooldown(attackCooldown));
        } else {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Sword") && !PlayerWeapon.canAttack) {
            int damage = Random.Range(10, 16);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }

    void DisablingObject() {
        animator.enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        navMeshAgent.SetDestination(gameObject.transform.position);
    }
}
