using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	public float traversalDistance = 40f;
	private float desiredPosition;
	public float movementSpeed = 10;
	public float stopTime = 1f;
	public enum TraversalState{up, down, right, left, stop};
	public TraversalState platState = TraversalState.up;
	public TraversalState pastState = TraversalState.stop;
	
	// Use this for initialization
	void Start () {
		Vector3 loc = this.transform.position;
		if(platState == TraversalState.up)
			desiredPosition = loc.y + traversalDistance;
		
		
		if(platState == TraversalState.right)
			desiredPosition = loc.x + traversalDistance;	
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 loc = this.transform.position;
		
		if(platState == TraversalState.up){
			if(loc.y < desiredPosition)
				transform.Translate(Vector3.up * Time.deltaTime * movementSpeed);
			else{
				platState = TraversalState.stop;
				pastState = TraversalState.up;
				Invoke("continueMoving", stopTime);
			}
		}
		
		if(platState == TraversalState.down){
			if(loc.y > desiredPosition)
				transform.Translate(Vector3.down * Time.deltaTime * movementSpeed);
			else{
				platState = TraversalState.stop;
				pastState = TraversalState.down;
				Invoke("continueMoving", stopTime);;
			}
		}
		
		if(platState == TraversalState.right){
			if(loc.x < desiredPosition)
				transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
			else{
				platState = TraversalState.stop;
				pastState = TraversalState.right;
				Invoke("continueMoving", stopTime);
			}
		}
		
		if(platState == TraversalState.left){
			if(loc.x > desiredPosition)
				transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
			else{
				platState = TraversalState.stop;
				pastState = TraversalState.left;
				Invoke("continueMoving", stopTime);
			}
		}
	}
	
	public void continueMoving(){
		Vector3 loc = this.transform.position;
		
		if(pastState == TraversalState.up){
			platState = TraversalState.down;
			desiredPosition = loc.y - traversalDistance;
		}
			
		else if(pastState == TraversalState.right){
			platState = TraversalState.left;
			desiredPosition = loc.x - traversalDistance;
		}
			
		else if(pastState == TraversalState.left){
			platState = TraversalState.right;
			desiredPosition = loc.x + traversalDistance;
		}
				
		else if(pastState == TraversalState.down){
			platState = TraversalState.up;
			desiredPosition = loc.y + traversalDistance;
		}
	}
}
