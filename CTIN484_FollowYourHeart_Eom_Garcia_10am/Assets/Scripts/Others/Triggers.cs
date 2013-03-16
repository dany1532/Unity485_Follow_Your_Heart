/*
 * Temporary script!!
 * 
 * 
 * 
 * 
*/




using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour
{
	public GameObject deathPrefab;
	private bool beenKilled = false;
	private GameObject mySprite;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "Death_Fall" ){
			Vector3 loc = Globals.currentCheckPoint.transform.position;
			loc.z = 1.25f;
			this.transform.position = loc;
		}
		
		if(!beenKilled && other.gameObject.name == "Spike" ||
			other.gameObject.name == "Boulder_Spikes" || other.gameObject.name == "Enemy" ){
			
			Invoke("Restart", 2f);
			beenKilled = true;
			mySprite = this.transform.FindChild("Hero_Sprite").gameObject;
			this.GetComponent<CharacterMovement>().inCutscene = true;
			this.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			mySprite.SetActive(false);
			Vector3 pos = this.transform.position;
			pos.z -= 5f;
			Instantiate(deathPrefab, pos, Quaternion.identity);
			
			
		}
	}
	
	void Restart(){
		this.rigidbody.constraints = RigidbodyConstraints.None;
		
		Vector3 loc = Globals.currentCheckPoint.transform.position;
		loc.z = 1.25f;
		this.transform.position = loc;
		beenKilled = false;
		mySprite.SetActive(true);
		this.GetComponent<CharacterMovement>().inCutscene = false;
		
		this.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ 
									| RigidbodyConstraints.FreezeRotation;
	}
}
