using UnityEngine;
using System.Collections;

public class IsGroundedTrigger : MonoBehaviour {
	
	CharacterMovement charMovement;
	
	// Use this for initialization
	void Start ()
	{
		charMovement = transform.parent.GetComponent<CharacterMovement>();
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
			charMovement.state = CharacterMovement.states.idle;
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
