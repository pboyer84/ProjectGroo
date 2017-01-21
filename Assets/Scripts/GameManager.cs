using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject selectionStrip;
    public GameObject theTankIcon;

    private float spawnTimer = 0f;
    public float spawnCooldown = 2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnCooldown)
        {
            spawnTimer = 0f;
            GameObject TankGO = Instantiate(theTankIcon, Vector3.zero, Quaternion.identity) as GameObject;
            TankIcon newIcon = TankGO.GetComponent<TankIcon>();
            newIcon.theSprite = Resources.Load<Sprite>("Sprites/Tank3Sprite");
            Debug.Log("Hello");
            //create a new tile in the strip
        }
	}
}
