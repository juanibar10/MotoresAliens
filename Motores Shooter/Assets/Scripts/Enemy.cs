using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int totalHealth = 100;
    [SerializeField] private int currentHealth;

    public int points = 50;
    public ScoreManager scoreManager;

    private void Start() {
        
        currentHealth = totalHealth;
    }

    public void RefreshHealth(int damage) {

        currentHealth -= damage;

        if(currentHealth <= 0) {

            KillEnemy();
        }
    }

    void KillEnemy() {

        if(scoreManager && points > 0) {

            scoreManager.AddScore(points);
        }

        Destroy(gameObject);
    }
}
