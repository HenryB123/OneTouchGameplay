using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bat : MonoBehaviour
{
    public int health = 10;
    public int bulletDmg = 10;
    public int score = 0;

    [SerializeField]
    private InputManager inputManagerObj;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Got hit by a bullet.");
            health -= bulletDmg;
            Destroy(other.gameObject);

            if(health <= 0)
            {
                //Destroy(this.gameObject);
                score = score + 1;
            }
        }
    }
}
