﻿using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {

    public GameObject bulletPrefab;

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
            bulletPrefab.GetComponent<Move>().multiplier = 1f;
        }
        else
        {
            bulletPrefab.GetComponent<Move>().multiplier = -1f;
        }
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
