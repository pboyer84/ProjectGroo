using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    public float health = 2.0f;
    public float shootTimer;
    public float shootCooldown;
	// Use this for initialization
	void Start () {
        shootTimer = 0;
        shootCooldown = 2;
	}
	
	// Update is called once per frame
	void Update () {
        shootTimer += Time.deltaTime;
        if (shootTimer > shootCooldown) {
            ShootBullet myInstance = gameObject.GetComponent<ShootBullet>();
            myInstance.Shoot();
            shootTimer = 0;
        }
        
	}
}
