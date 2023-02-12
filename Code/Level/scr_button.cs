using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_button : MonoBehaviour
{
    [SerializeField] GameObject door;
    bool activated = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            if (!activated)
            {
                activated = true;
                GetComponent<Animator>().SetTrigger("Activate");
                GetComponent<AudioSource>().Play();
                door.GetComponent<scr_door>().OpenDoor();
            }
        }
    }
}
