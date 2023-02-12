using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playermovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float groundAcceleration;
    [SerializeField] private float airAcceleration;
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float maxGrabSpeed;
    [SerializeField] private float groundLinearDrag;
    [SerializeField] private float airLinearDrag;
    [SerializeField] private float rainLinearDrag;

    [Header("WalkCorrection")]
    [SerializeField] float topRayAltitude;
    [SerializeField] float bottomRayAltitude;
    [SerializeField] float rayDistance;

    [Header("Particles")]
    [SerializeField] ParticleSystem footsteps;
    ParticleSystem.EmissionModule footEmission;

    private float horizontalDirection;
    private bool changeDirection => ((rb.velocity.x > 0 && horizontalDirection < 0) || (rb.velocity.x < 0 && horizontalDirection > 0));
    scr_climasystem climaSystem;

    scr_jump jumpComponent;
    Rigidbody2D rb;
    Animator anim;
    bool onPlatform;

    private void Awake()
    {
        climaSystem = GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_climasystem>();
        jumpComponent = GetComponent<scr_jump>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        footEmission = footsteps.emission;
    }

    private void Update()
    {
        horizontalDirection = GetInput().x;
        anim.SetFloat("XSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("YSpeed", rb.velocity.y);
        anim.SetBool("isFalling", !jumpComponent.checkGrounded());
    }

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 1 && jumpComponent.grounded)
        {
            footEmission.rateOverTime = 35;
        }
        else
        {
            footEmission.rateOverTime = 0;
        }
        MoveCharacter();
        Flip();
        if (jumpComponent.checkGrounded())
        {
            if (climaSystem.getCurrentClima() == scr_climasystem.clima.rain || climaSystem.getCurrentClima() == scr_climasystem.clima.storm)
            {
                ApplyDrag("Rain");
            }
            else
            {
                ApplyDrag("Normal");
            }
        }
        else
        {
            ApplyDrag("Air");
        }
    }

    private void Flip()
    {
        if (horizontalDirection > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (horizontalDirection < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void MoveCharacter()
    {
        if (jumpComponent.checkGrounded())
        {
            Vector2 force = new Vector2(horizontalDirection, 0f) * groundAcceleration;
            rb.AddForce(force);
        }
        else
        {
            Vector2 force = new Vector2(horizontalDirection, 0f) * airAcceleration;
            rb.AddForce(force);
        }


        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
    }

    private void ApplyDrag(string drag)
    {
        switch (drag)
        {
            case "Normal":
                if (Mathf.Abs(horizontalDirection) == 0 || changeDirection)
                {
                    rb.drag = groundLinearDrag;
                }
                else
                {
                    rb.drag = 0f;
                }
                break;

            case "Air":
                rb.drag = airLinearDrag;
                break;

            case "Rain":
                if (Mathf.Abs(horizontalDirection) == 0 || changeDirection)
                {
                    rb.drag = rainLinearDrag;
                }
                else
                {
                    rb.drag = 0f;
                }
                break;
        }
    }

    void WalkCorrection()
    {
        Debug.Log("WalkCorrection");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.x != 0)
        {
            RaycastHit2D topRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + topRayAltitude), transform.right, rayDistance);
            RaycastHit2D bottomRay = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - bottomRayAltitude), transform.right, rayDistance);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + topRayAltitude), transform.right, Color.blue, rayDistance);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - bottomRayAltitude), transform.right, Color.blue, rayDistance);

            if (!topRay && bottomRay)
            {
                WalkCorrection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Threat"))
        {
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_levelstate>().OnPlayerHurt();
        }
        if (collision.CompareTag("Finish"))
        {
            GameObject.FindGameObjectWithTag("LevelController").GetComponent<scr_levelstate>().OnPlayerFinish();
        }

        if (collision.GetComponentInParent<scr_railplatform>() != null)
        {
            transform.SetParent(collision.gameObject.transform);
            collision.GetComponentInParent<scr_railplatform>().setRailActive();
            onPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<scr_railplatform>() != null)
        {
            transform.SetParent(null);
            onPlatform = false;
        }
    }

    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public bool isOnPlatform()
    {
        return onPlatform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 topVector = new Vector3(transform.position.x, transform.position.y + topRayAltitude, transform.position.z);
        Vector3 bottomVector = new Vector3(transform.position.x, transform.position.y + bottomRayAltitude, transform.position.z);
        Gizmos.DrawLine(topVector, new Vector3(transform.position.x + rayDistance, transform.position.y + topRayAltitude, transform.position.z));
        Gizmos.DrawLine(bottomVector, new Vector3(transform.position.x + rayDistance, transform.position.y + bottomRayAltitude, transform.position.z));
    }
}
