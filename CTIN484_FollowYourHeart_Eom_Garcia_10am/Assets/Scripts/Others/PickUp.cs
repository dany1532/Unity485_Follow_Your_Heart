using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "_Hero"){
			
			if(this.gameObject.name == "Pills"){
				other.gameObject.GetComponent<CharacterMovement>().hasPills = true;
				Destroy(this.gameObject);
			}
			
			if(this.gameObject.name == "Umbrella"){
				other.gameObject.GetComponent<CharacterMovement>().turnOnFloating();
				Destroy(this.gameObject);
			}
			
			if(this.gameObject.tag == "Lantern"){
				other.gameObject.GetComponent<CharacterMovement>().TurnOnLantern();
				Destroy(this.gameObject);
			}
		}
	}
}
