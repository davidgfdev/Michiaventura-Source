using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_resource : MonoBehaviour
{
    [HideInInspector] public int id;
    [HideInInspector] public bool taken;
    [SerializeField] int resourceType;

    scr_inventorysystem inventory;

    Animator anim;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_inventorysystem>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        checkTaken();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inventory.AddResource(resourceType, 1);
            taken = true;
            anim.SetTrigger("taken");
        }
    }

    public void checkTaken()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = !taken;
        gameObject.GetComponent<BoxCollider2D>().enabled = !taken;
        if (GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
        //gameObject.SetActive(!taken);
    }

    public void activarParticulas()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
