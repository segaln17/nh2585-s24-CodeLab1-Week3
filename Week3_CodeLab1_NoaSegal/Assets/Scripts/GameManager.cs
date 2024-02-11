using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //there can only be one GameManager in a scene

    private int score = 0; //setting internal score variable

    public float timeLeft = 10; //declaring and initializing time variable

    public int Score //using a property to control Score
    { 
        get
        {
            return score; //pulls the private score variable's value out
        }
        set
        {
            score = value; //when changing the score, change it to the gotten value
            Debug.Log("score changed");

            if (score > HighScore)
            {
                HighScore = score; //if the score is greater than the last internally stored HighScore,
                //set the HighScore that's saved to the file to this value
            }
        }
    }

    int highScore = 0; //the highscore that isn't saved to the file should start at 0

    const string KEY_HIGH_SCORE = "High Score"; //sets a new key that can't be changed or messed with in naming because it's a constant

    int HighScore //the property for the HighScore saved to file
    {
        get
        {
            //pulling the value from the last highscore saved to the file
            //if the file exists, read its contents to get the highscore saved
            if (File.Exists(DATA_FULL_HS_FILE_PATH))
            {
                string fileContents = File.ReadAllText(DATA_FULL_HS_FILE_PATH);
                highScore = int.Parse(fileContents);
            }

            return highScore; //return this value
        }
        set
        {
            highScore = value; //set the highscore to this new value
            Debug.Log("New High Score");
            string fileContent = "" + highScore; //add this to the file as a string to overwrite last highscore

            //check to see if a file exists already and if it doesn't, make one to store the highscore
            if (!Directory.Exists(Application.dataPath + DATA_DIR))
            {
                Directory.CreateDirectory(Application.dataPath + DATA_DIR);
            }
            //put the new highscore into the file
            File.WriteAllText(DATA_FULL_HS_FILE_PATH, fileContent);
        }
    }

    public TextMeshProUGUI scoreText; //declare scoreText variable so we can assign it in the inspector
    
    //declare constant strings for the score saving code
    const string DATA_DIR = "/Data/";
    private const string DATA_HS_FILE = "hs.txt";
    
    //declare string that isn't constant because it depends on the operating system so we can't control that:
    string DATA_FULL_HS_FILE_PATH;
    
    //Awake function to run once the game starts, to ensure the singleton remains the only instance:

    void Awake()
    {
        if (instance == null) //if there are no other singletons of this type
        {
            instance = this;
            //commented this out because I don't need it for now, because I only have one level, but I wanted to make sure I remembered to include it for how singletons usually work
            //DontDestroyOnLoad(gameObject); //keep this gameobject in the scene
        }
        else //if there is already a singleton of this type:
        {
            Destroy(gameObject); //get rid of it
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //define the data path:
        DATA_FULL_HS_FILE_PATH = Application.dataPath + DATA_DIR + DATA_HS_FILE;
    }

    // Update is called once per frame
    void Update()
    {
        //count down to the end of the timer each second:
        timeLeft -= Time.deltaTime;
        
        //update the score text display:
        scoreText.text = "Score: " + score + "\nHigh Score: " + HighScore + "\nTime Remaining: " + timeLeft; 
        //HighScore is used because it's the one saved to the file --> overall high score

        //if out of time, go to the end screen
        if (timeLeft <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
