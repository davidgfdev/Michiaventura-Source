using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_parallax : MonoBehaviour
{
    [SerializeField] float parallaxMiltiplier;
    GameObject cameraHolder;
    GameObject Player;

    private Transform cameraTransform;
    private Vector3 previousCamPos;
    private float spriteWidth, startPos;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraHolder = GameObject.Find("CameraHolder");
        cameraTransform = cameraHolder.transform;
        previousCamPos = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }

    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCamPos.x) * parallaxMiltiplier;
        float moveAmount = cameraTransform.position.x * (1 - parallaxMiltiplier);
        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCamPos = cameraTransform.position;
    }
}
