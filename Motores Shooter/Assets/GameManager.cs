using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Material mat;
    public Transform player;
    public float revealSpeed;
    public AnimationCurve curve;
    float contador = 0;
    private void Awake()
    {
        mat.SetInt("end", 1);
        mat.SetVector("PlayerPos", player.position);

    }
    private void Update()
    {
        if(contador < 500)
        {
            contador += Time.deltaTime * revealSpeed;
        }
        else
        {
            contador = 500;
            mat.SetInt("end", 0);
        }

        mat.SetFloat("revealScale", Mathf.Lerp(0, 1000, curve.Evaluate(contador / 500))); 

    }
}
