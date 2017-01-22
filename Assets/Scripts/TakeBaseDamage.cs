using UnityEngine;
using System.Collections;

public class TakeBaseDamage : MonoBehaviour {

    private Health baseHealth;
    public GameObject ruinBase;
    public GameObject defeatText;
    public GameObject menuCommandText;
    private float dam;

    public AudioClip explosionBase;
	// Use this for initialization
	void Start () {
        baseHealth = GetComponent<Health>();
        dam = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	    if (baseHealth.value <= 0)
        {
            runDefeated();
        }
	}
    void OnTriggerEnter(Collider bullet)
    {
        if (bullet.tag != tag)
        {
            Destroy(bullet.gameObject);
            baseHealth.value -= dam;
        }
    }
    void runDefeated()
    {
        gameObject.SetActive(false);
        ruinBase.SetActive(true);
        menuCommandText.SetActive(true);
        defeatText.SetActive(true);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().gameEnded= true;
    }
}
