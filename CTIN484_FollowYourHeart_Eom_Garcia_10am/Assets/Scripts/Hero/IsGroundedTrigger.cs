using UnityEngine;
using System.Collections;

public class IsGroundedTrigger : MonoBehaviour {
	
	CharacterMovement charMovement;
	public ProtagonistAnimation anim;
	
	// Use this for initialization
	void Start ()
	{
		charMovement = transform.parent.GetComponent<CharacterMovement>();
		anim = transform.parent.GetComponent<ProtagonistAnimation>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "grass")
		{
			charMovement.isGrounded = true;
			charMovement.landing = true;
			anim.sJump = ProtagonistAnimation.StateJump.landing;
			anim.audioState = ProtagonistAnimation.AudioState.grass;
		}
		else if(other.tag == "dry")
		{
			charMovement.isGrounded = true;
			charMovement.landing = true;
			anim.sJump = ProtagonistAnimation.StateJump.landing;
			anim.audioState = ProtagonistAnimation.AudioState.dry;
		}
		
		else if(other.tag == "wet")
		{
			charMovement.isGrounded = true;
			charMovement.landing = true;
			anim.sJump = ProtagonistAnimation.StateJump.landing;
			anim.audioState = ProtagonistAnimation.AudioState.wet;
		}
		
		else if (other.tag == "ground"){
			print("here?");
			charMovement.isGrounded = true;
			charMovement.landing = true;
			anim.sJump = ProtagonistAnimation.StateJump.landing;
			anim.audioState = ProtagonistAnimation.AudioState.none;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "grass" || other.tag == "dry" || other.tag == "wet" || other.tag == "ground")
		{
			charMovement.isGrounded = false;
		}
	}
}
