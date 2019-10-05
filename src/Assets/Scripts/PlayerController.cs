using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject player;
    private int playerSpeed;
    private int horizontalMultiplyer;
    private float playerSize;
    private int maxspeed;
    public Text scoreCountText;
    public Text loseText;
    public AudioSource coinSounds;



    private Rigidbody rb;
    private int scoreCount;
    public float playerDistance;


    void Start()
    {

        playerSpeed = 7;
        horizontalMultiplyer = 1;
        rb = GetComponent<Rigidbody>();
        scoreCount = 0;
        playerDistance = 0;
        SetScoreCountText();
        setPlayerStats();
    }


    //FixedUpdate is used vs Update because physics are being applied. When you are using physics in unity use FixedUpdate, because reasons. Ricks class

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        playerDistance = player.transform.position.z;
        SetScoreCountText();

        moveHorizontal = horizontalMultiplyer * moveHorizontal;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 4f);

        rb.AddForce(movement * playerSpeed);

        

        if (rb.velocity.magnitude > playerSpeed)
        {
            rb.velocity = rb.velocity.normalized * playerSpeed;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            coinSounds.Play();
            other.gameObject.SetActive(false);
            scoreCount = scoreCount + 100;
            SetScoreCountText();
            GameObject.FindWithTag("Data").GetComponent<characterManager>().setMoney(100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("DestructableBuilding"))
        {
            scoreCount = scoreCount + 20;
            SetScoreCountText();
            projectObject(collision.gameObject);
            delayDestroy(collision.gameObject);
            GameObject.FindWithTag("Data").GetComponent<characterManager>().setMoney(20);
        }

        if (collision.gameObject.CompareTag("DestructableTree"))
        {
            scoreCount = scoreCount + 10;
            SetScoreCountText();
            projectObject(collision.gameObject);
            delayDestroy(collision.gameObject);
            GameObject.FindWithTag("Data").GetComponent<characterManager>().setMoney(10);
        }

        if (collision.gameObject.CompareTag("Death"))
        {
            pauseMenu.SetActive(true);
            resumeButton.SetActive(false);
            Time.timeScale = 0f;
            loseGame();
            GameObject.FindWithTag("Data").GetComponent<characterManager>().setHighscore(playerDistance);
        }

    }

    private void SetScoreCountText()
    {
        scoreCountText.text = "Money: " + scoreCount.ToString() + "\nDistance: " + playerDistance.ToString();
    }

    public void loseGame()
    {
        loseText.text = "You have died.\nReturn to main menu.";
    }

    private void delayDestroy(GameObject destroyee)
    {
        Object.Destroy(destroyee.gameObject, 3.0f);
    }

    private void projectObject(GameObject project)
    {
        project.gameObject.GetComponent<Rigidbody>().useGravity = false;
        project.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 25);
    }

    private void setPlayerStats()
    {
        playerSpeed += GameObject.FindWithTag("Data").GetComponent<characterManager>().getSpeed();
        horizontalMultiplyer += GameObject.FindWithTag("Data").GetComponent<characterManager>().getAgility();
        playerSize = GameObject.FindWithTag("Data").GetComponent<characterManager>().getsize();

        playerSize = playerSize * .1f;

        player.transform.localScale += new Vector3(playerSize, playerSize, playerSize);
    }

    public float getDistance()
    {
        return playerDistance;
    }
}
