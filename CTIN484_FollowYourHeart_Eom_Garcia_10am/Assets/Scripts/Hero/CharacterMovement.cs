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
	public float walkSpeed = 6;
	private float jumpSpeed = 14;
	private float climbSpeed = 6;
	private float pushingSpeed = 3;
	
	public float gravityAccel;
	
	public GameObject rockPrefab;
	
	public enum states {idle, walk, climb, jump, floating, pushingRight, pushingLeft,};
	public states state;
	
	public states pushState = states.idle;
	public bool isGrounded;
	public bool canClimb;
	public bool nearSwitch;
	
	void Start()
	{
		state = states.idle;
		gravityAccel = Physics.gravity.y;
	}
	
	void Update()
	{
		// ** SPEED HACK **
		
		
		// Jumping
		if(Input.GetKey(KeyCode.Space) && isGrounded)
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
			
			if(Input.GetKey(KeyCode.Space) && state == states.climb)
			{
				state = states.jump;
				rigidbody.useGravity = true;
				rigidbody.velocity = Vector3.up * jumpSpeed;
			}
		}
		
		// Floating
		if(Input.GetKeyDown(KeyCode.Q) && !isGrounded)
		{
			if(state != states.floating)
			{
				state = states.floating;
				Physics.gravity = Vector3.up * gravityAccel / 2;
			}
			else
			{
				state = states.jump;
				Physics.gravity = Vector3.up * gravityAccel;
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
		
		if(Input.GetKeyDown(KeyCode.F)){
			Vector3 loc = this.transform.position;
			loc.x -= 1;
			Instantiate(rockPrefab,	loc, Quaternion.identity);
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
		
		if(collider.tag == "Push_Block"){
			if(Input.GetKey(KeyCode.A)){
				pushState = states.pushingLeft;
			}
			else if(Input.GetKey(KeyCode.D)){
				pushState = states.pushingRight;
			}
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
		
		if(collider.tag == "Push_Block"){
			pushState = states.idle;
		}
	}
}