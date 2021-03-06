﻿using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    private Health myHealth;
    public char type;


    public bool retreating;
    public bool attackReady;

    public bool inCombat;

    public float shootTimer;
    public float shootCooldown;

    public Vector3 defaultDirection;
    public Quaternion defaultRotation;
    public float speed;

    public float radius;

    public GameObject manager;

    public AudioClip laser;


    // Use this for initialization
    void Start () {
        myHealth = GetComponent<Health>();
        manager = GameObject.FindGameObjectWithTag("GameController");
        if (tag == "Enemy") manager.GetComponent<GameManager>().enemyLeft += 1;
        shootTimer = 0;
        retreating = false;
        attackReady = true;
        inCombat = false;

        defaultDirection = transform.forward;
        defaultRotation = transform.rotation;

        speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        detectEnemies(radius);
        if (!inCombat && GetComponent<MoveTo>().agent.remainingDistance > 1f)
        {
            GetComponent<NavMeshAgent>().Resume();
        }
        else {
            GetComponent<NavMeshAgent>().Stop();
            attackReady = true;
        }
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown && inCombat && attackReady)
        {
            ShootBullet myInstance = gameObject.GetComponent<ShootBullet>();
            SoundManager.instance.PlaySingle(laser);
            myInstance.Shoot();
            shootTimer = 0;
        }
	}

    public void detectEnemies(float r) {
        int layer = 0;
        if (gameObject.tag == "Enemy" && attackReady)
        {
            layer = 1 << 8;
        }
        else if (gameObject.tag == "Friendly" && attackReady)
        {
            layer = 1 << 9;
        }
        Vector3 center = transform.position;
        Collider[] objectsInRadius = Physics.OverlapSphere(center, r, layer);
        if (objectsInRadius.Length > 0) {
            if (objectsInRadius[0].name != "Bullet(Clone)")
            {
                inCombat = true;
                transform.LookAt(objectsInRadius[0].transform);
                
            }
            

        }
        else inCombat = false;
    }

    void OnDrawGizmos() {
        Vector3 center = transform.position;
        Ray r = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(r);
    }
}
