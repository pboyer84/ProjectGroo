﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject selectionStrip;
    public GameObject castleGO;
    public GameObject EnemySpawn;
    public Text moneyDisplay;
    public Sprite SoldSprite;
    private Vector3 testLocation = new Vector3(-50f, 40f, 0f);
    private float spawnTimer = 0f;
    private GenerateTank generateTankScript;
    private GenerateTank generateEnemyScript;
    private GeneratePowerUp generatePowerUpScript;
    private float spawnCooldown;
    public float timeToTravelStrip = 10f;
    private float tankIconVelocity;
    private int money;
    int accumulator;
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
    public GameObject victoryText;
    public GameObject menuCommandText;

    GameObject[] pauseObjects;
    public bool paused = false;
    public bool gameEnded;

    public AudioClip buySound;

    private Text WaveCounter;
    // Use this for initialization

    void Start ()
    {
        gameEnded = false;
        tankIconVelocity = Screen.height / timeToTravelStrip;
        spawnCooldown = (timeToTravelStrip / 3.8f)/2f;
        Debug.Log("Screen height: " + Screen.height);
        won = false;
        enemyLeft = 0;
        money = 1000;
        //SoldSprite = Resources.Load<Sprite>("Sprites/SoldSprite");
        theTankIcons = new Dictionary<string, TankIcon>();
        accumulator = 1;
        generateTankScript = castleGO.GetComponent<GenerateTank>();
        generateEnemyScript = EnemySpawn.GetComponent<GenerateTank>();
        generatePowerUpScript = castleGO.GetComponent<GeneratePowerUp>();
        moneyDisplay.text = "$" + money.ToString();
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        GameObject waveCounterGO = GameObject.Find("WaveCounter");
        WaveCounter = waveCounterGO.GetComponent<Text>();
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
                if (Input.GetKeyDown("space") && !paused && !gameEnded)
                {
                    if(money >= 100)
                    {
                        SoundManager.instance.PlaySingle(buySound);
                        switch (icon.myType)
                        {
                            case IconType.TANK: generateTankScript.MakeTank(icon.MyTankPrefab); break;
                            case IconType.POWERUP: generatePowerUpScript.MakePowerUp(icon.MyTankPrefab); break;
                            default: break;
                        }
                       
                        touchedIcons.Add(icon);
                        updateMoney(-100);
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
                waveWins++;
                WaveCounter.text = waveWins.ToString();
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
                generateEnemyScript.MakeTank(theEnemyTankPrefabs[randomIdx]);
                generateEnemyScript.MakeTank(theEnemyTankPrefabs[randomIdx]);
                maxCount++;
            }
            if (maxCount >= maxSpawn)
            {
                maxCount = 0;
                waveActive = false;
                //WaveCounter.text = waveWins.ToString();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
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
        victoryText.SetActive(true);
        menuCommandText.SetActive(true);
        gameEnded = true;
    }
    public void updateMoney(int amount)
    {
        money += amount;
        moneyDisplay.text = "$" + money.ToString();
    }
}
