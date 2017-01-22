using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TakeDamage : MonoBehaviour {

    Dictionary<char, char> strength = new Dictionary<char, char>();
    public float dam;
    public GameObject manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("GameController");
        strength.Add('r', 'g');
        strength.Add('g', 'b');
        strength.Add('b', 'r');
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider bullet) {
        dam = 2f;
        char btype = bullet.gameObject.GetComponent<Bullet>().type;
        char ttype = GetComponent<Tank>().type;
        if (bullet.gameObject.tag != gameObject.tag) {
            if (ttype == 'n' || btype == 'n') dam = 2f;
            else if (strength[btype] == ttype) dam += 1f;
            else if (strength[ttype] == btype) dam -= 1f;
            gameObject.GetComponent<Health>().value -= dam;
            Destroy(bullet.gameObject);
            if (gameObject.GetComponent<Health>().value <= 0)
            {
                if (tag == "Enemy") manager.GetComponent<GameManager>().enemyLeft -= 1;
                Destroy(gameObject);
            }
        }
        Debug.Log("Collision Passed");
    }
}
