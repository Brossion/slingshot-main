using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public Vector3 offSet;

    public Vector3 firstOffSet;

    private Vector3 velocity = Vector3.zero;

    public float smoothSpeed = 0.125f;

    PlayerMovement playerMovement;

    void Start() 
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement.isGameStarted == true)
        {
            Vector3 desiredPosition = target.position + offSet;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            Vector3 desiredPosition = target.position + firstOffSet;
        }
        transform.LookAt(target);
    }
}
