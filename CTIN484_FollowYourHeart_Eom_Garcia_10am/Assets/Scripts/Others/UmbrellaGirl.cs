using UnityEngine;
using System.Collections;

public class UmbrellaGirl : MonoBehaviour {
	public bool startAnim = false;
	public float walkSpeed = 6;
	public float jumpSpeed = 15;
	public float floatingSpeed = 1;
	public float traversalDistance = 15f;
	public Vector3 girlCheckpoint;
	private float desiredPosition;
	public enum Direction {left, right, none};
	public Direction myDir = Direction.none;
	public Direction lastDir = Direction.none;
	public Direction nextDir = Direction.none;
	public bool useFloat;
	public bool useJump;
	public bool isGrounded;
	
	// Use this for initialization
	void Start () {
		Vector3 loc = this.transform.position;
		desiredPosition = loc.x + traversalDistance;
	}
	
	
	// Update is called once per frame
	void Update () {
		if(startAnim){
			float locX = this.transform.position.x;
			if(locX < desiredPosition){
				transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );	
			}
			else
				startAnim = false;
		}
		
		if(useJump){
			rigidbody.velocity = Vector3.up * jumpSpeed;
			useJump = false;
		}
		
		if(useFloat){
			rigidbody.useGravity = false;
			rigidbody.velocity = new Vector3(0,-floatingSpeed,0);
		}
		
		if(myDir == Direction.left){
			transform.Translate (Vector3.left * Time.deltaTime * walkSpeed );	
		}
		
		else if(myDir == Direction.right)
			transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );	
		
		else if(myDir == Direction.none){
			GameObject hero = GameObject.Find("_Hero");
			float distance = Mathf.Abs(hero.transform.position.x - this.transform.position.x);
			if(distance < 5){
				
				if(nextDir == Direction.left)
					myDir = Direction.left;
				
				else if(nextDir == Direction.right)
					myDir = Direction.right;
			}
		}
		
}
	void OnTriggerEnter(Collider col){
		if(col.name == "GirlTriggerStopLeft"){
			myDir = Direction.none;
			nextDir = Direction.left;
		}
		
		if(col.name == "GirlTriggerStopRight"){
			myDir = Direction.none;
			nextDir = Direction.right;
		}
		
		if(col.name == "GirlTriggerJump"){
			useJump = true;	
		}
		
		if(col.name == "GirlTriggerJumpFloat"){
			useJump = true;
			Invoke("enableFloat", .7f);
		}
		
		if(col.name == "GirlCheckPoint"){
			girlCheckpoint = this.transform.position;
		}
		
		if(col.name == "GirlTriggerDestroy"){
			this.gameObject.SetActive(false);
			//Destroy(this.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Ground"){
			isGrounded = true;
		    cancelFloat();	
		}
	}
	
	void OnCollisionExit(Collision col){
		if(col.gameObject.tag == "Ground")
			isGrounded = false;
	}
	
	public void goRight(){
		myDir = Direction.right;	
	}
	
	public void goLeft(){
		startAnim = false;
		myDir = Direction.left;	
	}
	
	public void stop(){
		if(isGrounded){
			lastDir = myDir;
			myDir = Direction.none;
		}
	}
	
	public void enableFloat(){
		useFloat = true;	
	}
	
	public void cancelFloat(){
		rigidbody.useGravity = true;
		useFloat = false;
	}
	
	public void restart(){
		this.gameObject.SetActive(true);
		this.transform.position = girlCheckpoint;	
		myDir = Direction.left;
	}
}