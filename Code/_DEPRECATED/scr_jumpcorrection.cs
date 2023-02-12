using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_jumpcorrection : MonoBehaviour
{
    Rigidbody2D rb;
    Transform parentTransform;
    [SerializeField] Vector3 raycastIzq;
    [SerializeField] Vector3 raycastDer;
    [SerializeField] float correccion;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        parentTransform = transform.parent;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(parentTransform.position + raycastIzq, Vector2.up);
        RaycastHit2D raycastRight = Physics2D.Raycast(parentTransform.position + raycastDer, Vector2.up);
        if (rb.velocity.magnitude > 0)
        {
            if (raycastLeft)
            {
                parentTransform.position = new Vector3(parentTransform.position.x - correccion, parentTransform.position.y, parentTransform.position.z);
                Debug.Log("Corrección izquierda");
            }
            else if (raycastRight)
            {
                parentTransform.position = new Vector3(parentTransform.position.x + correccion, parentTransform.position.y, parentTransform.position.z);
                Debug.Log("Corrección derecha");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + raycastDer, Vector2.up * 10);
        Gizmos.DrawRay(transform.position + raycastIzq, Vector2.up * 10);
    }
}
