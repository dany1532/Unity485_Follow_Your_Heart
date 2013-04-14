using UnityEngine;
using System.Collections;

public class ProtagonistAnimation : MonoBehaviour {
	public tk2dAnimatedSprite anim;
	CharacterMovement charMovement;
	public string lastClip;
	public enum DirectionJump {left,right,none}
	public enum StateJump {launch, air,landing, none};
	public DirectionJump dJump = DirectionJump.none;
	public StateJump sJump = StateJump.none;
	private bool doneLanding;
	private float lanternOffset = 1.6f;
	private Transform lantern;
	public Transform lanternLight;
	private bool falling = false;
	//public string[] beginning = new string[] {"Idle_Left", "Idle_Right"};
	
	
	// Use this for initialization
	void Start () {
		anim = this.transform.FindChild("Hero_Sprite").GetComponent<tk2dAnimatedSprite>();
		charMovement = this.GetComponent<CharacterMovement>();
		//lastClip = "Idle_Left";
		lantern = this.transform.FindChild("Lantern");
		
	}
	
	// Update is called once per frame
	void Update () {
	
		// Jump direction
		//if(charMovement.state == CharacterMovement.states.jump && !anim.IsPlaying("InitialJump_Left"))
		//{
			if(sJump == StateJump.air)
			{
				if (dJump == DirectionJump.left)
				{
					if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
					{
						dJump = DirectionJump.right;
					    lastClip = "Idle_Right";
						anim.Play("Jump_Right");
						moveLanternRight();
					}
				}
				else if (dJump == DirectionJump.right)
				{
					if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
					{
						dJump = DirectionJump.left;
						lastClip = "Idle_Left";
						anim.Play("Jump_Left");	
						moveLanternLeft();
					}
				}
			}
		//}
		
	//Jumping animation
		if(charMovement.inCutscene){
			this.transform.FindChild("Hero_Sprite").GetComponent<MeshRenderer>().enabled = false;	
		}
		
		else{
			this.transform.FindChild("Hero_Sprite").GetComponent<MeshRenderer>().enabled = true;
		}
		
		if(charMovement.state == CharacterMovement.states.jump &&
			sJump == StateJump.none && !anim.IsPlaying("InitialJump_Left")){
			
			doneLanding = false;
			if (lastClip == "Idle_Left" || lastClip == "Walk_Left"){ 
			 	dJump = DirectionJump.left;
				sJump = StateJump.launch;
			 	anim.Play("InitialJump_Left");
			 	anim.animationCompleteDelegate = JumpCompleteDelegate;
			}
			
			if (lastClip == "Idle_Right" || lastClip == "Walk_Right"){ 
			 	dJump = DirectionJump.right;
				sJump = StateJump.launch;
			 	anim.Play("InitialJump_Right");
			 	anim.animationCompleteDelegate = JumpCompleteDelegate;
			}
			 
		}
		
	//falling animation
		else if(!charMovement.isGrounded && !anim.IsPlaying("Falling_Left")&&
			  !falling && charMovement.state != CharacterMovement.states.jump){
			
			charMovement.state = CharacterMovement.states.falling;
			falling = true;
			doneLanding = false;
			dJump = DirectionJump.left;
			sJump = StateJump.air;
			
			if(lastClip == "Idle_Right" || lastClip == "Walk_Right"){
				anim.Play("Falling_Right");	
				anim.animationCompleteDelegate = null;	
			}
			
			else if(lastClip == "Idle_Left" || lastClip == "Walk_Left"){
				anim.Play("Falling_Left");	
				anim.animationCompleteDelegate = null;
			};	
			
		}
		
	/*//Landing animation
		else if(charMovement.landing &&
			   !anim.IsPlaying("Land_Right") && !anim.IsPlaying("Land_Left")){
			
			if(dJump == DirectionJump.right){
				anim.Play("Land_Right");	
				anim.animationCompleteDelegate = LandCompleteDelegate;	
			}
			
			else if(dJump == DirectionJump.left){
				anim.Play("Land_Left");	
				anim.animationCompleteDelegate = LandCompleteDelegate;
			}
			
		}*/
			
	//Landing animation
		else if(charMovement.landing &&
			   !anim.IsPlaying("Land_Right") && !anim.IsPlaying("Land_Left")){
			
			if(lastClip == "Idle_Right" || lastClip == "Walk_Right"){
				anim.Play("Land_Right");	
				anim.animationCompleteDelegate = LandCompleteDelegate;	
			}
			
			else if(lastClip == "Idle_Left" || lastClip == "Walk_Left"){
				anim.Play("Land_Left");	
				anim.animationCompleteDelegate = LandCompleteDelegate;
			}
			
		}
		
	//Idle Left animation
		else if(charMovement.state == CharacterMovement.states.idleLeft &&
			  		!anim.IsPlaying("Land_Left") && !anim.IsPlaying("Idle_Left")){
			 anim.Play("Idle_Left");
			 anim.animationCompleteDelegate = null;	
			 lastClip = "Idle_Left";
		}
		
	//Walking Left animation
		else if(charMovement.state == CharacterMovement.states.walkLeft &&
							doneLanding && !anim.IsPlaying("Walk_Left")){
			anim.Play("Walk_Left");
			anim.animationCompleteDelegate = null;
			lastClip = "Walk_Left";
			moveLanternLeft();
		}
		
	//Idle Right animation
		else if(charMovement.state == CharacterMovement.states.idleRight &&
			  		!anim.IsPlaying("Land_Right") && !anim.IsPlaying("Idle_Right")){
			 anim.Play("Idle_Right");
			 anim.animationCompleteDelegate = null;	
			 lastClip = "Idle_Right";
		}
		
	//Walking Right animation
		else if(charMovement.state == CharacterMovement.states.walkRight &&
			     			doneLanding && !anim.IsPlaying("Walk_Right")){
			anim.Play("Walk_Right");
			anim.animationCompleteDelegate = null;
			lastClip = "Walk_Right";
			moveLanternRight();
		}
		
	}
	
	void LandCompleteDelegate(tk2dAnimatedSprite sprite, int clipId)
    {
       /* if(dJump == DirectionJump.left){
			sJump = StateJump.none;
			anim.Play("Idle_Left");
			anim.animationCompleteDelegate = null;
			charMovement.state = CharacterMovement.states.idleLeft;	
		}
		
		else if(dJump == DirectionJump.right){
			sJump = StateJump.none;
			anim.Play("Idle_Right");
			anim.animationCompleteDelegate = null;
			charMovement.state = CharacterMovement.states.idleRight;
		}
		
		charMovement.landing = false;
		doneLanding = true;
		falling = false;*/
		
		if(lastClip == "Idle_Left" || lastClip == "Walk_Left"){
			dJump = DirectionJump.left;
			sJump = StateJump.none;
			anim.Play("Idle_Left");
			anim.animationCompleteDelegate = null;
			charMovement.state = CharacterMovement.states.idleLeft;	
		}
		
		else if(lastClip == "Idle_Right" || lastClip == "Walk_Right"){
			dJump = DirectionJump.right;
			sJump = StateJump.none;
			anim.Play("Idle_Right");
			anim.animationCompleteDelegate = null;
			charMovement.state = CharacterMovement.states.idleRight;
		}
		
		charMovement.landing = false;
		doneLanding = true;
		falling = false;
		
    }
	
	void JumpCompleteDelegate(tk2dAnimatedSprite sprite, int clipId)
    {
		if(dJump == DirectionJump.left && sJump == StateJump.launch){// &&
			          //!anim.IsPlaying("Jump_Left")){
			anim.Play("Jump_Left");	
			anim.animationCompleteDelegate = null;
			sJump = StateJump.air;
		}
		
		else if(dJump == DirectionJump.right && sJump == StateJump.launch){// &&
			          //!anim.IsPlaying("Jump_Left")){
			anim.Play("Jump_Right");	
			anim.animationCompleteDelegate = null;
			sJump = StateJump.air;
		}
	}
	
	void moveLanternLeft(){
		    Vector3 loc = lantern.transform.position;
			loc.x = this.transform.position.x -lanternOffset;
		    loc.z = -2.25f;
			lantern.transform.position = loc;
				
			loc = lanternLight.transform.position;
			loc.x = lantern.transform.position.x - lanternOffset;
			lanternLight.transform.position = loc;	
	}
	
	void moveLanternRight(){
			Vector3 loc = this.transform.position;
			loc.x = this.transform.position.x +lanternOffset;
			loc.z = -2.25f;
			lantern.transform.position = loc;
				
			loc = lanternLight.transform.position;
			loc.x = lantern.transform.position.x +lanternOffset;
			
			lanternLight.transform.position = loc;	
	}
	
	void DoneLanding(){
		anim.Stop();
		sJump = StateJump.none;
		
	}
}
