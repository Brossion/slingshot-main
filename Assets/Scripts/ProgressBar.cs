using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    private float maximum;
    private float current;
    public Image mask;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        maximum = 10f;
        current = 0f;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill() 
    {
        current = playerMovement.tapCount;
        float fillAmount = current / maximum;
        mask.fillAmount = fillAmount;
    }
}
