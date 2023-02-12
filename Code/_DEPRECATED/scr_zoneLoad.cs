using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_zoneLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToChange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TransitionZone();
        }
    }

    private void TransitionZone()
    {
        objectToChange.SetActive(!objectToChange.activeSelf);                       
    }
}
