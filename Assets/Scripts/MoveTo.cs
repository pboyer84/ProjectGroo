using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

    public Vector3[] points;
    private int currPoint;
    public NavMeshAgent agent;

    public float defence;
    public float offence;

	// Use this for initialization
	void Start () {
        points = new Vector3[2];
        agent = GetComponent<NavMeshAgent>();
        points[1] = transform.position + transform.forward * defence;
        points[0]= transform.position + transform.forward * offence;
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
