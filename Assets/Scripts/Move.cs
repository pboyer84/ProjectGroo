using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    //public Vector3 startingPos = new Vector3(11f, 0.5f);
    public float multiplier = 1f;
    public Vector3 movement = new Vector3();
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }
    void FixedUpdate() {
        gameObject.transform.position += movement * multiplier * Time.deltaTime;
    }

}
