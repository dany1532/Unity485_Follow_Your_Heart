using UnityEngine;
using System.Collections;

public class Hacks : MonoBehaviour {
	
	CharacterMovement cm;
	
	// Use this for initialization
	void Start ()
	{
		cm = GetComponent<CharacterMovement>();
		print ("Hack script on. Press 'K' to toggle speed hack.");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			if(cm.walkSpeed == 6)
			{
				cm.walkSpeed = 20;
			}
			else
			{
				cm.walkSpeed = 6;
			}
		}
	}
}
