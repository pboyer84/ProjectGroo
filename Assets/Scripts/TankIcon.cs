using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankIcon : MonoBehaviour
{

    public Sprite StartingSprite;
    public Sprite MyImageSprite
    {
        get { return theImage.sprite; }
        set { theImage.sprite = value; }
    }
    private Image theImage;
    public float velocity = 1f;
    private GameObject theSelector;
    public bool IsTouchingBar;
    private RectTransform myRect;
    private GameManager GMScript;
    private float myCanvasScaleFactor;
    public bool IsAvailableForPurchase = true;
    public GameObject MyTankPrefab;
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
        theImage.sprite = StartingSprite;
        myCanvasScaleFactor = theImage.canvas.scaleFactor;
        theSelector = GameObject.FindGameObjectWithTag("Selector");
    }

	void Update()
    {
        IsTouchingBar = Mathf.Abs((transform.position.y - theSelector.transform.position.y)/myCanvasScaleFactor) < myRect.rect.width / 2;
        
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
