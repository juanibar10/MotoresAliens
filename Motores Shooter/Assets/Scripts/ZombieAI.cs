using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Animator anim;

    public Transform target;

    public bool hitting;
    public bool jumping;
    public bool canMove;

    float health;
    float velocity;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Vel", 0);
    }

    public Transform SetTarget
    {
        set{ target = value; }
    }

    public float TakeDamage
    {
        set
        {
            if (health - value > 0)
                health -= value;
            else if (health - value <= 0)
            {
                health = 0;
                Die();
            }
        }
    }

    public float Health
    {
        get{ return health; }
    }

    public float Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }
    
    private void Update()
    {
        if (canMove)
        {
            agent.SetDestination(target.position);

            if (agent.isOnOffMeshLink && !jumping)
                StartCoroutine(JumpBarrier(0.7f));

            if (agent.remainingDistance <= agent.stoppingDistance && target.tag == "Player" && !hitting)
                StartCoroutine(Hit());
        }
    }

    void Die()
    {

    }


    IEnumerator JumpBarrier(float tiempo)
    {
        print("Empieza");
        Vector3 startPos = transform.position;
        Vector3 endPos = agent.currentOffMeshLinkData.endPos;

        jumping = true;
        agent.isStopped = true;
        anim.SetTrigger("Jump");

        float forTime = 50f;

        for (int i = 0; i <= forTime; i++)
        {
            transform.position = new Vector3(Mathf.Lerp(startPos.x, endPos.x, i * (1f / forTime)), transform.position.y, Mathf.Lerp(startPos.z, endPos.z, i * (1f / forTime)));
            yield return new WaitForSeconds(tiempo * (1f / forTime));
        }

        agent.isStopped = false;
        agent.CompleteOffMeshLink();

        yield return new WaitUntil(() => !agent.isOnOffMeshLink);
        jumping = false;
    }

    IEnumerator Hit()
    {
        hitting = true;
        agent.isStopped = true;
        anim.SetFloat("Vel", 0);
        yield return new WaitForEndOfFrame();

        print("Animacion golpe");

        yield return new WaitForSeconds(2);

        anim.SetFloat("Vel", 1);
        agent.isStopped = false;
        agent.SetDestination(target.position);
        hitting = false;
    }


}
