using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    [HideInInspector] public GameObject enemy;
    private EnemyController enemyController;
    [HideInInspector] public bool enemySet;
    private bool mouseReleased;

    public float attackRange;

    public CharacterStat stat;
    [HideInInspector] public float curHealth;
    private bool potion;
    private GameObject potionObj;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stat = new CharacterStat(Random.Range(10, 20), Random.Range(10, 20), Random.Range(10, 20));
        stat.ArmorStat = 140.0f;
        curHealth = stat.HP;
    }

    private void Update()
    {
        float distance = 100.0f;

        if (stat.EXP >= stat.RequiredEXP)
        {
            stat.lvlUP();
            curHealth = stat.HP;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
                navMeshAgent.isStopped = false;
                if (hit.transform.tag == "Enemy")
                {
                    enemySet = true;
                    mouseReleased = false;
                    enemy = hit.transform.gameObject;
                    enemyController = enemy.GetComponent<EnemyController>();
                    potion = false;
                }
                else
                {
                    enemySet = false;
                    potion = false;
                }
                if (hit.transform.tag == "Potion")
                {
                    potion = true;
                    potionObj = hit.transform.gameObject;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseReleased = true;
        }

        if (enemySet)
            distance = Vector3.Distance(enemy.transform.position, transform.position);
        if (enemySet && distance <= attackRange)
        {
            Vector3 enemyPosition = new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z);
            transform.LookAt(enemyPosition);
            navMeshAgent.isStopped = true;
            if (enemyController.enemyState == EnemyController.State.ALIVE)
            {
                animator.SetTrigger("attack");
                if (mouseReleased == true)
                    enemySet = false;
            }
        }
        if (!enemySet && navMeshAgent.remainingDistance <= 1.0f)
        {
            navMeshAgent.isStopped = true;
            if (potion)
            {
                curHealth += stat.HP * 0.3f;
                if (curHealth > stat.HP)
                    curHealth = stat.HP;
                potion = false;
                Destroy(potionObj);
            }
        }

        if (navMeshAgent.isStopped)
            animator.SetBool("run", false);
        else
            animator.SetBool("run", true);
    }

    public void Attack()
    {
        if (enemyController.enemyState == EnemyController.State.ALIVE)
        {
            float hitChance = stat.HitChance(enemyController.stat.Agility);
            float random = Random.value;
            if (random <= hitChance / 100)
                enemyController.Attacked(stat.FinalDamage(0.0f));
        }
        else
            enemySet = false;
    }

    public void Attacked(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
            Die();
    }

    private void Die()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("death");
    }

    public void Stop(bool trueOrFalse)
    {
        navMeshAgent.isStopped = trueOrFalse;
        
    }

}