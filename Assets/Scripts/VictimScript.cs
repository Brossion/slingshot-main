using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimScript : MonoBehaviour
{
    [SerializeField]
    float forwardSpeed;

    public Animator[] goodGuyAnimators;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isGameStarted == true)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            goodGuyAnimators[0].SetBool("isRunning", true);
        }
        if (playerMovement.isVictim == true)
        {
            transform.position += transform.forward * 0 * Time.deltaTime;
            goodGuyAnimators[0].SetBool("isRunning", false);
            goodGuyAnimators[0].SetBool("isFailed", true);
        }
    }
}
