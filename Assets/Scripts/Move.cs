using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    //public Vector3 startingPos = new Vector3(11f, 0.5f);
    public float multiplier = 1f;
    
	// Use this for initialization
	void Start () {
        //gameObject.transform.position = startingPos;
	}
	
	// Update is called once per frame
	void Update () {

    }
    void FixedUpdate() {
        float velocity = 1f;
        gameObject.transform.position += new Vector3(velocity, 0f) * multiplier * Time.deltaTime;
    }

}
