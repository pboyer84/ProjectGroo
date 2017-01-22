using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject selectionStrip;
    public GameObject castleGO;
    public GameObject EnemySpawn;
    public Text moneyDisplay;
    public Sprite SoldSprite;
    private Vector3 testLocation = new Vector3(-50f, 30f, 0f);
    private float spawnTimer = 0f;
    private GenerateTank generateTankScript;
    private GenerateTank generateEnemyScript;
    private float spawnCooldown;
    public float timeToTravelStrip = 5f;
    private float tankIconVelocity;
    int accumulator, money;
    Dictionary<string, TankIcon> theTankIcons;
    public GameObject[] theTankPrefabs;
    public GameObject[] theTankIconPrefabs;

    public GameObject[] theEnemyTankPrefabs;
    private float enemySpawnTimer = 0f;
    public float enemySpawnCooldown;

    private float waveTimer = 0f;
    public float waveCooldown;
    public bool waveActive;
    private int waveWins;
    public int wavesNeeded;
    public int enemyLeft;

    public int maxSpawn;
    private int maxCount = 0;

    public bool won;

    GameObject[] pauseObjects;
    bool paused = false;
    // Use this for initialization

    void Start ()
    {
        tankIconVelocity = Screen.height / timeToTravelStrip;
        spawnCooldown = timeToTravelStrip / 3.8f;
        won = false;
        enemyLeft = 0;
        money = 1000;
        //SoldSprite = Resources.Load<Sprite>("Sprites/SoldSprite");
        theTankIcons = new Dictionary<string, TankIcon>();
        accumulator = 1;
        generateTankScript = castleGO.GetComponent<GenerateTank>();
        generateEnemyScript = EnemySpawn.GetComponent<GenerateTank>();
        moneyDisplay.text = "$" + money.ToString();
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnCooldown)
        {
            spawnTimer = 0f;
            int randomIdx = Random.Range(0, theTankPrefabs.Length);
            GameObject TankIconGO = Instantiate(theTankIconPrefabs[randomIdx], Vector3.zero, Quaternion.identity) as GameObject;
            TankIcon newIcon = TankIconGO.GetComponent<TankIcon>();
            newIcon.MyTankPrefab = theTankPrefabs[randomIdx];
            newIcon.velocity = tankIconVelocity;
            TankIconGO.transform.SetParent(selectionStrip.transform);
            TankIconGO.transform.localPosition = testLocation;
            RectTransform r = TankIconGO.GetComponent<RectTransform>();
            r.localScale = new Vector3(1f, 1f, 1f);
            TankIconGO.name = "Tank" + accumulator;
            theTankIcons.Add(TankIconGO.name, newIcon);
            accumulator++;
        }
        List<TankIcon> touchedIcons = new List<TankIcon>();
        foreach(TankIcon icon in theTankIcons.Values)
        {
            if (icon.IsTouchingBar && icon.IsAvailableForPurchase)
            {
                if (Input.GetKeyDown("space") && !paused)
                {
                    if(money >= 100)
                    {
                        generateTankScript.MakeTank(icon.MyTankPrefab);
                        touchedIcons.Add(icon);
                        money -= 100;
                        moneyDisplay.text = "$" +  money.ToString();
                    }
                }
            } 
        }
        if (!waveActive && waveWins < wavesNeeded)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer > waveCooldown)
            {
                waveTimer = 0f;
                waveActive = true;
                
            }
        }
        else
        {
            enemySpawnTimer += Time.deltaTime;
            if (waveActive && enemySpawnTimer > enemySpawnCooldown && maxCount < maxSpawn && !won)
            {
                enemySpawnTimer = 0f;
                int randomIdx = Random.Range(0, theEnemyTankPrefabs.Length);
                generateEnemyScript.MakeTank(theEnemyTankPrefabs[randomIdx]);
                randomIdx = Random.Range(0, theEnemyTankPrefabs.Length);
                generateEnemyScript.MakeTank(theEnemyTankPrefabs[randomIdx]);
                randomIdx = Random.Range(0, theEnemyTankPrefabs.Length);
                generateEnemyScript.MakeTank(theEnemyTankPrefabs[randomIdx]);
                maxCount++;
            }
            if (maxCount >= maxSpawn)
            {
                maxCount = 0;
                waveActive = false;
                waveWins++;
            }
            
        }
        if (waveWins >= wavesNeeded && enemyLeft <= 0)
        {
            waveActive = false;
            won = true;
            victory();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
                paused = true;
            }
            else if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
                paused = false;
            }
            
        }
        if (touchedIcons.Count > 0)
        {
            touchedIcons[0].MyImageSprite = SoldSprite;
            touchedIcons[0].IsAvailableForPurchase = false;
        }
	}

    public void DestroyIcon(GameObject go)
    {
        theTankIcons.Remove(go.name);
        Destroy(go);
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void unpause()
    {
        Time.timeScale = 1;
        hidePaused();
        paused = false;
    }
    public void victory()
    {

    }
}
