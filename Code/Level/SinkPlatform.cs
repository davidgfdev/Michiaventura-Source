using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkPlatform : MonoBehaviour
{
    [SerializeField] float sinkTime;
    [SerializeField] float recoverTime;
    [SerializeField]
    float force;

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRender;
    Animator anim;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, 0);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            collision.gameObject.GetComponent<scr_jump>().PlaySound();
            StartCoroutine(Sink());
        }
    }

    IEnumerator Sink()
    {
        yield return new WaitForSeconds(sinkTime);
        anim.SetTrigger("Desparecer");
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 0);
        boxCollider.enabled = false;
        yield return new WaitForSeconds(recoverTime);
        anim.SetTrigger("Aparecer");
        spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, 1);
        boxCollider.enabled = true;
    }
}
