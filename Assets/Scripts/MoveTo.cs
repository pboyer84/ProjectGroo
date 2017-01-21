using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Vector3[] points;
    private int currPoint;
    public NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        points = new Vector3[2];
        agent = GetComponent<NavMeshAgent>();
        points[1] = transform.position + transform.forward * 1f;
        points[0]= transform.position + transform.forward * 20f;
        GotoNextPoint();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GotoNextPoint() {
        if (!GetComponent<Tank>().retreating) currPoint = 0;
        else currPoint = 1;
        agent.destination = points[currPoint];

    }
}
