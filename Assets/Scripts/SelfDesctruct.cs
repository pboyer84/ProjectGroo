using UnityEngine;

public class SelfDesctruct : MonoBehaviour {

    public float SecondsToLive = 4;
    private float timer;
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > SecondsToLive)
        {
            Destroy(gameObject);
        }
	}
}
