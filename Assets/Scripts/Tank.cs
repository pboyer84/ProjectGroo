using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

    public float health;
    public char type;


    public bool retreating;
    public bool attackReady;

    public bool inCombat;

    public float shootTimer;
    public float shootCooldown;

    public Vector3 defaultDirection;
    public Quaternion defaultRotation;
    public float speed;
    // Use this for initialization
    void Start () {
        if (tag == "Friendly") health = 15f;
        else health = 5f;
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
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown && inCombat && attackReady)
        {
            ShootBullet myInstance = gameObject.GetComponent<ShootBullet>();
            myInstance.Shoot();
            shootTimer = 0;
        }
	}
    void FixedUpdate() {
        detectEnemies(4f);
        if (!inCombat && GetComponent<MoveTo>().agent.remainingDistance > 1f)
        {
            GetComponent<NavMeshAgent>().Resume();
            //transform.rotation = defaultRotation;
        }
        else {
            GetComponent<NavMeshAgent>().Stop();
            attackReady = true;
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
            inCombat = true;
            transform.LookAt(objectsInRadius[0].transform);

        }
        else inCombat = false;
    }

    void OnDrawGizmos() {
        Vector3 center = transform.position;
        Ray r = new Ray(transform.position, transform.forward);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, 4);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(r);
    }
}
