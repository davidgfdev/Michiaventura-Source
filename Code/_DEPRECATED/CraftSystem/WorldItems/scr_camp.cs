using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class scr_camp : MonoBehaviour
{
    GameObject gameController;

    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                Rest();
            }

            if (Input.GetKey(KeyCode.X))
            {
                gameController.GetComponent<scr_craftSystem>().CraftItem();
            }
        }
    }

    void Rest()
    {
        //gameController.GetComponent<scr_gamestate>().ChangeSpawnPoint(transform);
        //gameController.GetComponent<scr_savesystem>().SaveGame();
    }
}
