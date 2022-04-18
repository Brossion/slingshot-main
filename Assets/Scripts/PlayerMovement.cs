using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public bool isRotationStarted, isFinished, isTapCount, isBossDead, isGameStarted, isVictim;

    private bool isTapping;

    private Vector3 _mouseOffset, _rotation;

    public Vector3 startTouch;

    private float _sensitivity;

    public float forwardSpeed, killedEnemy;

    public float tapCount;

    [SerializeField]
    float enemyAmount;

    public Camera[] cameras;

    public GameObject bossEnemyScript;

    public Animator slingShotMan;

    Rigidbody rb;
    UiManager uiManager;
    VFXManager vFX;
    EnemyScript boss;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isVictim = false;
        tapCount = 0;
        isBossDead = false;
        isTapCount = false;
        killedEnemy = 0;
        isFinished = false;
        boss = bossEnemyScript.GetComponent<EnemyScript>();
        rb = GetComponent<Rigidbody>();
        vFX = FindObjectOfType<VFXManager>();
        uiManager = FindObjectOfType<UiManager>();
        isRotationStarted = false;
        _sensitivity = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTapCount == false && isGameStarted == true)
        {
            Movement();
        }
        else if (isTapCount == true)
        {
            Tapping();
        }
        Finish();
    }

    private void Movement() 
    {
        transform.position += transform.forward * forwardSpeed * Time.deltaTime; // Make our character move forward
        //rb.velocity = transform.forward * forwardSpeed * Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && !isRotationStarted && !isFinished)
        {
            
            startTouch = Input.mousePosition;
            isRotationStarted = true;
        }
        else if (Input.GetMouseButton(0) && isRotationStarted && !isFinished)
        {
            Controller();
        }
        else if (Input.GetMouseButtonUp(0) && isRotationStarted && !isFinished)
        {
            isRotationStarted = false;
        }
    }
    private void Tapping() 
    {
        if (Input.GetMouseButtonDown(0) && tapCount < 10)
        {
            tapCount += 1;
        }
    }
    private void Controller()
    {
        _mouseOffset = (Input.mousePosition - startTouch);
        startTouch = Input.mousePosition;

        _rotation.y = (_mouseOffset.x) * _sensitivity;
        _rotation.x = -(_mouseOffset.y) * _sensitivity;

        transform.eulerAngles += _rotation;

        float angle = transform.localEulerAngles.x;
        float xDeg = (angle > 180) ? angle - 360 : angle;

        xDeg = Mathf.Clamp(xDeg, -60, 60);

        float angleY = transform.localEulerAngles.y;
        float yDeg = (angleY > 180) ? angleY - 360 : angleY;

        yDeg = Mathf.Clamp(yDeg, -60, 60);

        transform.rotation = Quaternion.Euler(xDeg, yDeg, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bina")
        {
            isFinished = true;
            Time.timeScale = 0.05f;
            if (isBossDead == true)
            {
                return;
            }
            else if (isBossDead == false)
            {
                uiManager.tryAgainTexts[0].SetActive(true);
                uiManager.TryAgain();
            }
        }
        if (other.gameObject.tag == "OutOfArea")
        {
            isFinished = true;
            Time.timeScale = 0.05f;
            if (isBossDead == true)
            {
                return;
            }
            else if (isBossDead == false)
            {
                uiManager.tryAgainTexts[3].SetActive(true);
                uiManager.TryAgain();
            }
        }
        if (other.gameObject.tag == "TooHigh")
        {
            isFinished = true;
            Time.timeScale = 0.05f;
            if (isBossDead == true)
            {
                return;
            }
            else if (isBossDead == false)
            {
                uiManager.tryAgainTexts[2].SetActive(true);
                uiManager.TryAgain();
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            killedEnemy += 1;
            Destroy(other.gameObject);
            StartCoroutine(SpeedUp());
        }
        if (other.gameObject.tag == "Boss")
        {
            if (forwardSpeed == 25)
            {
                isBossDead = true;
                killedEnemy += 1;
                isFinished = true;
                uiManager.panels[2].SetActive(true);
                StartCoroutine(SpeedUp());
                Time.timeScale = 0.05f;
                Destroy(other.gameObject);
            }
            else
            {
                isBossDead = false;
                isFinished = true;
                Time.timeScale = 0.05f;
                uiManager.TryAgain();
            }
        }
        if (other.gameObject.tag == "TappingPlane")
        {
            uiManager.panels[3].SetActive(true);
            StartCoroutine(TapFast());
        }
        if (other.gameObject.tag == "Victim")
        {
            isVictim = true;
            isFinished = true;
            Time.timeScale = 0.05f;
            uiManager.tryAgainTexts[1].SetActive(true);
            uiManager.TryAgain();
        }
    }
    private IEnumerator SpeedUp() 
    {
        forwardSpeed = 20f;
        vFX.VFX[0].SetActive(true);
        vFX.VFX[1].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        vFX.VFX[1].SetActive(false);
        forwardSpeed = 17.5f;
        yield return new WaitForSeconds(1f);
        forwardSpeed = 15f;
        vFX.VFX[0].SetActive(false);        
    }
    private IEnumerator TapFast() 
    {
        isTapCount = true;
        forwardSpeed = 0;
        isRotationStarted = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(3.5f);
        uiManager.panels[3].SetActive(false);
        isTapCount = false;
        isRotationStarted = false;
        forwardSpeed = 15 + tapCount;
        vFX.VFX[0].SetActive(true);
    }
    public IEnumerator AtStart() 
    {
        isGameStarted = false;
        cameras[1].gameObject.SetActive(true);
        boss.badGuyAnimators[0].SetBool("isLaughing", true);
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayClipAtPoint(boss.laughing, new Vector3(-11.0600004f, -4.71999979f, 237.229996f));
        yield return new WaitForSeconds(3.5f);
        cameras[1].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
        //slingShotMan.SetBool("isSwinging", true);
        yield return new WaitForSeconds(0.2f);
        uiManager.panels[4].SetActive(true);
    }
    void Finish() 
    {
        if (killedEnemy == enemyAmount)
        {
            uiManager.panels[2].SetActive(true);
            Time.timeScale = 0.05f;
        }
    }
}
