using UnityEngine;
using System.Collections;

public class Clickontank : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    void OnMouseDown()
    {
        GetComponent<Tank>().retreating = !GetComponent<Tank>().retreating;
        GetComponent<MoveTo>().GotoNextPoint();
    }
}
