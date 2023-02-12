using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_blueprint : MonoBehaviour
{
    [SerializeField] c_item item;

    scr_craftSystem craftSystem;
    Animator anim;

    private void Awake()
    {
        craftSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<scr_craftSystem>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            craftSystem.SetCraftableItem(item);
            anim.SetTrigger("taken");
        }
    }

    public void activarParticulas()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }

    public void CheckIfTaken()
    {
        if (craftSystem.GetCraftableItem() != null)
        {
            if (craftSystem.GetCraftableItem().id == item.id)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);

            }
        }

        if (GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
}
