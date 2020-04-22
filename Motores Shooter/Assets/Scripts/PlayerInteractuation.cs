using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractuation : MonoBehaviour
{
    public Door currentDoor;

    private void Update()
    {
        if (currentDoor)
        {
            if (Input.GetButtonDown("Interactuar"))
            {
                currentDoor.OpenDoor();
            }
        }
    }
}
