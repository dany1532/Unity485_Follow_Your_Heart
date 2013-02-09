/*
 * Things to implement:
 * - Acceleration/force/friction to character movement
 * 
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	
	public float vSpeed = 5;
	public float hSpeed = 5;
	
	private bool isJumping;
	
	void Start ()
	{
		isJumping = false;
	}
	
	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
			Jump();
		
		HorizontalMovement();
	}
	
	void Jump()
	{
		if(!isJumping)
		{
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, vSpeed, 0);
			isJumping = true;
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
}
