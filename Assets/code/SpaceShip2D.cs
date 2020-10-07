using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SpaceShip2D : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed = 10;
    public float rotSpeed = 10;
    public int health = 100;
    public int score = 0;

    private float turnDirection = 0;

    public bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject youWin;
    public GameObject youLose;

    private AudioSource deathAudio;

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        pauseMenu.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
        Load();

        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1;
        }
        else
        {
            turnDirection = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        */

        if(score > 500)
        {
            Debug.Log("You Win");
            youWin.SetActive(true);
            Time.timeScale = 0;
        }

        if(health < 0)
        {
            Debug.Log("You Lose");
            youLose.SetActive(true);
            deathAudio.Play();
            Reset();
            StartCoroutine(deathRespawn());
        }

        healthSlider.value = health;
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * speed);
        rb.AddTorque(turnDirection * rotSpeed, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Asteroid"))
        {
            health -= 10;
            
        }
        else if(other.gameObject.CompareTag("Pickup"))
        {
            score += 50;
        }
    }

    public void Pause()
    {
        if(gameIsPaused)
        {
            //unpause the game
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Load();
        }
        else
        {
            //pause the game because it is not paused.
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Save();
        }
        //pauseMenu.SetActive(!gameIsPaused);

        //if pause, make it not paused. if not paused, make it paused.
        gameIsPaused = !gameIsPaused;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("Score",score);

    }

    public void Load()
    {
        health = PlayerPrefs.GetInt("Health");
        score = PlayerPrefs.GetInt("Score");
    }

    public void Reset()
    {
        PlayerPrefs.SetInt("Health", 100);
        PlayerPrefs.SetInt("Score", 0);
        Load();
    }

    public void UpdateTurnDirection(int direction)
    {
        turnDirection = direction;
    }

    IEnumerator deathRespawn()
    {
        Debug.Log("deathRespawn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
