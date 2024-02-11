using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDScript : MonoBehaviour
{
    //to make it a singleton:
    public static WASDScript Instance; //there can only be one

    public Rigidbody2D rb2d; //rigidbody 2d to be assigned in inspector
    public int forceAmount = 20; //declaring and initializing forceAmount

    void Awake() //to delete extraneous instances:
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this; //this is the one now
        }
        else
        {
            Destroy(gameObject); //get rid of this one if there's another already
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the rigidbody2d of the gameObject this script is on
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { //movement every frame, if keys are pressed to indicate directions:

        if (Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(Vector2.up * forceAmount); //the up direction multiplied by the forceAmount
        }
        
    }
}
