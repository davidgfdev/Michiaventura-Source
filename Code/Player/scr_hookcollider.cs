using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_hookcollider : MonoBehaviour
{
    [HideInInspector] public float speed;
    scr_hook hookScript;
    Rigidbody2D rb;
    [SerializeField] GameObject audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hookScript = GameObject.FindGameObjectWithTag("Player").GetComponent<scr_hook>();
    }

    private void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HookableWall"))
        {
            rb.velocity = Vector2.zero;
            hookScript.OnHookCollide(transform.position, "HookableWall", collision);
            GameObject audioSFX = Instantiate(audioSource, transform.position, transform.rotation);
            Destroy(audioSFX, 5);
        }
        else
        {
            hookScript.OnHookCollide(transform.position, "Failed", collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<scr_windZone>() != null)
        {
            hookScript.OnHookCollide(transform.position, "Failed", null);
        }
    }
}
