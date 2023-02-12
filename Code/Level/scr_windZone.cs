using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_windZone : MonoBehaviour
{
    //Variables
    //Cuanto dura el viento activo, en segundos.
    [SerializeField] int m_segundosActivo;
    //Cuanto dura el viento desactivado, en segundos.
    [SerializeField] int m_segundosInactivo;

    //Guardamos el siguiente tick en esta variable.
    float m_nextTick;
    bool m_activado;
    ParticleSystem m_particleSystem;
    GameObject box;

    //Al instanciarse, recoge el SpriteRenderer y lo guarda en la variable.
    private void Awake()
    {
        m_particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    //Al empezar, activar� la corutina donde activa-desactiva el viento.
    private void Start()
    {
        StartCoroutine(ActivarDesactivarViento());
        m_activado = true;
    }

    //Detectamos que ha entrado la caja y la guardamos.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            box = collision.gameObject;
        }
    }

    //Detectamos que la caja ha salido y la eliminamos.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            box = null;
        }
    }

    //Activa o desactiva el tiempo, esperando los segundos indicados y ajustar� el color para que se vea si esta activado o no.
    IEnumerator ActivarDesactivarViento()
    {
        yield return new WaitForSeconds(m_segundosInactivo);
        m_activado = true;
        m_particleSystem.Play();
        GetComponent<BuoyancyEffector2D>().enabled = true;
        yield return new WaitForSeconds(m_segundosActivo);
        m_activado = false;
        m_particleSystem.Stop();
        GetComponent<BuoyancyEffector2D>().enabled = false;
        StartCoroutine(ActivarDesactivarViento());
    }

    private void OnDrawGizmos()
    {
        //Gizmo para indicar hacia donde ir� el viento.
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 5);
    }
}
