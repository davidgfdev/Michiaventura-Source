using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_railplatform : MonoBehaviour
{
    [SerializeField] bool startsActive = true;
    [SerializeField] float speed;
    Vector3[] points;
    Vector3 target;
    int pointIndex = 0;
    int pointLenght = 0;
    GameObject platform;
    bool isResting = false;
    bool isActive = false;
    void Start()
    {
        isActive = startsActive ? true : false;
        PlatformSetup();
    }

    void Update()
    {
        if (isActive) PlatformMovement();
    }

    void PlatformSetup()
    {
        points = new Vector3[transform.childCount - 1];
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject o = transform.GetChild(i).gameObject;
            Debug.Log(o.name);
            if (o.GetComponent<SpriteRenderer>() == null)
            {
                points[pointLenght] = o.transform.position;
                pointLenght++;
            }
            else if (o.GetComponent<SpriteRenderer>() != null)
            {
                platform = o;
            }
        }
        target = points[pointIndex];
    }

    void PlatformMovement()
    {
        if (!isResting)
        {
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, target, 0.001f * speed);

            float distance = Vector2.Distance(platform.transform.position, target);
            if (distance < 0.1f)
            {
                StartCoroutine(PlatformRest());
            }
        }
    }

    IEnumerator PlatformRest()
    {
        isResting = true;
        yield return new WaitForSeconds(1);
        pointIndex = (pointIndex + 1) % points.Length;
        target = points[pointIndex];
        isResting = false;
    }

    public void setRailActive()
    {
        isActive = true;
    }
}
