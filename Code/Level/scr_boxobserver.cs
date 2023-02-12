using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class scr_boxobserver : MonoBehaviour
{
    bool alreadyInvoked = false;
    [SerializeField] GameObject door;
    private enum ButtonModels
    {
        interaction,
        box
    }

    [SerializeField] private ButtonModels mode;
    [SerializeField] private UnityEvent observerAction;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (mode == ButtonModels.interaction)
        {
            if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.X) && !alreadyInvoked)
            {
                GetComponent<Animator>().SetTrigger("Activate");
                GetComponent<AudioSource>().Play();
                door.GetComponent<scr_door>().OpenDoor();
                alreadyInvoked = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") && mode == ButtonModels.box)
        {
            //observerAction.Invoke();
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("Activate");
            door.GetComponent<scr_door>().OpenDoor();
        }
    }
}
