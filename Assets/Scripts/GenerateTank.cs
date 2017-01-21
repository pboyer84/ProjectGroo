using UnityEngine;
using System.Collections;

public class GenerateTank : MonoBehaviour {

    int spawnNumber = 0;
    public Vector3[] spawnPoints;
    Vector3 spawnPosition;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       
	}

    public void MakeTank(GameObject tankPrefab)
    {
        //three points, three unity units apart, cycled through
        spawnPosition = spawnPoints[spawnNumber];
        Instantiate(tankPrefab, spawnPosition, tankPrefab.transform.rotation);
        if (spawnNumber == 2)
        {
            spawnNumber = 0;
        }
        else
        {
            spawnNumber++;
        }
    }
}
