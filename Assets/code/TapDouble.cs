using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TapDouble : MonoBehaviour
{
    float tapTimer = 0f;
    public float doubleTapInterval = 0.2f;
    bool tapped = false;
    Rigidbody rb;
    public int jumpPower = 5;
    public int forwardSpeed = 20;

    bool grounded = false;
    public int score = 0;
    TextMeshPro scoreText;
    Vector3 startPosition; 

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshPro>();
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If tapped is true, start timer.

        if (tapped)
        {
            tapTimer += Time.deltaTime;
            // If it has been more than 0.2 seconds...
            if(tapTimer > doubleTapInterval)
            {
                SingleTap();
                tapped = false;
            }

        }
        if(Input.anyKeyDown && grounded)
        {
            // if tapped within 0.2 seconds, call doubletap
            if(tapped && grounded)
            {
                DoubleTap();
                tapped = false;
            } 
            else
            {
                tapped = true;
            }
        }
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.right * forwardSpeed);
    }
    void SingleTap() 
    {
        Debug.Log("<color=magenta>Single Tap!</color>");
        Debug.Log("Timer = " + tapTimer);
        tapTimer = 0;

        // Change the color to a random color
        //this.GetComponent<Renderer>().material.color = Random.ColorHSV();

        rb.AddRelativeForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    void DoubleTap() 
    {
        Debug.Log("<color=cyan>Double Tap!</color>");
        Debug.Log("Timer = " + tapTimer);
        tapTimer = 0;

        // Increase the size by 20%
        //this.transform.localScale += Vector3.one * 0.2f;
        // If scale is greater than 5, reset to 1.
        //if(this.transform.localScale.x > 5)
        //{
            //this.transform.localScale = Vector3.one;
        //}

        rb.AddRelativeForce(Vector3.up * jumpPower * 2, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        else if(other.gameObject.CompareTag("Pickup"))
        {
            score += 1000;
            scoreText.text = "Score = " + score; 
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Finish"))
        {
            // reset everything! Let the player keep their own high score.
            score = 0;
            this.transform.position = startPosition;
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

}
