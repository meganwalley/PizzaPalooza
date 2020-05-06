using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    PlayerData data;
    GameManager manager;

    public Button HealthButton;
    
    public GameObject holderPizzaPepperoni;
    public Button buyPizzaPepperoni;
    public GameObject holderPizzaBBQ;
    public Button buyPizzaBBQ;
    public GameObject holderPizzaHawaiian;
    public Button buyPizzaHawaiian;
    public GameObject holderPizzaSupreme;
    public Button buyPizzaSupreme;

    public GameObject holderZombieCautionTape;
    public Button buyZombieCautionTape;
    public GameObject holderZombieClosedSign;
    public Button buyZombieClosedSign;

    public GameObject holderBeltOil;
    public Button buyBeltOil;
    public GameObject holderBeltGears;
    public Button buyBeltGears;
    public GameObject holderBeltFire;
    public Button buyBeltFire;
    public GameObject holderBeltReplace;
    public Button buyBeltReplace;

    public GameObject holderHealthMop;
    public Button buyHealthMop;
    public GameObject holderHealthSoap;
    public Button buyHealthSoap;
    public GameObject holderHealthGloves;
    public Button buyHealthGloves;
    public GameObject holderHealthMask;
    public Button buyHealthMask;

    public SpriteRenderer imageZombieClosedSign;
    public SpriteRenderer imageZombieCautionTape;


    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.FindObjectOfType<PlayerData>();
        manager = GameObject.FindObjectOfType<GameManager>();
        HealthButton.onClick.AddListener(OnBuyHealth);
        
        buyPizzaPepperoni.onClick.AddListener(OnBuyPizzaPepperoni);
        buyPizzaBBQ.onClick.AddListener(OnBuyPizzaBBQ);
        buyPizzaHawaiian.onClick.AddListener(OnBuyPizzaHawaiian);
        buyPizzaSupreme.onClick.AddListener(OnBuyPizzaSupreme);

        buyZombieCautionTape.onClick.AddListener(OnBuyZombieCautionTape);
        buyZombieClosedSign.onClick.AddListener(OnBuyZombieClosedSign);

        buyHealthGloves.onClick.AddListener(OnBuyHealthGloves);
        buyHealthMask.onClick.AddListener(OnBuyHealthMask);
        buyHealthMop.onClick.AddListener(OnBuyHealthMop);
        buyHealthSoap.onClick.AddListener(OnBuyHealthSoap);

        buyBeltOil.onClick.AddListener(OnBuyBeltOil);
        buyBeltGears.onClick.AddListener(OnBuyBeltGears);
        buyBeltFire.onClick.AddListener(OnBuyBeltFire);
        buyBeltReplace.onClick.AddListener(OnBuyBeltReplace);

        RefreshStore();
    }

    public void OnBuyHealth() {
        float cost = data.costHealth;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            manager.AddHealth(1);
        }
    }

    public void OnBuyPizzaPepperoni() {

        float cost = data.costPizzaPepperoni;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockPizzaPepperoni = true;
            RefreshStore();
        }
    }

    public void OnBuyPizzaBBQ()
    {
        float cost = data.costPizzaBBQ;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockPizzaBBQ = true;
            RefreshStore();
        }
    }
    
    public void OnBuyPizzaHawaiian()
    {
        float cost = data.costPizzaHawaiian;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockPizzaHawaiian = true;
            RefreshStore();
        }
    }

    public void OnBuyPizzaSupreme()
    {
        float cost = data.costPizzaSupreme;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockPizzaSupreme = true;
            RefreshStore();
        }
    }

    public void OnBuyZombieCautionTape()
    {
        float cost = data.costZombieCautionTape;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockZombieCautionTape = true;
            RefreshStore();
        }
    }
    
    public void OnBuyZombieClosedSign()
    {
        float cost = data.costZombieClosedSign;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockZombieClosedSign = true;
            RefreshStore();
        }
    }

    public void OnBuyHealthMop()
    {
        float cost = data.costHealthMop;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockHealthMop = true;
            RefreshStore();
        }
    }

    public void OnBuyHealthSoap()
    {
        float cost = data.costHealthSoap;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockHealthSoap = true;
            RefreshStore();
        }
    }
    
    public void OnBuyHealthGloves()
    {
        float cost = data.costHealthGloves;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockHealthGloves = true;
            RefreshStore();
        }
    }
    
    public void OnBuyHealthMask()
    {
        float cost = data.costHealthMask;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockHealthMask = true;
            RefreshStore();
        }
    }

    public void OnBuyBeltOil ()
    {
        float cost = data.costBeltOil;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockBeltOil = true;
            RefreshStore();
        }
    }
    
    public void OnBuyBeltGears()
    {
        float cost = data.costBeltGears;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockBeltGears = true;
            RefreshStore();
        }
    }
    
    public void OnBuyBeltFire()
    {
        float cost = data.costBeltFire;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockBeltFire = true;
            RefreshStore();
        }
    }
    
    public void OnBuyBeltReplace()
    {
        float cost = data.costBeltReplace;
        if (manager.points >= cost)
        {
            manager.points -= cost;
            data.unlockBeltReplace = true;
            RefreshStore();
        }
    }
    
    public void RefreshStore()
    {
        // written in code, but no special abilities yet.
        holderPizzaPepperoni.SetActive(!data.unlockPizzaPepperoni); // implemented
        holderPizzaBBQ.SetActive(!data.unlockPizzaBBQ); // implemented
        holderPizzaHawaiian.SetActive(!data.unlockPizzaHawaiian); // implemented
        holderPizzaSupreme.SetActive(!data.unlockPizzaSupreme);

        // not written in code.
        holderBeltOil.SetActive(!data.unlockBeltOil);
        holderBeltGears.SetActive(!data.unlockBeltGears && data.unlockBeltOil);
        holderBeltFire.SetActive(!data.unlockBeltFire && data.unlockBeltReplace);
        holderBeltReplace.SetActive(!data.unlockBeltReplace && data.unlockBeltGears);

        // soap = mop
        // soap < mask < gloves
        // all are implemented - increases the time until damaged
        holderHealthMop.SetActive(!data.unlockHealthMop); // 0.2f
        holderHealthSoap.SetActive(!data.unlockHealthSoap); // 0.1f
        holderHealthGloves.SetActive(!data.unlockHealthGloves && data.unlockHealthMask); // 0.4f
        holderHealthMask.SetActive(!data.unlockHealthMask && data.unlockHealthSoap); // 0.3f

        // only one zombie thing will show at a time.        
        // slows zombies by .1x - implemented
        holderZombieCautionTape.SetActive(!data.unlockZombieCautionTape && data.unlockZombieClosedSign);
        // slows waves by .2f - implemented
        holderZombieClosedSign.SetActive(!data.unlockZombieClosedSign);

        imageZombieCautionTape.enabled = data.unlockZombieCautionTape;
        imageZombieClosedSign.enabled = data.unlockZombieClosedSign;
    }
}
