using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
	public float rockSpeed = 40;
	// Use this for initialization
	void Start () {
		Invoke("DestroyMe", 5);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * Time.deltaTime * rockSpeed);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Enemy"){
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
	}
	
	void DestroyMe(){
		Destroy(this.gameObject);	
	}
}
