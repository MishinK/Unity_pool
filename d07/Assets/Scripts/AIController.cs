using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Transform[] goal;
    public GameObject cannon;
    public float hp;

    private NavMeshAgent agent;
    private GameObject target;
    private bool locked;
    private Vector3 cannonOrig;
    private bool inRange;

    private bool player;
    private bool enemy;

    private int enemyState;

    private bool killed;


    public GameObject[] explosionParticles;
    public GameObject tankExplosion;
    private bool shooting;
    private Coroutine shootCoroutine;
    private bool enemyKilled;

	private int idx_target = 0;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal[idx_target].position;
        cannonOrig = cannon.transform.eulerAngles;
    }

    private void Update()
    {
        if(inRange)
        {
            cannon.transform.LookAt(target.transform.position);
            cannon.transform.eulerAngles = new Vector3(cannonOrig.x, cannon.transform.eulerAngles.y, 0.0f);
            RayCastFocus();
        }
        if(locked)
        {
            agent.isStopped = true;
            if (!shooting)
            {
                shooting = true;
                shootCoroutine = StartCoroutine(ShootTarget());
            }
        }
		if ((transform.position - goal[idx_target].position).magnitude < 10f && !target)
		{
			if (idx_target == 0)
				idx_target++;
			else
				idx_target--;
			agent.destination = goal[idx_target].position;
		}
		else if (target)
		{
			agent.destination = target.transform.position;
		}
        if (hp <= 0 && !killed)
            StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        killed = true;
        GameObject tmp = (GameObject)Instantiate(explosionParticles[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(tmp);
        Destroy(this.gameObject);
    }

    private IEnumerator ShootTarget()
    {
            PlayerController tankController = null;
            AIController aIController = null;
            if (player)
                tankController = target.GetComponent<PlayerController>();
            else if (enemy)
                aIController = target.GetComponent<AIController>();
            
        while (true)
        {
            float randY = Random.Range(-0.2f, 0.2f);
            Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
            fwd.y += randY;
            RaycastHit hit;
            if (Physics.Raycast(cannon.transform.position, fwd, out hit, 30.0f))
            {
                int type = Random.Range(0, 2);
                StartCoroutine(Explosion(explosionParticles[type], hit.point));
                if (hit.transform.tag == "Player" || hit.transform.tag == "AI")
                {
                    if (player)
                        enemyState = tankController.HPDecrease(type == 0 ? 5.0f : 0.5f);
                    else if (enemy)
                        enemyState = aIController.HPDecrease(type == 0 ? 5.0f : 0.5f);
                }
				else
				{
					agent.destination = target.transform.position;
				}
                yield return new WaitForSeconds(type == 0 ? 2.0f : 1.0f);
            }
            if (enemyState == 1)
            {
                enemyKilled = true;
                yield return new WaitForSeconds(1.0f);
                ResetVal();
            }
        }
    }

    public int HPDecrease(float damage)
    {
        hp -= damage;
		if (hp < 20)
		{
			explosionParticles[2].SetActive(true);
		}
        if (hp > 0)
            return (0);
        else
            return (1);
    }

    private IEnumerator Explosion(GameObject particle, Vector3 pos)
    {
        GameObject tmp = (GameObject)Instantiate(particle, pos, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        Destroy(tmp);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "AI")
        {
            inRange = true;
            if (!target || other.transform.tag == "Player")
                target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "AI")
        {
            ResetVal();
        }
    }

    private void RayCastFocus()
    {
        Vector3 fwd = cannon.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(cannon.transform.position, fwd, out hit, 30.0f))
        {
            if (hit.transform.gameObject != transform.gameObject && (hit.transform.tag == "Player" || hit.transform.tag == "AI"))
            {
                locked = true;
                if (hit.transform.tag == "Player")
                    player = true;
                else
                    enemy = true;
            }
            else
            {
                if (!enemyKilled)
                    agent.destination = target.transform.position;
                else
				{
                    agent.destination = goal[idx_target].position;
				}
                agent.isStopped = false;
                locked = false;
                if (shooting)
                {
                    shooting = false;
                    StopCoroutine(shootCoroutine);
                }
            }
        }
    }

    private void ResetVal()
    {
        inRange = false;
        target = null;
        locked = false;
        agent.isStopped = false;
        player = false;
        enemy = false;
        enemyKilled = false;
        agent.destination = goal[idx_target].position;
        if (shooting)
        {
            shooting = false;
            StopCoroutine(shootCoroutine);
        }
    }
}
