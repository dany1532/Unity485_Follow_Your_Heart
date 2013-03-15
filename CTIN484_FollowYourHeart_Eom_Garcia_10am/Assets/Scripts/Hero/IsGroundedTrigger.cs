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
		if(other.tag == "Ground")
		{
			charMovement.isGrounded = true;
			charMovement.landing = true;
			anim.sJump = ProtagonistAnimation.StateJump.landing;
			//charMovement.state = CharacterMovement.states.idleLeft;
			
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Ground")
		{
			charMovement.isGrounded = false;
		}
	}
}
