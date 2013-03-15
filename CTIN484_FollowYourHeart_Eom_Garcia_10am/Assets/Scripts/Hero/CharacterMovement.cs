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
	private float jumpSpeed = 15;
	private float climbSpeed = 6;
	private float pushingSpeed = 3;
	public float floatingSpeed = 1;
	//private float maxJump = 15;
	
	// mario jump
	private float extendedJumpPeriod = 0.3f;	// the extra time a player may hold the jump button to get an extended jump
	private float extendedJumpAlarm;			// the time at which the player no longer gets extended jump power
	private float extendedJumpVelocity = 25f;
	
	public GameObject rockPrefab;
	
	public enum states {idle, walk, climb, jump, floating, pushingRight, pushingLeft};
	public states state;
	public bool isGrounded;
	public bool canClimb;
	public bool nearSwitch;
	public bool hasPills = false;
	public bool nextLevel = false;
	public bool inCutscene = false;
	private float lanternOffset = 1.6f;
	private Transform lantern;
	public Transform lanternLight;
	
	private float ladderPosX;
	private float ladderPosDiff = 0.2f;	// Maximum distance between ladderPosX and character before clamping character to ladder
	private float ladderPosAdd = 1f;	// Adding a length to the left or right of ladderPosX to distinguish player to the left or right 
	
	void Start()
	{
		state = states.idle;
		lantern = this.transform.FindChild("Lantern");
	}
	
	void Update()
	{
		if(!inCutscene)
		{
			// Hero position
			Globals.heroPos = transform.position;
				
			// Jumping
			if(Input.GetKeyDown(KeyCode.Space) && isGrounded)				// standard jump
			{
				state = states.jump;
				extendedJumpAlarm = Time.time + extendedJumpPeriod;
				rigidbody.velocity = Vector3.up * jumpSpeed;
			}
			if(Input.GetKeyDown(KeyCode.Space) && state == states.climb)	// jumping off a ladder
			{
				if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
				{
					state = states.jump;
					rigidbody.velocity = Vector3.up * jumpSpeed;
				}
				else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
				{
					state = states.jump;
					rigidbody.velocity = Vector3.up * jumpSpeed;
				}
			}
			if(Input.GetKey(KeyCode.Space))									// mario jump extension
			{
				if(Time.time < extendedJumpAlarm)
				{
					print ("hello");
					rigidbody.velocity += Vector3.up * extendedJumpVelocity * Time.deltaTime;
				}
			}
			
			// Horizontal movement
			if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				RaycastHit hit;
				Vector3 loc = lantern.transform.position;
				loc.x = this.transform.position.x -lanternOffset;
				lantern.transform.position = loc;
					
				loc = lanternLight.transform.position;
				loc.x = lantern.transform.position.x - lanternOffset;
				lanternLight.transform.position = loc;
					
				if(!rigidbody.SweepTest(Vector3.left, out hit, Time.deltaTime * walkSpeed))
				{
					transform.Translate (Vector3.left * Time.deltaTime * walkSpeed );
				}
			}
			else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
			{
				Vector3 loc = this.transform.position;
				RaycastHit hit;
				loc.x = this.transform.position.x +lanternOffset;
				lantern.transform.position = loc;
					
				loc = lanternLight.transform.position;
				loc.x = lantern.transform.position.x +lanternOffset;
				lanternLight.transform.position = loc;
				if(!rigidbody.SweepTest(Vector3.right, out hit, Time.deltaTime * walkSpeed))
				{
					transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );
				}
			}
			
			// Ladder climbing
			if(canClimb)
			{
				if(Mathf.Abs(transform.position.x-ladderPosX) < ladderPosDiff)
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
				if(state == states.climb)
				{
					transform.position = new Vector3(ladderPosX,transform.position.y,transform.position.z);
				}
			}
			
			// Floating
			if(Input.GetKeyDown(KeyCode.E) && !isGrounded)	// toggle floating while in air
			{
				if(state != states.floating)
				{
					state = states.floating;
				}
				else
				{
					state = states.jump;
				}
			}
			if(state == states.floating)
			{
				if(rigidbody.velocity.y < 0)	// turn gravity off and use floatingSpeed when state = float and is falling
				{
				rigidbody.useGravity = false;
				rigidbody.velocity = new Vector3(0,-floatingSpeed,0);
				}
				else
				{
				rigidbody.useGravity = true;
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
				loc.x -= 2;
				Instantiate(rockPrefab,	loc, Quaternion.identity);
			}
		}
	}
	
	void OnCollisionEnter()
	{
		
		
	}
	
	public void TurnOnLantern(){
		Transform myLantern = this.transform.FindChild("Lantern");
		myLantern.renderer.enabled = true;
		myLantern.FindChild("Candle_Halo").light.enabled = true;
		myLantern.FindChild("Candle_Light").light.enabled = true;
		this.transform.FindChild("Hero_Spotlight").light.enabled = false;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "Ladder")
		{
			ladderPosX = collider.transform.position.x;
			canClimb = true;
		}
		if(collider.tag == "Switch")
		{
			nearSwitch = true;
		}
		
		if(collider.tag == "Push_Block"){
			if(Input.GetKey(KeyCode.A)){
				state = states.pushingLeft;
			}
			else if(Input.GetKey(KeyCode.D)){
				state = states.pushingRight;
			}
		}
		
		if(collider.gameObject.name == "Death_Fall" ||
			collider.gameObject.name == "Boulder_Spikes"){
			Vector3 loc = Globals.currentCheckPoint.transform.position;
			loc.z = 1.25f;
			this.transform.position = loc;
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
			state = states.idle;
		}
	}
}