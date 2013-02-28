using UnityEngine;
using System.Collections;

public class Push_Block : MonoBehaviour {
	public float pushSpeed = 3;
	public enum movementState {pushedRight, pushedLeft, neutral};
	public movementState myState;
	public bool beingPushed = false;
	CharacterMovement movScript;
	// Use this for initialization
	void Start () {
		myState = movementState.neutral;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myState == movementState.pushedLeft){
			transform.parent.transform.Translate(Vector3.left * Time.deltaTime * pushSpeed );	
		}
		
		else if(myState == movementState.pushedRight){
			transform.parent.transform.Translate(Vector3.right * Time.deltaTime * pushSpeed );	
		}
	}
				
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			movScript = col.GetComponent<CharacterMovement>();
			Invoke("checkIfPushed",.1f);
			
		}
	}
	
	public void checkIfPushed(){
		if(movScript.pushState == CharacterMovement.states.pushingLeft){
				myState = movementState.pushedLeft;
		}
		
		else if(movScript.pushState == CharacterMovement.states.pushingRight){
				myState = movementState.pushedRight;
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Player"){
			myState = movementState.neutral;
		}
	}
}
