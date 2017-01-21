using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider bullet) {
        if (bullet.gameObject.tag != gameObject.tag) {
            gameObject.GetComponent<Tank>().health -= 1f;
            Destroy(bullet.gameObject);
            if (gameObject.GetComponent<Tank>().health == 0)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log("Collision Passed");
        
    }
}
