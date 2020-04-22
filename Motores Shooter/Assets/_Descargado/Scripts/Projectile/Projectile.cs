using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	public float speed;
	private Rigidbody projectileRigidbody;

	public float lifeTime;
	public int damage = 20;

	public GameObject scenarioParticles;
	public GameObject enemyParticles;

	void Start()
	{
		projectileRigidbody = GetComponent<Rigidbody>();
		Invoke("RemoveProjectile", lifeTime);
	}
	
	void Update()
	{
			Vector3 movement = transform.forward * speed * Time.deltaTime;
			projectileRigidbody.MovePosition(transform.position + movement);
	}


	private void OnTriggerEnter(Collider other) {

		if (other.CompareTag("Scenario")) {

			Instantiate(scenarioParticles, transform.position, transform.rotation);
		
		}else if (other.CompareTag("Enemy")) {

			if(damage > 0) {

				other.GetComponent<Enemy>().RefreshHealth(damage);
			}

			Instantiate(enemyParticles, transform.position, transform.rotation);
		}
	}

	void RemoveProjectile()
	{
		Destroy(gameObject);
	}

}
