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
	public float jumpSpeed = 15;
	private float climbSpeed = 6;
	private float horizClimbSpeed = 10;
	private float pushingSpeed = 3;
	public float floatingSpeed = 1;
	public float platformSpeedX = 0f;
	public float platformSpeedY = 0f;
	private bool isPlatformSpeedPositive = false;
	public bool canFloat = false;
	
	// mario jump
	public float extendedJumpPeriod = 0.3f;	// the extra time a player may hold the jump button to get an extended jump
	private float extendedJumpAlarm;			// the time at which the player no longer gets extended jump power
	public float extendedJumpVelocity = 2f;
	
	public GameObject rockPrefab;
	
	public enum states {idleLeft, idleRight, walkLeft, walkRight, climb, jump,
				 falling, floating, pushingRight, pushingLeft};
	public states state;
	public bool isGrounded;
	private bool isFloating;
	public bool canClimb;
	public bool nearSwitch;
	public bool hasApples = false;
	public bool nextLevel = false;
	public bool inCutscene = false;
	public bool landing = false;
	private bool isRockLeft = false;
	public bool throwingRock = false;
	
	
	private float ladderPosX;
	private float ladderClampMinDist = 1f;	// Minimum distance between ladderPosX and character before clamping character to ladder
	private float ladderClampBoundingDist = 0.8f;	// The limit in range of character horizontal movement while on ladder
	private float ladderCurrentDist = 0f;	// The current character x-distance away from ladder
	
	
	void Start()
	{
		state = states.idleLeft;
	}
	
	void Update()
	{
		
		if(!inCutscene)
		{
	
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
					rigidbody.useGravity = true;
					rigidbody.velocity += Vector3.up * extendedJumpVelocity * Time.deltaTime;
				}
			}
			
		// Horizontal movement
			
			if(state != states.climb)
			{
				// Move left
				if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
				{
					RaycastHit hit;
					
					isRockLeft = true;
		
					if(!throwingRock){
						if(!rigidbody.SweepTest(Vector3.left, out hit, Time.deltaTime * walkSpeed))
						{
							transform.Translate (Vector3.left * Time.deltaTime * walkSpeed );
						}
					}
					
					if(isGrounded && !landing && state != states.floating 
													&& state != states.climb){
							state = states.walkLeft;	
					}
					else if(state != states.falling)
							state = states.jump;
		
				}
				// Move right
				else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
				{
					
					RaycastHit hit;
					isRockLeft = false;
					if(!throwingRock){
						if(!rigidbody.SweepTest(Vector3.right, out hit, Time.deltaTime * walkSpeed))
						{
							transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );
						}
					}
					if(isGrounded && !landing && state != states.floating
															&& state != states.climb){
							state = states.walkRight;
					}
					else if(state != states.falling)
							state = states.jump;
				}
					
				else if (isGrounded){
					if(Input.GetKeyUp(KeyCode.A))
						state = states.idleLeft;
					else if(Input.GetKeyUp(KeyCode.D))
						state = states.idleRight;	
				}
			}
			
		//Platform Translation
			
			if(state != states.jump){
				if(isPlatformSpeedPositive ){
					transform.Translate (Vector3.right * Time.deltaTime * platformSpeedX );
					transform.Translate (Vector3.up * Time.deltaTime * platformSpeedY);
				}
				
				else if(!isPlatformSpeedPositive ){
					transform.Translate (Vector3.left * Time.deltaTime * platformSpeedX );
					transform.Translate (Vector3.down * Time.deltaTime * platformSpeedY);
				}
			}
			else{
				platformSpeedX = 0f;
				platformSpeedY = 0f;
			}
		
		// Ladder climbing
			
			if(canClimb)
			{
				if(Mathf.Abs(transform.position.x-ladderPosX) < ladderClampMinDist)
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
				
			// Horizontal movement on ladder
				
				if(state == states.climb)
				{
					if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
					{																// Move left on ladder
						if(ladderCurrentDist > -ladderClampBoundingDist)
						{
							ladderCurrentDist -= Time.deltaTime * horizClimbSpeed;
						}
					}
					else if(!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
					{																// Move right on ladder
						if(ladderCurrentDist < ladderClampBoundingDist)
						{
							ladderCurrentDist += Time.deltaTime * horizClimbSpeed;
						}
					}
					else
					{																// Re-center character position on ladder
						if(ladderCurrentDist > 0.1f)
						{
							ladderCurrentDist -= Time.deltaTime * horizClimbSpeed;
						}
						else if (ladderCurrentDist < -0.1f)
						{
							ladderCurrentDist += Time.deltaTime * horizClimbSpeed;
						}
						else
						{
							ladderCurrentDist = 0;
						}
					}
					transform.position = new Vector3(ladderPosX + ladderCurrentDist,transform.position.y,transform.position.z);
				}
			}
		
		// Floating
			if(Input.GetKey(KeyCode.J) && !isGrounded && canFloat)	// toggle floating while in air
			{
				if(!isFloating)
				{
					state = states.floating;
					isFloating = true;
				}
				else
				{
					state = states.jump;
					isFloating = false;
					rigidbody.useGravity = true;
				}
			}
			else{
				isFloating = false;
				if(state != states.climb)
					rigidbody.useGravity = true;
			}
			if(isFloating)
			{
				//if(rigidbody.velocity.y < 0)	// turn gravity off and use floatingSpeed when state = float and is falling
				if(!isGrounded)
				{
					rigidbody.useGravity = false;
					rigidbody.velocity = new Vector3(0,-floatingSpeed,0);
				}
				else
				{
					rigidbody.useGravity = true;
					isFloating = false;
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
			
		//Something went wrong with the grounded trigger (hack)
			if(Input.GetKeyDown(KeyCode.L)){
				this.GetComponent<ProtagonistAnimation>().sJump = ProtagonistAnimation.StateJump.landing;
				this.isGrounded = true;
				this.landing = true;
			}
			
			if(Input.GetKeyDown(KeyCode.M)){
				Globals.loadNextLevel();
			}
			
		

	  	}
	}
	
	void OnCollisionEnter()
	{
		
		
	}
	
	public bool isRockGoingLeft(){
		return isRockLeft;
	}
	public void TurnOnLantern(){
		Transform myLantern = this.transform.FindChild("Lantern");
		//myLantern.renderer.enabled = true;
		//myLantern.FindChild("Candle_Halo").light.enabled = true;
		myLantern.FindChild("Candle_Light").light.enabled = true;
		//this.transform.FindChild("Hero_Spotlight").light.enabled = false;
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
		
		/*if(collider.gameObject.name == "Death_Fall" ||
			collider.gameObject.name == "Boulder_Spikes"){
			Vector3 loc = Globals.currentCheckPoint.transform.position;
			loc.z = 1.25f;
			this.transform.position = loc;
		} */
	}
	
	public void NewPlatformSpeedX(float newSpeed, bool isPositive){
		platformSpeedX = newSpeed;
		isPlatformSpeedPositive = isPositive;
	}
	
	public void NewPlatformSpeedY(float newSpeed, bool isPositive){
		platformSpeedY = newSpeed;
		isPlatformSpeedPositive = isPositive;
	}
	
	public void turnOnFloating(){
		canFloat = true;	
	}
	
	public void pickUpApple(){
		hasApples = true;
	}
	
	public void enableRockThrowing(){
		this.GetComponent<ProtagonistAnimation>().canThrowRock = true;
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
			state = states.idleLeft;
		}
	}
}