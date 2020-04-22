using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

	[Header("Input")]
	public KeyCode spawnKey = KeyCode.Mouse0;
	public string axis;

	[Header("Spawner Settings")]
	public GameObject projectilePrefab;
	public Transform spawnPoint;

	public float spawnRate;
	private float timer;

	public ParticleSystem flashParticles;

	public AudioSource fireSound;
	
	void Update()
	{
		timer += Time.deltaTime;

		if((Input.GetKey(spawnKey) || Input.GetAxis(axis) > 0) && timer >= spawnRate)
		{
			SpawnProjectile();
		}
	}
	
	void SpawnProjectile()
	{
		timer = 0f;
		Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
	
		if(flashParticles)
		{
			flashParticles.Play();
		}

		if(fireSound)
		{
			fireSound.Play();
		}
	}

}
