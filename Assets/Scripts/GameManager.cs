using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public GameObject selectionStrip;
    public GameObject tankIconPrefab;
    public GameObject castleGO;
    public Sprite SoldSprite;
    private Vector3 testLocation = new Vector3(-50f, 100f, 0f);
    private float spawnTimer = 0f;
    private GenerateTank generateTankScript;
    public float spawnCooldown = 1f;
    int random, accumulator;
    string spriteString;
    Dictionary<string, TankIcon> theTankIcons;
    // Use this for initialization

    void Start ()
    {
        SoldSprite = Resources.Load<Sprite>("Sprites/SoldSprite");
        theTankIcons = new Dictionary<string, TankIcon>();
        accumulator = 1;
        generateTankScript = castleGO.GetComponent<GenerateTank>();
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
            //Debug.Log("Hello");
            //create a new tile in the strip
        }
        List<TankIcon> touchedIcons = new List<TankIcon>();
        foreach(TankIcon icon in theTankIcons.Values)
        {
            if (icon.IsTouchingBar)
            {
                if (Input.GetKeyDown("space"))
                {
                    generateTankScript.MakeTank();
                    //icon.MyImageSprite = SoldSprite;
                    touchedIcons.Add(icon);
                }
            } 
        }
        if (touchedIcons.Count > 0)
        {
            touchedIcons[0].MyImageSprite = SoldSprite;
        }
	}

    public void DestroyIcon(GameObject go)
    {
        theTankIcons.Remove(go.name);
        Destroy(go);
    }
}
