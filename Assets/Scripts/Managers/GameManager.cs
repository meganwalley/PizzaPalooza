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
    // Elements that change during the game.
    public Image Pizza;
    public Text Score;
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
    public GameObject ShopWalls;
    HealthScript health;
    bool paused = false;
    bool hasPizza = false;
    bool CD = false;
    float points = 0;
    public GameObject pauseBackground;
    public GameObject PizzaProjectilePrefab;
    public GameObject flashlight;
    public HorizontalLayoutGroup healthContainer;

    public GameObject HealthIndicator;

    public string nextScene = "GameOverScene";
    public string menuScene = "MenuScene";

    public List<GameObject> ConveyerBeltPizzas;
    public List<GameObject> thrownPizzas;
    public List<GameObject> zombies;

    private float second = 0.25f;
    private float pointsEverySecond = 0.01f;

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
        health = ShopWalls.GetComponent<HealthScript>();

        PauseButton.onClick.AddListener(Pause);
        ResumeButton.onClick.AddListener(Pause);

        StartCoroutine(PointIncrementalTime(3f));
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
                OnQuit();
            }
        } else
        {
            if (!CD)
            {
                // if we are throwing or picking up, not both
                if (Input.GetKeyUp(KeyPickUp) && !Input.GetKeyUp(KeyThrow) && !hasPizza)
                {
                    Log("Picking up a pizza.");
                    GameObject grabbedPizza = movement.GetPizza();
                    if (grabbedPizza != null)
                    {
                        movement.PickUp(grabbedPizza);
                        DeleteGameObject(grabbedPizza);
                        hasPizza = true;
                        //Pizza.color = new Color(Pizza.color.r, Pizza.color.g, Pizza.color.b, 1);
                        CD = true;
                        StartCoroutine(Cooldown(0.5f));
                    }
                }
                else if (!Input.GetKeyUp(KeyPickUp) && Input.GetKeyUp(KeyThrow) && hasPizza)
                {
                    Log("Throwing a pizza.");
                    movement.Throw(); // doesn't delete
                    hasPizza = false; // doesn't delete
                    //Pizza.color = new Color(Pizza.color.r, Pizza.color.g, Pizza.color.b, 0);
                    CD = true;
                    StartCoroutine(Cooldown(0.5f));
                    GameObject temp = Instantiate(PizzaProjectilePrefab);
                    temp.transform.position = Player.transform.position;
                    thrownPizzas.Add(temp);
                }
            }

            if (Input.GetKey(KeyUp) && !Input.GetKey(KeyDown))
            {
                //Log("Moving character up");
                movement.MoveUp();
            } else if (!Input.GetKey(KeyUp) && Input.GetKey(KeyDown))
            {
                //Log("Moving character down");
                movement.MoveDown();
            }
        }

        int currentHealth = health.currentHealth;
        DisplayHealth(currentHealth);
        if (currentHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
        flashlight.SetActive(hasPizza);
    }

    public void Log(string msg)
    {
        Debug.Log("Log: " + msg);
    }
    public void OnQuit()
    {
        Debug.Log("Note: Moving from PlayScene to MenuScene");
        SceneManager.LoadScene(menuScene);
    }

    public void Pause()
    {
        // needs to pause all enemies
        // needs to pause pizza conveyer belt
        // needs to pause pizza generation
        // needs to pause point generation
        // needs to freeze player (done)
        paused = !paused;
        Debug.Log("Note: Pause " + paused);
        PauseButton.gameObject.SetActive(!paused);
        ResumeButton.gameObject.SetActive(paused);
        pauseBackground.SetActive(paused);
        health.pause = paused;

        foreach (GameObject o in ConveyerBeltPizzas)
        {
            if (o == null)
                Object.Destroy(o);
            else
                o.GetComponent<PizzaCollectibleScript>().pause = paused;
        }
        foreach (GameObject o in thrownPizzas)
        {
            if (o == null)
                Object.Destroy(o);
            else
                o.GetComponent<PizzaProjectileScript>().pause = paused;
        }
        foreach (GameObject o in zombies)
        {
            if (o == null)
                Object.Destroy(o);
            else
                o.GetComponent<EnemyScript>().pause = paused;
        }
    }

    public bool IsPaused()
    {
        return paused;
    }

    public bool DeleteGameObject(GameObject obj, bool getPoints = false)
    {
        if (obj.tag == "Pizza Projectile")
        {
            foreach(GameObject o in thrownPizzas)
            {
                if (o == obj)
                {
                    Object.Destroy(obj);
                    thrownPizzas.Remove(o);
                    return true;
                }
            }
            Object.Destroy(obj);
            return true;
        } else if (obj.tag == "Zombie")
        {
            foreach (GameObject o in zombies)
            {
                if (o == obj)
                {
                    if (getPoints)
                        points += o.GetComponent<EnemyScript>().GetPoints();
                    Object.Destroy(obj);
                    zombies.Remove(o);
                    return true;
                }
            }
            Object.Destroy(obj);
            return true;
        } else if (obj.tag == "Pizza Collectible")
        {
            foreach (GameObject o in ConveyerBeltPizzas)
            {
                if (o == obj)
                {
                    Destroy(obj);
                    ConveyerBeltPizzas.Remove(o);
                    return true;
                }
            }
            Destroy(obj);
            return true;
        }
        return true;
    }

    IEnumerator Cooldown(float animDelay)
    {
        yield return new WaitForSeconds(animDelay);
        CD = false;
    }
    public void AddPizzaConveyer(GameObject obj)
    {
        this.ConveyerBeltPizzas.Add(obj);
    }
    public void AddEnemy(GameObject obj)
    {
        this.zombies.Add(obj);
    }

    IEnumerator PointIncrementalTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (!paused)
            points += pointsEverySecond;
        Score.text = points.ToString("$ 0.00");
        StartCoroutine(PointIncrementalTime(second));
    }

    void DisplayHealth(int currentHealth)
    {
        foreach (Transform child in healthContainer.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < currentHealth; ++i)
        {
            GameObject temp = Instantiate(HealthIndicator);
            temp.transform.SetParent(healthContainer.transform);
        }
    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetFloat("Score", points);
        Debug.Log("Note: Moving from PlayScene to " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}

// need an enemy script and presets for different types of enemies? 
// need a pizza projectile
// need a pizza conveyer belt..? ugh!
