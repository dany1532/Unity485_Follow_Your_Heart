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
	private CharacterMovement theHero = null;
	public bool isDestructible = false;
	
	// Use this for initialization
	void Start () {
		getDesiredPosition();
		
	}
	
	void getDesiredPosition(){
		Vector3 loc = this.transform.position;
		if(platState == TraversalState.up)
			desiredPosition = loc.y + traversalDistance;
		
		
		if(platState == TraversalState.right)
			desiredPosition = loc.x + traversalDistance;
		
		if(platState == TraversalState.left)
			desiredPosition = loc.x - traversalDistance;
		
		if(platState == TraversalState.down)
			desiredPosition = loc.y - traversalDistance;
	}
	
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Player"){
			if(isDestructible){
				platState = TraversalState.right;
				getDesiredPosition();
				Invoke("fall",1f);
			}
			
			else if(this.gameObject.name == "Moving_Platform_Sides" ||
					 this.gameObject.name == "Moving_Platform_Upward"){
				theHero = col.transform.parent.gameObject.GetComponent<CharacterMovement>();	
			}
		}
	}
	
	void OnTriggerExit(Collider col){
		if(!isDestructible && this.gameObject.name == "Moving_Platform_Sides" ||
				 this.gameObject.name == "Moving_Platform_Upward"){

			theHero.NewPlatformSpeedX(0f, false);
			//theHero.NewPlatformSpeedY(0f, false);
			theHero = null;
		}
	}
	
	void fall(){
		Vector3 loc = this.transform.position;
		platState = TraversalState.down;
		desiredPosition = loc.y - (traversalDistance + 1140);
		movementSpeed = 30;
		Destroy(this.gameObject,2f);
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
		
		if(theHero != null){
			if(platState == TraversalState.right)
				theHero.NewPlatformSpeedX(movementSpeed, true);
				
			else if(platState == TraversalState.up)
				theHero.NewPlatformSpeedY(movementSpeed, true);
			
			else if(platState == TraversalState.left)
				theHero.NewPlatformSpeedX(movementSpeed, false);
			
			else if(platState == TraversalState.down){
				theHero.NewPlatformSpeedY(movementSpeed, false);
			}
			
			else if(platState == TraversalState.stop){
				theHero.NewPlatformSpeedX(0f, false);
				theHero.NewPlatformSpeedY(0f, false);
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
