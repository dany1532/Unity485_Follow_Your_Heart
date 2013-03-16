using UnityEngine;
using System.Collections;

public class Death_Animation : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Vector3 vel = Random.insideUnitSphere;
		vel.z = 0f;
		rigidbody.velocity = vel * 20;
		Invoke("DestroyMe", 2f);
	}
	
	void DestroyMe(){
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
