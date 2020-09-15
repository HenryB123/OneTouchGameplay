// Henry Brinckerhoff MobGameDev Fall 2020

// create a function/method named 'Create Cube' with a return type of 'void'
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Program : MonoBehaviour
{

    public bool myBoolean = true;
    [Tooltip("Set to zero to get a random value.")]
    private float testNumber;
    public int cubeHeight = 0;
    public int givenNum;

    // Start is called before the first frame update
    void Start()
    {
        if(myBoolean == true)
        {
            Debug.Log("myBoolean is true");
        }
        else
        {
            Debug.Log("myBoolean is false");
        }


        DoubleTheNumber(2);
        DoubleTheNumber(4);
        DoubleTheNumber(6);


//we are building a loop here
//while(this test is true {keep looping})
        int counter = 0;                               //this number keeps track of how many times we've looped
        while(counter < Random.Range(5, 51))         //while counter is less than a random number,  {run this code}
        {
            counter += 1;                           //add one to counter
            CreateCube(counter);                    //call the CreateCube function and send it counter
        }

// second assignment
        
        if(testNumber == 0)
        {
            testNumber = Random.value;
        }

        Debug.Log("TestNumber = " + testNumber);

        if(testNumber > .9)
        {
            Debug.Log("TestNumber is greater than 0.9");
        }
        else if(testNumber > 0.5)
        {
            Debug.Log("TestNumber is greater than 0.5");
        }
        else
        {
            Debug.Log("TestNumber is less than 0.5");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            cubeHeight += 1;
            CreateCube(cubeHeight);
        }
    }

    void CreateCube(int givenHeight)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, givenHeight, 0);
    }

    void DoubleTheNumber(int givenNum)
    {
        givenNum *= 2;
        Debug.Log("GivenNum x 2 = " + givenNum);
    }
}
