using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class scr_sectionGizmo : MonoBehaviour
{
    [SerializeField] Vector3 gizmoSize;
    [SerializeField] Color gizmoColor;
    [SerializeField] float gizmoBorderSize;
    private void OnDrawGizmos()
    {
        //Handles.Label(transform.position, gameObject.name);
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, gizmoSize);
    }
}
