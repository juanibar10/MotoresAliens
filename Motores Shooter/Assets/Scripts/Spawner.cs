using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform player;
    public GameManager manager;
    public bool spawning;
    public void OnSpawnZombie()
    {
        spawning = true;
        StartCoroutine(SpawnZombie());
    }

    IEnumerator SpawnZombie()
    {
        manager.zombiesInMap++;
        manager.zombiesLeftToSpawn--;
        yield return new WaitForSeconds(2);
        print("a");
        ZombieAI zombie = Instantiate(zombiePrefab).GetComponent<ZombieAI>();
        zombie.transform.position = transform.position;
        zombie.SetTarget = player;

        yield return new WaitForSeconds(2);

        zombie.canMove = true;
        zombie.agent.SetDestination(zombie.target.position);
        zombie.anim.SetFloat("Vel", 1);
        spawning = false;
    }

}
