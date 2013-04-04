using UnityEngine;
using System.Collections;

public class Final_Camera : MonoBehaviour {
	Vector3 endPosition = Vector3.zero;
	Vector3 startPosition = Vector3.zero;
	public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
	private bool start = false;

	// Use this for initialization
	void Start () {
		endPosition.x = 20.9f;
		endPosition.y = 5.2f;
		endPosition.z = -12.9f;
	}
	
	public void unparent(){
		this.transform.parent = null;
		start = true;
		startPosition = transform.position;
		startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, endPosition);
	}
	
	
	// Update is called once per frame
	void Update () {
		if(start){
			float distCovered = (Time.time - startTime) * speed;
        	float fracJourney = distCovered / journeyLength;
        	transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
		}
	}
}
