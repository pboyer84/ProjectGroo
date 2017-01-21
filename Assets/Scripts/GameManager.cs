using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject selectionStrip;
    public GameObject tankIconPrefab;
    public GameObject castleGO;
    public Text moneyDisplay;
    public Sprite SoldSprite;
    private Vector3 testLocation = new Vector3(-50f, 30f, 0f);
    private float spawnTimer = 0f;
    private GenerateTank generateTankScript;
    public float spawnCooldown = 1f;
    int accumulator, money;
    string spriteString;
    Dictionary<string, TankIcon> theTankIcons;
    Dictionary<string, GameObject> theTankPrefabsLookup;
    public GameObject[] theTankPrefabs;
    public GameObject[] theTankIconPrefabs;
    GameObject[] pauseObjects;
    bool paused = false;
    // Use this for initialization

    void Start ()
    {
        money = 1000;
        SoldSprite = Resources.Load<Sprite>("Sprites/SoldSprite");
        theTankIcons = new Dictionary<string, TankIcon>();
        theTankPrefabsLookup = new Dictionary<string, GameObject>();
        accumulator = 1;
        generateTankScript = castleGO.GetComponent<GenerateTank>();
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
            TankIconGO.transform.SetParent(selectionStrip.transform);
            TankIconGO.transform.localPosition = testLocation;
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
}
