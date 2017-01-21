using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankIcon : MonoBehaviour {

    public Sprite theSprite;
    private Image theImage;
    public float velocity = 1f;
    
    // Use this for initialization
	void Start () {
        theImage = GetComponent<Image>();
        theImage.sprite = theSprite;    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position -= new Vector3(0f, velocity * Time.deltaTime, 0f);
	}
}
