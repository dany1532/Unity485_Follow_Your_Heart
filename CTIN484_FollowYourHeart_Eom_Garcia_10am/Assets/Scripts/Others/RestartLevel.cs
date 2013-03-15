using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour {
	public GameObject groupPlatformsPrefab;
	public GameObject myPlatforms;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			//myPlatforms = (new GameObject()).transform;
			//myPlatforms.gameObject.name = "GroupPlatforms";
			if(myPlatforms != null)
				Destroy(myPlatforms);
			myPlatforms = Instantiate(groupPlatformsPrefab) as GameObject;
			//myPlatforms = vb;
		}
	} 
}
