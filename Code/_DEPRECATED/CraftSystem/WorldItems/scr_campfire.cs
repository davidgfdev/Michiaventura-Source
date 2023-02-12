using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_campfire : MonoBehaviour
{
    GameObject levelController;
    Vector3 offset = new Vector3(0, 2, 0);

    [SerializeField] bool isMenu = false;

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController");
    }

    private void Start()
    {
        if (isMenu) GetComponent<Animator>().SetBool("Activa", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Rest();
        }
    }

    void Rest()
    {
        GameObject[] fogatas = GameObject.FindGameObjectsWithTag("Campfire");
        foreach (GameObject fogata in fogatas)
        {
            fogata.GetComponent<Animator>().SetBool("Activa", false);
        }
        GetComponent<Animator>().SetBool("Activa", true);
        GetComponent<AudioSource>().Play();
        levelController.GetComponent<scr_levelstate>().setCheckPoint(transform.position + offset);
    }
}
