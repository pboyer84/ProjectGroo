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
    private Vector3 testLocation = new Vector3(-50f, 100f, 0f);
    private float spawnTimer = 0f;
    private GenerateTank generateTankScript;
    public float spawnCooldown = 1f;
    int random, accumulator, money;
    string spriteString;
    Dictionary<string, TankIcon> theTankIcons;
    // Use this for initialization

    void Start ()
    {
        money = 1000;
        SoldSprite = Resources.Load<Sprite>("Sprites/SoldSprite");
        theTankIcons = new Dictionary<string, TankIcon>();
        accumulator = 1;
        generateTankScript = castleGO.GetComponent<GenerateTank>();
        moneyDisplay.text = money.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnCooldown)
        {
            spawnTimer = 0f;
            GameObject TankGO = Instantiate(tankIconPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            TankIcon newIcon = TankGO.GetComponent<TankIcon>();
            random = Random.Range(1, 4);
            spriteString = "Sprites/Tank" + random.ToString() + "Sprite";
            newIcon.StartingSprite = Resources.Load<Sprite>(spriteString);
            TankGO.transform.SetParent(selectionStrip.transform);
            TankGO.transform.localPosition = testLocation;
            TankGO.name = "Tank" + accumulator;
            theTankIcons.Add(TankGO.name, newIcon);
            accumulator++;
        }
        List<TankIcon> touchedIcons = new List<TankIcon>();
        foreach(TankIcon icon in theTankIcons.Values)
        {
            if (icon.IsTouchingBar && icon.IsAvailableForPurchase)
            {
                if (Input.GetKeyDown("space"))
                {
                    if(money >= 100)
                    {
                        generateTankScript.MakeTank();
                        touchedIcons.Add(icon);
                        money -= 100;
                        moneyDisplay.text = money.ToString();
                    }
                }
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
}
