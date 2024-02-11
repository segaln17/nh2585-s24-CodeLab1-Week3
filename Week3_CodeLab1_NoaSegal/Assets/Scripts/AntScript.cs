using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AntScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //get the mouse position and send a raycast to see if there's something there
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint((Input.mousePosition));
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            
            //if the raycast hits something:
            if (hit.collider != null)
            {
                //score goes up by 1 if something's there
                GameManager.instance.Score++;
                
                //the thing that is hit (the ant) gets reassigned to a new spot on the screen
                hit.collider.gameObject.transform.position = new Vector3(
                    Random.Range(-7, 7), Random.Range(-7, 7));

                //below is the other way I tried it first, when this code was on the ants themselves:
                /*
                //making sure that the gameObject that is affected by the next part of code is specifically this one instead of the whole class
                if (hit.collider.gameObject == gameObject)
                {
                    //if hit, score goes up by 1
                    GameManager.instance.Score++;
                    //ant gets reassigned to a new spot on the screen
                    gameObject.transform.position = new Vector3(
                        Random.Range(-7, 7), Random.Range(-7, 7));
                }

                */
            }
            
        }
    }

    
    }
