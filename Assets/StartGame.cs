using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Game");
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
	}
}
