using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour
{
		// Voice text globals
	public static float textSpeed = 0.025f;
	public static string upperVoice = "";
	public static string lowerVoice = "";
	public static bool deathMother = false;
	
	public static GameObject currentCheckPoint;
	public Transform vectionCandle;
	public Transform vbParent;
	public Transform candlePrefab;
	public Transform rainPrefab;
	public GameObject tutorialLight;
	private static int currentLevel = 0;
	private static bool fadeLight = false;
	public int fadeSpeed = 3;
	
	void Awake() {
		vbParent = (new GameObject()).transform;
		vbParent.position = GameObject.Find("_Hero").transform.position;
		vbParent.gameObject.name = "Candle";
		for (int i=0; i<100; i++) {
			Vector3 pos = new Vector3(Random.value *+720,Random.value*100 - 40, Random.value*200 + 10);
			Transform vb = Instantiate(vectionCandle, pos, Quaternion.identity) as Transform;
			vb.parent = vbParent;
		}
	}
	
	public static void loadNextLevelTutorial(){
		currentLevel++;
		fadeLight = true;
	}
	
	public static void FadeLight(){
		fadeLight = true;	
	}
	
	
	
	void Update(){
		float heroX = GameObject.Find("_Hero").transform.position.x;
		float heroY = GameObject.Find("_Hero").transform.position.y;
		foreach (Transform child in vbParent){
			if (-heroX+child.position.x > 100){
				Vector3 loc = child.position;
				loc.x -= 200;
				child.position = loc;
			}
			
			else if(heroX-child.position.x > 100){
				Vector3 loc = child.position;
				loc.x += 200;
				child.position = loc;	
			}
			
			if(-heroY+child.position.y > 100){
				Vector3 loc = child.position;
				loc.y -= 200;
				child.position = loc;
			}
			
			else if(heroY - child.position.y > 100){
				Vector3 loc = child.position;
				loc.y += 200;
				child.position = loc;	
			}
		}
		
		if(fadeLight){
			float intensity = tutorialLight.light.intensity - Time.deltaTime/(fadeSpeed);
			tutorialLight.light.intensity = intensity;
			
			if(intensity <= 0){
				fadeLight = false;
				Invoke("goToNextLevel",2);
				
			}
		}
	}
	
	void goToNextLevel(){
		Application.LoadLevel(currentLevel);
	}
	

	
	
}
