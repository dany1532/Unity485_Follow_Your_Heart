using UnityEngine;
using System.Collections;

public class UmbrellaGirl : MonoBehaviour {
	public bool startAnim = false;
	public float walkSpeed = 6;
	public float traversalDistance = 15f;
	private float desiredPosition;
	
	// Use this for initialization
	void Start () {
		Vector3 loc = this.transform.position;
		desiredPosition = loc.x + traversalDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if(startAnim){
			float locX = this.transform.position.x;
			if(locX < desiredPosition){
				transform.Translate (Vector3.right * Time.deltaTime * walkSpeed );	
			}
			else
				startAnim = false;
		}
	}
}
