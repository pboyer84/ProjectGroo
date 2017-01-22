using UnityEngine;
using System.Collections;

public class GenerateTank : MonoBehaviour {

    public int limit;
    int spawnNumber = 0;
    public Vector3[] spawnPoints;
    Vector3 spawnPosition;
    // Use this for initialization
    void Start () {
        if (tag == "Enemy") limit = 2;
        else limit = 2;
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void MakeTank(GameObject tankPrefab)
    {
        //three points, three unity units apart, cycled through
        spawnPosition = spawnPoints[spawnNumber];
        Instantiate(tankPrefab, spawnPosition, tankPrefab.transform.rotation);
        if (spawnNumber == limit)
        {
            spawnNumber = 0;
        }
        else
        {
            spawnNumber++;
        }
    }
}
