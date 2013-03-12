using UnityEngine;
using System.Collections;

public class TorchTrigger : MonoBehaviour {
	public bool active;

	// Use this for initialization
	void Start () {
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
//When Player gets close, activate checkpoint
	void OnTriggerEnter(Collider col){
		if(col.CompareTag("Player")){
			if(!active){
				Transform halo = this.transform.parent.FindChild("Torch_Halo");
				Transform brightness = this.transform.parent.FindChild("Torch_Light");
				halo.light.range = 4;
				brightness.light.range = 10;
				Globals.currentCheckPoint = this.gameObject;
			}

			
			active = true;
		}
	}
}
