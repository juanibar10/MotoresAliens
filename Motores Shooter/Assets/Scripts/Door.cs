using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int precio;
    public GameObject canvas;

    Animator anim;
    bool abierta;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        canvas.SetActive(false);
    }

    public void OpenDoor()
    {
        if(ScoreManager.instance.currentScore >= precio)
        {
            ScoreManager.instance.currentScore -= precio;
            anim.SetBool("Abierta", true);
            abierta = true;
        }
    }

    public void CloseDoor()
    {
        anim.SetBool("Abierta", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (!abierta)
            {
                other.GetComponent<PlayerInteractuation>().currentDoor = this;
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other.GetComponent<PlayerInteractuation>().currentDoor == this)
            {
                other.GetComponent<PlayerInteractuation>().currentDoor = null;
                canvas.SetActive(false);
            }
        }
    }
}
