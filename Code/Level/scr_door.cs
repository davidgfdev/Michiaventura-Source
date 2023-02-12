using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_door : MonoBehaviour
{
    [SerializeField] BoxCollider2D m_collider;
    SpriteRenderer m_renderer;
    void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
        m_renderer = GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        GetComponent<Animator>().SetTrigger("Activate");
        GetComponent<AudioSource>().Play();
        m_collider.enabled = false;
    }
}
