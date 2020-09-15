﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public TextMeshProUGUI debugText;
    public TextMeshProUGUI scoreText;
    public Slider powerSlider;
    public bool phoneIsAttached = true;
    public Transform bulletSpawn;
    [Tooltip("This is your gun barrel, or your tank.")]
    public Transform gun;
    public Rigidbody2D bullet;
    public GameObject enemyPrefab;
    public float timer = 0;
    public bool mouseIsDown = false;
    public float minPower = 50f, maxPower = 200;
    public int score = 0;
    [Tooltip("The X and Y min and max spawn positions for enemies.")]
    public Vector2 min, max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         

        if(phoneIsAttached)
        {
            //use Input.touch
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                debugText.text = "Pressed! Timer = " + timer;
                timer += Time.deltaTime * 30;
                powerSlider.value = timer;
                
                if(touch.phase == TouchPhase.Ended)
                {
                    debugText.text = "Timer = " + timer;
                    Shoot();
                }
            }
        }
        else
        {
            // use mouse0
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                debugText.text = "You are clicking the screen!";
                mouseIsDown = true;
            }
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                mouseIsDown = false;
                debugText.text = "Timer = " + timer;
                Shoot();
            }
            if(mouseIsDown)
            {
                timer += Time.deltaTime * 10;
                debugText.text = "Clicked! Timer = " + timer;
                powerSlider.value = timer;
            }
        }
    }
    public void Shoot()
    {
        // add a ceiling to the timer's max amount.
        if(timer > maxPower) timer = maxPower;

        debugText.text = "Pow! Power = " + (timer * 100).ToString("0.0");

        Rigidbody2D rb = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        rb.AddRelativeForce(Vector2.up * 10 * timer);

        timer = minPower;   //reset timer
        powerSlider.value = timer;

    }

    public void UpdateScore(int givenScore){
        score += givenScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void RotateGun(int dir)
    {
        gun.transform.Rotate(0, 0, dir * 15);
    }

    public void SpawnEnemy()
    {
        // the for loop has the counter built in.
        int totalEnemies = Random.Range(4, 9);
        for(int i = 0; i < totalEnemies; i += 1)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
        
    }
}
