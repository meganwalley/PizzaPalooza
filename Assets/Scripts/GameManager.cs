using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Adding button objects to make this a potential uh. phone app? Who knows.
    public Button QuitButton;
    public Button PauseButton;
    public Button ResumeButton;
    public Button NewPizzaButton;
    public Button ReleasePizzaButton;
    public Button UpMovementButton;
    public Button DownMovementButton;
    // Adding string keys so that players can have custom inputs.
    public string KeyThrow;
    public string KeyPickUp;
    public string KeyUp;
    public string KeyDown;
    public string KeyEscape;
    public string KeyQuit;
    // Other important things
    public GameObject Player;
    PlayerMovementScript movement;
    bool paused = false;
    int points = 0;
    public Image pauseBackground;

    public string nextScene = "MenuScene";

    // Start is called before the first frame update
    void Start()
    {
        KeyPickUp = PlayerPrefs.GetString("PickUp", "a");
        KeyThrow = PlayerPrefs.GetString("Throw", "d");
        KeyUp = PlayerPrefs.GetString("Up", "w");
        KeyDown = PlayerPrefs.GetString("Down", "s");
        KeyEscape = PlayerPrefs.GetString("Escape", "escape");
        KeyQuit = PlayerPrefs.GetString("Quit", "q");

        movement = Player.GetComponent<PlayerMovementScript>();
    }

    void Update()
    {
        // a to pick up
        // d to throw
        // w to move up
        // s to move down
        // esc to pause
        // r to resume
        // q to quit

        if (Input.GetKeyUp(KeyEscape))
        {
            Pause();
        }
        if (paused)
        {
            if  (Input.GetKey(KeyQuit))
            {
                onQuit();
            }
        } else
        {
            // if we are throwing or picking up, not both
            if (Input.GetKey(KeyPickUp) && !Input.GetKey(KeyThrow))
            {
                Log("Picking up a pizza.");
                movement.PickUp();
            } else if (!Input.GetKey(KeyPickUp) && Input.GetKey(KeyThrow))
            {
                Log("Throwing a pizza.");
                movement.Throw();
            }

            if (Input.GetKey(KeyUp) && !Input.GetKey(KeyDown))
            {
                Log("Moving character up");
                movement.MoveUp();
            } else if (!Input.GetKey(KeyUp) && Input.GetKey(KeyDown))
            {
                Log("Moving character down");
                movement.MoveDown();
            }
        }
    }

    public void Log(string msg)
    {
        Debug.Log("Log: " + msg);
    }
    public void onQuit()
    {
        Debug.Log("Note: Moving from MenuScene to " + nextScene);
        SceneManager.LoadScene(nextScene);
    }

    public void Pause()
    {
        // needs to pause all enemies
        // needs to pause pizza conveyer belt
        // needs to pause pizza generation
        // needs to pause point generation
        // needs to freeze player (done)
        Debug.Log("Note: Pause");
        paused = !paused;
        pauseBackground.enabled = paused;
    }
}
// need an enemy script and presets for different types of enemies? 
// need a pizza projectile
// need a pizza conveyer belt..? ugh!
