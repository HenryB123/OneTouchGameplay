using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMove : MonoBehaviour
{
    // Move object using accelerometer
    public float speed = 10.0f;
    public float rotSpeed = 60f;

    //Transform body;
    void Start()
    {
        // "body" variable is equal to the "Body" object in the Player object
        //body = this.transform.GetChild(0);
    }
    void Update()
    {
        Vector3 dir = Vector3.zero;

        // we assume that device is held parallel to the ground
        // and Home button is in the right hand

        // remap device acceleration axis to game coordinates:
        //  1) XY plane of the device is mapped onto XZ plane
        //  2) rotated 90 degrees around Y axis
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        // clamp acceleration vector to unit sphere
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        // Make it move 10 meters per second instead of 10 meters per frame...
        dir *= Time.deltaTime;

        // Move object
        transform.Translate(Vector3.forward * speed * Time.deltaTime);      // move forward at 10m per second.
        transform.Translate(dir * speed);                                       // move left/right and forward/back.
        transform.Rotate(0,0,-dir.x * rotSpeed);                           // rotate the body around the z-axis.
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("Respawn");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}