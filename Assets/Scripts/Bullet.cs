using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float existTimer;
    public float existThreshold;
	// Use this for initialization
	void Start () {
        existTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        existTimer += Time.deltaTime;
        if (existTimer > existThreshold)
        {
            Destroy(gameObject);
        }
	}
    void FixedUpdate() {
        
    }
}
