using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject[] panels, GC, tryAgainTexts;

    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        panels[0].SetActive(true);
        playerMovement = GC[0].GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartTheGame() 
    {
        panels[0].SetActive(false);
        StartCoroutine(playerMovement.AtStart());
    }
    public void ReleaseTheStone() 
    {
        playerMovement.isRotationStarted = true;
        playerMovement.isGameStarted = true;
        Time.timeScale = 1;
        playerMovement.forwardSpeed = 15f;
        panels[4].SetActive(false);
    }
    public void Finished() 
    {
        panels[2].SetActive(false);
    }
    public void TryAgain()
    {
        panels[1].SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        // this means to reload the current scene that you're playing on.
        //Other way of doing it:
        //SceneManager.LoadScene("Level 1");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            //if the next scene index is the same as the number that we have, like the total number of scenes
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
