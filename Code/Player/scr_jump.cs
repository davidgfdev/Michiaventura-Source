using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_jump : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] AudioClip jumpSFX;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float coyoteTime;
    [SerializeField] float bufferTime;
    [SerializeField] float stopJumpX;
    [SerializeField] float stopJumpY;

    Rigidbody2D rb;
    Animator anim;
    AudioSource audioSource;

    float coyoteCount, bufferCount;
    public bool grounded, onPlatform;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z) && onPlatform)
        {
            Vector3 downOffset = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            transform.position = downOffset;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            bufferCount = bufferTime;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            StopJump();
        }

        if (grounded && (rb.velocity.y < 0.5f && rb.velocity.y > -0.5f))
        {
            coyoteCount = coyoteTime;
        }
        else
        {
            coyoteCount -= Time.deltaTime;
        }

        bufferCount -= Time.deltaTime;

        if ((bufferCount >= 0 && coyoteCount > 0))
        {
            Jump(Vector2.up);
        }
    }

    void Jump(Vector2 direction)
    {
        anim.SetTrigger("Jump");
        PlaySound();
        grounded = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(direction * jumpForce * 100);
        coyoteCount = 0;
        bufferCount = 0;
    }

    void StopJump()
    {
        if (grounded == false && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * stopJumpX, rb.velocity.y * stopJumpY);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") &&
         !collision.gameObject.GetComponent<scr_windZone>() &&
          !collision.gameObject.GetComponent<scr_stormTrigger>() &&
          !collision.gameObject.CompareTag("Moneda") &&
          !collision.gameObject.GetComponent<scr_campfire>())
        {
            if (collision.gameObject.GetComponentInChildren<PlatformEffector2D>())
            {
                onPlatform = true;
            }
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            grounded = false;
            onPlatform = false;
        }
    }

    public void PlaySound()
    {
        audioSource.clip = jumpSFX;
        audioSource.Play();
    }

    public bool checkGrounded()
    {
        return grounded;
    }
}
