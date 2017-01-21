using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankIcon : MonoBehaviour
{

    public Sprite theSprite;
    private Image theImage;
    public float velocity = 1f;
    public GameObject theSelector;
    Image theSelector2;
    public bool IsTouchingBar;
    private RectTransform myRect;
    private GameManager GMScript;
    // Use this for initialization

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        GameObject GameManagerGO = GameObject.FindGameObjectWithTag("GameController");
        GMScript = GameManagerGO.GetComponent<GameManager>();
    }

    void Start ()
    {
        theImage = GetComponent<Image>();
        theImage.sprite = theSprite;
        theSelector = GameObject.FindGameObjectWithTag("Selector");
    }

	void Update()
    {
        IsTouchingBar = transform.position.y - theSelector.transform.position.y < myRect.rect.width / 2;
        
        if (transform.position.y < -350f)
        {
           GMScript.DestroyIcon(transform.gameObject);
        }

    }
	
    // Update is called once per frame
	void FixedUpdate()
    {
        transform.position -= new Vector3(0f, velocity * Time.deltaTime, 0f);
	}
}
