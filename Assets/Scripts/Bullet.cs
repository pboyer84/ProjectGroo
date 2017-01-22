using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float existTimer;
    public float existThreshold;
    public char type;

    public Material redBulletMaterial;
    public Material greenBulletMaterial;
    public Material blueBulletMaterial;
    public Material greyBulletMaterial;
	// Use this for initialization
	void Start () {
        existTimer = 0;
        Renderer myRenderer = GetComponent<Renderer>();
        switch (type)
        {
            case 'r': myRenderer.material = redBulletMaterial; break;
            case 'b': myRenderer.material = blueBulletMaterial; break;
            case 'g': myRenderer.material = greenBulletMaterial; break;
            case 'n': myRenderer.material = greyBulletMaterial; break;
            default: myRenderer.material = blueBulletMaterial; break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        existTimer += Time.deltaTime;
        if (existTimer > existThreshold)
        {
            Destroy(gameObject);
        }
	}
    void FixedUpdate() {
        
    }
}
