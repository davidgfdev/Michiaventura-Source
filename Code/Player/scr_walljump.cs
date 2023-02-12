using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_walljump : MonoBehaviour
{
    [Header("WallJump")]
    [SerializeField] float wallJumpCD;
    [SerializeField] float wallJumpForce;
    [SerializeField] GameObject gorro;
    Color gorroColor;

    Rigidbody2D rb;
    Vector2 direction;
    bool canWallJump;
    Animator anim;
    bool escalada = true;
    float nextWallJump = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        gorroColor = gorro.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (Time.time > nextWallJump)
        {
            escalada = true;
            gorro.GetComponent<SpriteRenderer>().color = gorroColor;
        }

        if (canWallJump && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("walljmup");
            rb.AddForce(direction, ForceMode2D.Impulse);
            escalada = false;
            gorro.GetComponent<SpriteRenderer>().color = Color.gray;
            nextWallJump = Time.time + wallJumpCD;
            anim.SetTrigger("Jump");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Mathf.Abs(contact.normal.x) == 1 && Input.GetAxisRaw("Horizontal") == -contact.normal.x && rb.velocity.y < 0 && escalada)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.60f);
                    canWallJump = true;
                    direction = new Vector2(0, wallJumpForce);
                    anim.SetBool("WallStick", true);
                }
                else
                {
                    canWallJump = false;
                    anim.SetBool("WallStick", false);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (canWallJump) Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 2));
    }

    public void recuperarEscalada()
    {
        escalada = true;
        gorro.GetComponent<SpriteRenderer>().color = gorroColor;
    }
}