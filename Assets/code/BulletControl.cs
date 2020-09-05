using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletControl : MonoBehaviour
{
    InputManager mgr;
    
    void Start()
    {
        mgr = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Enemy")){
            Destroy(other.gameObject);
            mgr.UpdateScore(1000);
        }
        Destroy(this.gameObject);
    }
}
