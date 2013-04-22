using UnityEngine;
using System.Collections;

public class Platform_Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "_Hero"){
			transform.parent.GetComponent<MovingPlatform>().enabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
