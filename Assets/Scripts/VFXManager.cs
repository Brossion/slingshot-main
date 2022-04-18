using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public GameObject[] VFX;
    public GameObject target;

    PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && playerMovement.isTapCount)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(playerMovement.startTouch);

            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null)

                    Instantiate(target, hit.point, transform.rotation);
        }*/
    }
}
