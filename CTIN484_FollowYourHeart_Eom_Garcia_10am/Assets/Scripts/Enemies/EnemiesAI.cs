using UnityEngine;
using System.Collections;

public class EnemiesAI : MonoBehaviour
{
	public float moveSpeed = 5;
	public bool chaseAI = false;
	public float minPosX;
	public float maxPosX;
	
	public enum states {idle, patrolLeft, patrolRight, chase}
	public states state;

	// Use this for initialization
	void Start ()
	{
		state = states.patrolRight;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(state == states.chase)
		{
			if (transform.position.x < Globals.heroPos.x)
			{
				transform.Translate (Vector3.right * Time.deltaTime * moveSpeed );
			}
			else if (transform.position.x > Globals.heroPos.x)
			{
				transform.Translate (Vector3.left * Time.deltaTime * moveSpeed );
			}
		}
		else if(state == states.patrolLeft)
		{
			transform.Translate (Vector3.left * Time.deltaTime * moveSpeed );
			
			if(transform.position.x < minPosX)
			{
				state = states.patrolRight;
			}
		}
		else if(state == states.patrolRight)
		{
			transform.Translate (Vector3.right * Time.deltaTime * moveSpeed );
			
			if(transform.position.x > maxPosX)
			{
				state = states.patrolLeft;
			}
		}
	}
}
