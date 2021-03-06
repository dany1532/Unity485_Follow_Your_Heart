using UnityEngine;
using System.Collections;

public class ProtagonistAnimation : MonoBehaviour {
	public tk2dAnimatedSprite anim;
	CharacterMovement charMovement;
	public string lastClip;
	public enum DirectionJump {left,right,none}
	public enum StateJump {launch, air,landing, none};
	public enum AudioState{grass, dry, wet, none};
	public AudioState audioState = AudioState.grass;
	public DirectionJump dJump = DirectionJump.none;
	public StateJump sJump = StateJump.none;
	private bool doneLanding;
	private float lanternOffset = 1.6f;
	private Transform lantern;
	public Transform lanternLight;
	public bool falling = false;
	private PSoundFX audio;
	public GameObject rockPrefab;
	public bool throwingRock = false;
	public bool reloadingRock = false;
	public bool canThrowRock = false;
	//public string[] beginning = new string[] {"Idle_Left", "Idle_Right"};
	
	
	// Use this for initialization
	void Start () {
		anim = this.transform.FindChild("Hero_Sprite").GetComponent<tk2dAnimatedSprite>();
		charMovement = this.GetComponent<CharacterMovement>();
		audio = this.GetComponent<PSoundFX>();
		//lastClip = "Idle_Left";
		lantern = this.transform.FindChild("Lantern");
		
	}
	
	// Update is called once per frame
	void Update () {
	
		// Jump direction
		//if(charMovement.state == CharacterMovement.states.jump && !anim.IsPlaying("InitialJump_Left"))
		//{
		
		if(Input.GetKeyDown(KeyCode.F) && !throwingRock && canThrowRock ){
			throwingRock = true;
			if(dJump == DirectionJump.left || lastClip == "Idle_Left" || lastClip == "Walk_Left"){
				anim.Play("Throw_Left");
			}
			else if(dJump == DirectionJump.right || lastClip == "Idle_Right" || lastClip == "Walk_Right"){
				anim.Play("Throw_Right");
			}
			anim.animationCompleteDelegate = ThrowCompleteDelegate;
			anim.animationEventDelegate = RockDelegate;
				/*Vector3 loc = this.transform.position;
				if(isRockLeft){
					loc.x -= 2;
				}
				else{
					loc.x += 2;
				}
				loc.y += 1f;
				Instantiate(rockPrefab,	loc, Quaternion.identity);*/
		}
		
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
			anim.Stop();
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
			  !falling && charMovement.state != CharacterMovement.states.jump
			   && !throwingRock){
			
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
		
			if(audioState == AudioState.grass)
				audio.playGrassLanding();
			
			else if (audioState == AudioState.dry)
				audio.playDryLanding();
			
			else if (audioState == AudioState.wet)
				audio.playWetLanding();
			
		}
		
	//Idle Left animation
		else if(charMovement.state == CharacterMovement.states.idleLeft &&
			  		!anim.IsPlaying("Land_Left") && !anim.IsPlaying("Idle_Left")
			         && !throwingRock){
			 anim.Play("Idle_Left");
			 anim.animationCompleteDelegate = null;	
			 lastClip = "Idle_Left";
		}
		
		else if(charMovement.state == CharacterMovement.states.idleLeft &&
			  		!anim.IsPlaying("Land_Left") && !anim.IsPlaying("Idle_Left")
			         && throwingRock){
			charMovement.throwingRock = true;
		}
		
	//Walking Left animation
		else if(charMovement.state == CharacterMovement.states.walkLeft &&
							doneLanding && !anim.IsPlaying("Walk_Left")
			                 && !throwingRock){
			anim.Play("Walk_Left");
			anim.animationEventDelegate = WalkingEventDelegate;
			anim.animationCompleteDelegate = null;
			lastClip = "Walk_Left";
			moveLanternLeft();
			//if(audioState == AudioState.grass)
				
		}
		
		else if(charMovement.state == CharacterMovement.states.walkLeft &&
							doneLanding && !anim.IsPlaying("Walk_Left")
			                 && throwingRock){
			charMovement.throwingRock = true;
		}
		
	//Idle Right animation
		else if(charMovement.state == CharacterMovement.states.idleRight &&
			  		!anim.IsPlaying("Land_Right") && !anim.IsPlaying("Idle_Right")
			         && !throwingRock){
			 anim.Play("Idle_Right");
			 anim.animationCompleteDelegate = null;	
			 lastClip = "Idle_Right";
		}
		
		else if(charMovement.state == CharacterMovement.states.idleRight &&
			  		!anim.IsPlaying("Land_Right") && !anim.IsPlaying("Idle_Right")
			         && throwingRock){
			charMovement.throwingRock = true;
		}
		
	//Walking Right animation
		else if(charMovement.state == CharacterMovement.states.walkRight &&
			     			doneLanding && !anim.IsPlaying("Walk_Right")
			                && !throwingRock){
			anim.Play("Walk_Right");
			anim.animationCompleteDelegate = null;
			anim.animationEventDelegate = WalkingEventDelegate;
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
		throwingRock = false;
		charMovement.throwingRock = false;
		
    }
	
	void WalkingEventDelegate(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum){
		if(frame.eventInfo == "step1"){
			if(audioState == AudioState.grass)
				audio.playGrassWalkingS1();
			
			else if(audioState == AudioState.dry)
				audio.playDryWalkingS1();
			
			else if(audioState == AudioState.wet)
				audio.playWetWalkingS1();
		}
		
		else if(frame.eventInfo == "step2"){
			if(audioState == AudioState.grass)
				audio.playGrassWalkingS2();
			
			else if(audioState == AudioState.dry)
				audio.playDryWalkingS2();
			
			else if(audioState == AudioState.wet)
				audio.playWetWalkingS2();
		}
			
	}
	
	void RockDelegate(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum){
		if(frame.eventInfo == "Rock")
			charMovement.throwRock();
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
	
	void ThrowCompleteDelegate(tk2dAnimatedSprite sprite, int clipId)
    {
		throwingRock = false;
		charMovement.throwingRock = false;
		if(charMovement.state == CharacterMovement.states.jump && !falling
			&& sJump == StateJump.launch){
			charMovement.state = CharacterMovement.states.falling;
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
		throwingRock = false;
		charMovement.throwingRock = false;
		
	}
}
