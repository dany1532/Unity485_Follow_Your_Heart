using UnityEngine;
using System.Collections;

public class FinalLevelMechanic : MonoBehaviour
{
	CharacterMovement charMovement;
	private float lowerBoundY = -20;			// if hero falls below this y-point, reset to last checkpoint
	private float defaultWalkSpeed;
	private float boostedWalkSpeed;
	
	void Start ()
	{
		charMovement = this.GetComponent<CharacterMovement>();
		defaultWalkSpeed = charMovement.walkSpeed;
		boostedWalkSpeed = charMovement.walkSpeed;
	}
	
	void Update ()
	{
		if(charMovement.isGrounded)
		{
			charMovement.walkSpeed = defaultWalkSpeed;
		}
		else
		{
			charMovement.walkSpeed = boostedWalkSpeed;
		}
		
		if (transform.position.y < lowerBoundY)
		{
			transform.position = new Vector3(0,0,0);
			boostedWalkSpeed *= 1.2f;
		}
	}
}
