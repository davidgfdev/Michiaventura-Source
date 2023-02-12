using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_hook : MonoBehaviour
{
    [Header("Prefab, spawners y velocidad del gancho")]
    [SerializeField] GameObject hookObject;
    [SerializeField] Transform HookPositionTop, HookPositionForward;
    [SerializeField] float hookSpeed;
    [SerializeField] AudioClip hookSFX;

    [Header("Velocidad del jugador y rango")]
    [SerializeField] float movementSpeed;
    [SerializeField] float hookRange;

    [Header("Camera")]
    [SerializeField] float freezeTime;
    [SerializeField] float shakeMagnitude;

    bool canHook, moveToHook, moveBoxToHook;
    GameObject hookInstance;
    scr_camera cameraScript;
    Vector3 hookDestinationTarget;
    Rigidbody2D rb;
    float gameGravityScale;
    GameObject boxToMove;
    Animator anim;
    AudioSource audioSource;

    private void Awake()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<scr_camera>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        gameGravityScale = rb.gravityScale;
        canHook = true;
    }

    void Update()
    {

        if (canHook && Input.GetKeyDown(KeyCode.C) && !GetComponent<scr_playermovement>().isOnPlatform())
        {
            ShootHook();
        }

        if (hookInstance != null)
        {
            if (Vector2.Distance(transform.position, hookInstance.transform.position) > hookRange)
            {
                StopHook();
            }
        }

    }

    private void FixedUpdate()
    {
        if (moveToHook)
        {
            transform.position = Vector3.MoveTowards(transform.position, hookDestinationTarget, movementSpeed / 100);

            if (Vector3.Distance(transform.position, hookDestinationTarget) < 0.1f)
            {
                StopHook();
            }
        }

        if (moveBoxToHook)
        {
            //boxToMove.transform.position = Vector3.MoveTowards(boxToMove.transform.position, transform.position, movementSpeed / 100);
            Vector3 boxForce = new Vector3(Mathf.Sign(transform.position.x - boxToMove.transform.position.x) * 1, 0, 0);
            boxToMove.GetComponent<Rigidbody2D>().AddForce(boxForce * movementSpeed);

            if (Vector3.Distance(boxToMove.transform.position, transform.position) < 1.5f)
            {
                StopHook();
                boxToMove.GetComponent<Rigidbody2D>().gravityScale = 5;
            }
        }
    }

    private void ShootHook()
    {
        rb.bodyType = RigidbodyType2D.Static;
        canHook = false;
        rb.gravityScale = 0;
        Transform targetPosition = HookPositionForward;
        audioSource.clip = hookSFX;
        audioSource.Play();

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            targetPosition = HookPositionTop;
            anim.SetTrigger("Hook_Up");
        }
        else
        {
            anim.SetTrigger("Hook_Forward");
        }

        hookInstance = Instantiate(hookObject, targetPosition.position, targetPosition.rotation);
        hookInstance.GetComponent<scr_hookcollider>().speed = hookSpeed;
    }

    void StopHook()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        moveToHook = false;
        moveBoxToHook = false;
        canHook = true;
        rb.gravityScale = gameGravityScale;
        Destroy(hookInstance);
    }

    //Esta funci�n es llamada desde el HookCollider para pasarle el resultado del gancho y el objeto que haya tocado.
    public void OnHookCollide(Vector2 hookDestination, string result, Collision2D collision)
    {
        switch (result)
        {
            case "HookableWall":
                StartCoroutine("OnHookSuccess", hookDestination);
                break;
            case "Failed":
                StopHook();
                break;
        }
    }

    IEnumerator OnHookSuccess(Vector2 hookDestination)
    {
        float _distanceToDestination = Vector2.Distance(transform.position, hookDestination);
        if (_distanceToDestination > 1.5f)
        {
            cameraScript.CameraShake(freezeTime, shakeMagnitude);
            yield return new WaitForSeconds(freezeTime);
            hookDestinationTarget = new Vector3(hookDestination.x, hookDestination.y, transform.position.z);
            moveToHook = true;
        }
        else
        {
            StopHook();
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, hookRange);
    }

    public void resetValues()
    {
        StopHook();
    }
}
