using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;

    public static ScoreManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }



    public void AddScore(int points) 
    {

        currentScore += points;
    }
}
