using UnityEngine;
using UnityEngine.UI;

public class BillboardingHealthBar : MonoBehaviour {

    private Image healthBarImage;
    private float fullHealthBarLength;
    private Health myHealth;

	void Start ()
    {
        myHealth = GetComponentInParent<Health>();
        fullHealthBarLength = myHealth.value;
        healthBarImage = GetComponentInChildren<Image>();
        healthBarImage.gameObject.transform.rotation = Camera.main.transform.rotation;
    }
	
	void Update ()
    {
        healthBarImage.fillAmount = myHealth.value / fullHealthBarLength;
    }

    void LateUpdate()
    {
        healthBarImage.gameObject.transform.rotation = Camera.main.transform.rotation;
    }
}
