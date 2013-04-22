using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
	public float rockSpeed = 20;
	private bool isLeft = true;
	// Use this for initialization
	void Start () {
		isLeft = GameObject.Find("_Hero").GetComponent<CharacterMovement>().
														   isRockGoingLeft();
		if(isLeft)
			rigidbody.AddForce(Vector3.left * 700);
		else
			rigidbody.AddForce(Vector3.right * 700);
		Invoke("DestroyMe", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
			//transform.Translate(Vector3.left * Time.deltaTime * rockSpeed);
		//else
			//transform.Translate(Vector3.right * Time.deltaTime * rockSpeed);
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Enemy"){
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
	}
	
	public void setRockDirection(bool checkLeft){
		isLeft = checkLeft;	
	}
	void DestroyMe(){
		Destroy(this.gameObject);	
	}
}
