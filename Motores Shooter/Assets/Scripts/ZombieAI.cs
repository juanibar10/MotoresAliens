using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    public Transform target;

    public bool hitting;
    public bool jumping;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Vel", 1);
        agent.SetDestination(target.position);
    }

    public Transform SetTarget
    {
        set{ target = value; }
    }

    private void Update()
    {
        agent.SetDestination(target.position);

        if (agent.isOnOffMeshLink && !jumping)
            StartCoroutine(JumpBarrier(0.7f));

        if (agent.remainingDistance <= agent.stoppingDistance && target.tag == "Player" && !hitting)
            StartCoroutine(Hit());

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
