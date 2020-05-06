using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    PlayerData data;
    ClockScript clock;
    // Adding button objects to make this a potential uh. phone app? Who knows.
    public Button QuitButton;
    public Button PauseButton;
    public Button ResumeButton;
    // Elements that change during the game.
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

    public GameObject PauseObject;
    public GameObject CheesePizzaProjectilePrefab;
    public GameObject PepperoniPizzaProjectilePrefab;
    public GameObject BBQPizzaProjectilePrefab;
    public GameObject SupremePizzaProjectilePrefab;
    public GameObject HawaiianPizzaProjectilePrefab;
    public GameObject flashlight;
    public HorizontalLayoutGroup healthContainer;

    public GameObject HealthIndicator;

    public List<GameObject> ConveyerBeltPizzas;
    public List<GameObject> thrownPizzas;
    public List<GameObject> zombies;

    public float points = 0;
    private float second = 0.25f;
    private float pointsEverySecond = 0.01f;

    // difficulty per level
    int difficulty = 1;
    int maxWaves = 10;
    public int currentWave = 0;
    bool zen = false;
    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        clock = GameObject.FindObjectOfType<ClockScript>();
        clock.difficulty = difficulty;
        clock.level = 1 + ((data.maxWaves - data.baseWaves) / data.nextLevelWavesAdd);
        // load in level settings.
        // data.score = 0f;
        points = data.score;
        Score.text = points.ToString("$ 0.00");
        difficulty = data.difficulty;
        maxWaves = data.maxWaves;
        zen = data.zenMode;

        // player settings
        KeyPickUp = PlayerPrefs.GetString("PickUp", "a");
        KeyThrow = PlayerPrefs.GetString("Throw", "d");
        KeyUp = PlayerPrefs.GetString("Up", "w");
        KeyDown = PlayerPrefs.GetString("Down", "s");
        KeyEscape = PlayerPrefs.GetString("Escape", "escape");
        KeyQuit = PlayerPrefs.GetString("Quit", "q");
        // important components
        movement = Player.GetComponent<PlayerMovementScript>();
        health = ShopWalls.GetComponent<HealthScript>();
        // important listeners
        PauseButton.onClick.AddListener(Pause);
        ResumeButton.onClick.AddListener(Pause);
        // let's get this party started!
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
        clock.wave = this.currentWave;
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
        } else {
            if (!CD)
            {
                // if we are throwing or picking up, not both
                if (Input.GetKeyUp(KeyPickUp) /**&& !Input.GetKeyUp(KeyThrow)**/ && !hasPizza)
                {
                    Log("Picking up a pizza.");
                    GameObject grabbedPizza = movement.GetPizza();
                    if (grabbedPizza != null)
                    {
                        movement.PickUp(grabbedPizza);
                        DeleteGameObject(grabbedPizza);
                        data.pizzaHeld = grabbedPizza.name.Replace("PizzaCollectible", "").Replace("(Clone)", "");
                        hasPizza = true;
                        //Pizza.color = new Color(Pizza.color.r, Pizza.color.g, Pizza.color.b, 1);
                        CD = true;
                        StartCoroutine(Cooldown(0.2f));
                    }
                }
                else if (/**!Input.GetKeyUp(KeyPickUp) &&**/ Input.GetKeyUp(KeyThrow) && hasPizza)
                {
                    Log("Throwing a pizza.");
                    movement.Throw(); // doesn't delete
                    hasPizza = false; // doesn't delete
                    //Pizza.color = new Color(Pizza.color.r, Pizza.color.g, Pizza.color.b, 0);
                    CD = true;
                    StartCoroutine(Cooldown(0.2f));
                    GameObject temp = Instantiate(getPizzaProjectile());
                    temp.transform.position = Player.transform.position;
                    thrownPizzas.Add(temp);
                }
            }
        }

        if (Input.GetKeyUp("\\"))
        {
            points += 100;
        }

        int currentHealth = health.currentHealth;
        DisplayHealth(currentHealth);
        if (currentHealth <= 0)
        {
            // lost
            data.winStatus = false;
            StartCoroutine(GameOver());
        }
        if (zombies.Count == 0 && currentWave >= maxWaves)
        {
            // won
            data.health = health.currentHealth;
            data.winStatus = true;
            StartCoroutine(GameOver());
        }
        flashlight.SetActive(hasPizza);
    }

    private GameObject getPizzaProjectile()
    {
        switch(data.pizzaHeld)
        {
            case "Pepperoni":
                return PepperoniPizzaProjectilePrefab;
            case "BBQ":
                return BBQPizzaProjectilePrefab;
            case "Supreme":
                return SupremePizzaProjectilePrefab;
            case "Hawaiian":
                return HawaiianPizzaProjectilePrefab;
            case "Cheese":
            default:
                return CheesePizzaProjectilePrefab;
        }
    }

    void FixedUpdate()
    {
        if (paused)
            return;

            //(Input.GetKey(KeyUp)|| (Input.GetKey(KeyUp))
        if ((Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.RightArrow)))
        && !(Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.LeftArrow))))
        {
            //Log("Moving character up");
            movement.MoveUp();
        }
        else if (!(Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.RightArrow)))
          && (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.LeftArrow))))
        {
            //Log("Moving character down");
            movement.MoveDown();
        }
    }
    public void Log(string msg)
    {
        Debug.Log("Log: " + msg);
    }
    public void OnQuit()
    {
        Debug.Log("Note: Moving from PlayScene to MenuScene");
        SceneManager.LoadScene(data.menuScene);
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
        PauseObject.SetActive(paused);
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
                    ConveyerBeltPizzas.Remove(o);
                    Destroy(obj);
                    Object.Destroy(obj);
                    return true;
                }
            }
            Destroy(obj);
            Object.Destroy(obj);
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

    public void AddHealth(int i)
    {
        health.currentHealth += i;
        DisplayHealth(health.currentHealth);
    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);
        data.score = points;
        data.lastScene = SceneManager.GetActiveScene().name;
//        PlayerPrefs.SetFloat("Score", points);
        Debug.Log("Note: Moving from PlayScene to " + data.gameoverScene);
        SceneManager.LoadScene(data.gameoverScene);
    }
}

// need an enemy script and presets for different types of enemies? 
// need a pizza projectile
// need a pizza conveyer belt..? ugh!
