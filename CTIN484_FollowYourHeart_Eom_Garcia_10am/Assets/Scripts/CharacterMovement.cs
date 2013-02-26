/*
 * Things to implement:
 * - Acceleration/force/friction to character movement
 * - Max Y pos the character can get in the ladder(make him stop at certain height)
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	private float walkSpeed = 6;
	private float jumpSpeed = 14;
	private float climbSpeed = 6;
	
	private float distToSides;
	private float distToGround;
	
	private enum states {idle, walk, climb, jump};
	private states state;
	
	public bool canClimb;
	public bool nearSwitch;
	
	void Start()
	{
		state = states.idle;
		
		distToSides = collider.bounds.extents.x;
		distToGround = collider.bounds.extents.y;
	}
	
	void Update()
	{
		// Jumping
		if(Input.GetKey(KeyCode.Space) && isGrounded())
		{
			state = states.jump;
			rigidbody.velocity = Vector3.up * jumpSpeed;
		}
		
		// Horizontal movement
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			RaycastHit hit;
			if(!rigidbody.SweepTest(Vector3.left, out hit, Time.deltaTime * walkSpeed))
			{
				transform.Translate (Vector3.left * Time.deltaTime * walkSpeed );
			}
		}
		else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			RaycastHit hit;
			if(!rigidbody.SweepTest(Vector3.right, out hit, Time.deltaTime * walkSpeed))
			{
				transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );
			}
		}
		
		// Climbing
		if(canClimb)
		{
			if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
			{
				state = states.climb;
				rigidbody.useGravity = false;
				rigidbody.velocity = Vector3.zero;
				transform.Translate (Vector3.up * Time.deltaTime * climbSpeed);
			}
			else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
			{
				state = states.climb;
				rigidbody.useGravity = false;
				rigidbody.velocity = Vector3.zero;
				transform.Translate (Vector3.down * Time.deltaTime * climbSpeed);
			}
		}
		
		// Switches
		if(nearSwitch)
		{
			if (Input.GetKeyDown (KeyCode.S))
			{
				print ("Switch activated!");
			}
		}
	}
	
	void OnCollisionEnter()
	{
		if (rigidbody.velocity.y > 0)
		{
			rigidbody.velocity = Vector3.zero;
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Ladder")
		{
			canClimb = true;
		}
		if(collider.tag == "Switch")
		{
			nearSwitch = true;
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if(collider.tag == "Ladder")
		{
			canClimb = false;
			this.rigidbody.useGravity = true;
		}
		if(collider.tag == "Switch")
		{
			nearSwitch = false;
		}
	}
	
	bool isGrounded()
	{
		// Casts two rays downward from each of character's sides
		// If either ray collides, character is grounded
		if(Physics.Raycast(transform.position - new Vector3(distToSides,0,0), -Vector3.up, distToGround + 0.1f) ||
			Physics.Raycast(transform.position + new Vector3(distToSides,0,0), -Vector3.up, distToGround + 0.1f))
		{
			return true;
		}
		return false;
	}
}

/*
using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	
	public float vSpeed = 5;
	public float hSpeed = 5;
	public float climbSpeed = .5f;
	
	private bool isJumping;
	
	// Added events and States
	public enum events{canClimb, do_nothing};
	public enum states{climbing, do_nothing};
	public states state = states.do_nothing;
	public events myEvent = events.do_nothing;
	
	void Start ()
	{
		isJumping = false;
	}
	
	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
			Jump();
		
	//got near a ladder, you can climb...
		if(myEvent == events.canClimb)
			Climb();
		
		HorizontalMovement();
	}
	
	void Jump()
	{
		if(!isJumping && state != states.climbing)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, vSpeed, 0);
			isJumping = true;
		}
	}
	
	void Climb(){
	//while pressing up, you can climb ladder
		if(Input.GetKey(KeyCode.UpArrow)){
			rigidbody.velocity = Vector3.zero; 
			state = states.climbing;
			
			Vector3 loc = this.transform.position;
			loc.y += climbSpeed;
			this.transform.position = loc;
		}
	//while pressing down, you can climb down ladder
		else if(Input.GetKey(KeyCode.DownArrow)){
			if(state == states.climbing){
				rigidbody.velocity = Vector3.zero;
				state = states.climbing;
				
				Vector3 loc = this.transform.position;
				loc.y -= climbSpeed;
				this.transform.position = loc;
			}
		}
	//if you're climbing, disable gravity
		if(state == states.climbing){
			this.rigidbody.useGravity = false;	
		}
		
		
	}
	
	void HorizontalMovement()
	{
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			rigidbody.velocity = new Vector3(-hSpeed, rigidbody.velocity.y, 0);
		}
		else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			rigidbody.velocity = new Vector3(hSpeed, rigidbody.velocity.y, 0);
		}
		else
		{
			rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
		}
	}
	
	void OnCollisionEnter(Collision collision)
	{	
		if(collision.collider.tag == "Ground")						// if the colliding object is tagged "Ground"
		{
			Vector3 contactPoint = collision.contacts[0].point;
			
			if(transform.position.y > contactPoint.y)				// then if the character is higher than the point of contact
			{
				isJumping = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.tag == "Ladder"){
			myEvent = events.canClimb;
		}
	}
	
	void OnTriggerExit(Collider collider){
		if(collider.tag == "Ladder"){
			myEvent = events.do_nothing;
			state = states.do_nothing;
			this.rigidbody.useGravity = true;
		}
	}
}
*/