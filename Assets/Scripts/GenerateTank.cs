using UnityEngine;
using System.Collections;

public class GenerateTank : MonoBehaviour {

    int randomTank;
    int spawnNumber = 0;
    public Vector3[] spawnPoints;
    Vector3 spawnPosition;
    Quaternion StartingRotation = Quaternion.Euler(0f, 180f, 0f);
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
        GameObject go = Instantiate(tankPrefab, spawnPosition, Quaternion.identity) as GameObject;
        go.transform.rotation = StartingRotation;
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
