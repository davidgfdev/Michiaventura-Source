using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_camera : MonoBehaviour
{
    [Header("Parametros de velocidad de camara")]
    [SerializeField] float speed;
    [SerializeField] float maxAccelerationOffset;
    [SerializeField] float accelerationTime;

    [Header("Offset Visual de la C�mara")]
    [SerializeField] Vector2 originalOffset;

    [Header("Referencia del Player")]
    [SerializeField] GameObject objective;

    [Header("Referencia del CameraHolder")]
    [SerializeField] GameObject cameraHolder;

    float acceleration;
    Vector3 currentVelocity;
    Vector3 target;
    bool isCameraShaking;
    Vector2 positionOffset;

    private void Start()
    {
        positionOffset = originalOffset;
        target = new Vector3(objective.transform.position.x + positionOffset.x, objective.transform.position.y + positionOffset.y, transform.position.z);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            positionOffset.y = originalOffset.y + 3;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            positionOffset.y = originalOffset.y;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            positionOffset.y = originalOffset.y - 3;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            positionOffset.y = originalOffset.y;
        }
    }

    void LateUpdate()
    {
        if (!isCameraShaking)
        {
            if (objective.GetComponent<Rigidbody2D>() != null)
            {

                DisplaceTarget();

            }
            cameraHolder.transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, speed);
        }
    }

    void DisplaceTarget()
    {
        if (Mathf.Abs(GetVelocity()) > 0.1f)
        {
            float factor = acceleration * GetVelocity();
            target = new Vector3(objective.transform.position.x + factor + positionOffset.x, objective.transform.position.y + positionOffset.y, transform.position.z);
        }
        else
        {
            target = new Vector3(objective.transform.position.x + positionOffset.x, objective.transform.position.y + positionOffset.y, transform.position.z);
        }
    }

    float GetVelocity()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.4f)
        {
            acceleration += Mathf.Lerp(0, 1, accelerationTime / 10);
        }
        acceleration = Mathf.Clamp(acceleration, 0, maxAccelerationOffset);
        Vector3 velocity = objective.GetComponent<Rigidbody2D>().velocity;
        return velocity.x;
    }

    public void CameraShake(float time, float magnitude)
    {
        if (!isCameraShaking) StartCoroutine(CameraShakeCorutine(time, magnitude));
    }

    IEnumerator CameraShakeCorutine(float time, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < time)
        {
            isCameraShaking = true;
            float shakeX = Random.Range(-1, 1) * magnitude;
            float shakeY = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(shakeX, shakeY, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        isCameraShaking = false;
        transform.localPosition = originalPos;
    }
}
