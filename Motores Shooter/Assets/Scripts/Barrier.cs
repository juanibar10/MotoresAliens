using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public GameObject jump;
    public Transform AIPosition;
    public int woodCount = 6;

    private void Awake()
    {
        //jump.SetActive(false);
    }

    private void Update()
    {
        if (woodCount == 0)
            jump.SetActive(true);
    }
}
