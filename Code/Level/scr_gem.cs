using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_gem : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 2, 0);
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("taken");
        }
    }

    public void destroyGem()
    {
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_levelstate>().setCheckPoint(transform.position + offset);
        GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_levelstate>().addGems(1);
        Destroy(gameObject);
    }
}
