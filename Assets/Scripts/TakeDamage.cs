using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TakeDamage : MonoBehaviour {

    Dictionary<char, char> strength = new Dictionary<char, char>();
    public float dam;

	// Use this for initialization
	void Start () {
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
            gameObject.GetComponent<Tank>().health -= dam;
            Destroy(bullet.gameObject);
            if (gameObject.GetComponent<Tank>().health <= 0)
            {
                Destroy(gameObject);
            }
        }
        Debug.Log("Collision Passed");
    }
}
