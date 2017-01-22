using UnityEngine;
using System.Collections.Generic;

public class RepairAllTanks : MonoBehaviour {

    List<Health> allFriendlyTanksHealth;
	// Use this for initialization
	void Start () {
        allFriendlyTanksHealth = new List<Health>();
        GameObject[] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        foreach (GameObject go in friendlies)
        {
            if (go.name.Contains("Tank"))
            {
                allFriendlyTanksHealth.Add(go.GetComponent<Health>());
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	    foreach (Health h in allFriendlyTanksHealth)
        {
            h.value = h.Max;
        }
        Destroy(gameObject);
	}
}
