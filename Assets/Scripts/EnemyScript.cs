using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyScript : MonoBehaviour
{
    public float forwardSpeed;

    public Animator[] badGuyAnimators;

    PlayerMovement playerMovement;

    public GameObject bosss;

    public AudioClip laughing;

    EnemyScript boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = bosss.GetComponent<EnemyScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isGameStarted == true)
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            badGuyAnimators[0].SetBool("isLaughing", false);
            badGuyAnimators[0].SetBool("isRunning", true);
            boss.badGuyAnimators[0].SetBool("isRunning", false);
        }
    }
}
