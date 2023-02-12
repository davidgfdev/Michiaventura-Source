using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_grab : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform frontChecker;
    [SerializeField] GameObject topChecker;

    [Header("Lanzamiento")]
    [SerializeField] float throwForce;

    GameObject box;

    bool hasObject;

    void Start()
    {
        hasObject = false;
    }

    void Update()
    {
        if (hasObject)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StopGrab(false);
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                StopGrab(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Collider2D boxToGrab = Physics2D.OverlapCircle(frontChecker.position, 0.1f);
                if (boxToGrab != null && boxToGrab.CompareTag("Box") && Input.GetKey(KeyCode.X))
                {
                    GrabBox(boxToGrab.gameObject);
                }
            }
        }

        
        if (Input.GetKeyDown(KeyCode.X) || (Input.GetKeyDown(KeyCode.C)))
        {
            if (hasObject)
            {
                StopGrab(false);
            }
            else
            {
                Collider2D boxToGrab = Physics2D.OverlapCircle(frontChecker.position, 0.1f);
                if (boxToGrab != null && boxToGrab.CompareTag("Box") && Input.GetKey(KeyCode.X))
                {
                    GrabBox(boxToGrab.gameObject);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hasObject)
            {
                StopGrab(true);
            }
        }
    }

    public void GrabBox(GameObject boxToGrab)
    {
        box = boxToGrab;
        box.gameObject.transform.SetParent(topChecker.transform);
        box.transform.localPosition = Vector2.zero + new Vector2(0, 1);
        box.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        hasObject = true;
        GetComponent<scr_jump>().enabled = !hasObject;
        GetComponent<scr_hook>().enabled = !hasObject;
    }

    void StopGrab(bool impulse)
    {
        Rigidbody2D boxrb = box.GetComponent<Rigidbody2D>();
        box.transform.SetParent(null);
        boxrb.bodyType = RigidbodyType2D.Dynamic;
        boxrb.velocity = Vector2.zero;
        if (impulse)
        {
            boxrb.AddForce(new Vector2(-box.transform.right.x, 0.5f) * throwForce, ForceMode2D.Impulse);
        }
        else
        {
            box.transform.position = GameObject.Find("HookForward").transform.position + new Vector3(2 * Mathf.Sign(transform.forward.x), 0, 0);
        }
        box = null;
        hasObject = false;
        GetComponent<scr_jump>().enabled = !hasObject;
        GetComponent<scr_hook>().enabled = !hasObject;
    }
}
