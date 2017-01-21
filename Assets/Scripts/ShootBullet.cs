﻿using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

    public GameObject bulletPrefab;
    public float speed;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void Shoot() {
        bulletPrefab.tag = gameObject.tag;
        if (bulletPrefab.tag == "Enemy")
        {
            bulletPrefab.GetComponent<Move>().multiplier = speed;
        }
        else
        {
            bulletPrefab.GetComponent<Move>().multiplier = -speed;
        }
        bulletPrefab.GetComponent<Move>().movement = gameObject.transform.forward;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}