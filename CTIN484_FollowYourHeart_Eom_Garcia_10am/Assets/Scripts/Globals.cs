using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
		// Voice text globals
	public static float textSpeed = 0.025f;
	public static string upperVoice = "";
	public static string lowerVoice = "";
	
	public static GameObject currentCheckPoint;
	public Transform vectionCandle;
	public Transform vbParent;
	public Transform candlePrefab;
	
	void Awake() {
		vbParent = (new GameObject()).transform;
		vbParent.position = GameObject.Find("_Hero").transform.position;
		vbParent.gameObject.name = "Candle";
		for (int i=0; i<1000; i++) {
			Vector3 pos = new Vector3(Random.value *-1000,Random.value*100, Random.value*200 + 10);
			Transform vb = Instantiate(vectionCandle, pos, Quaternion.identity) as Transform;
			vb.parent = vbParent;
		}
	}
	
	void Update(){
		float heroX = GameObject.Find("_Hero").transform.position.x;
		foreach (Transform child in vbParent){
			if (-heroX+child.position.x > 100){
				Vector3 loc = child.position;
				loc.x += 100;
				child.position = loc;
			}
		}
	}
	

	
	
}
