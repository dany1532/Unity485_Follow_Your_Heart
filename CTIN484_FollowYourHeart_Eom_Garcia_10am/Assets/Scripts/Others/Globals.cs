using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour
{
	// Voice text globals
	public static float textSpeed = 0.025f;
	public static string upperVoice = "";
	public static string lowerVoice = "";
	public static bool deathMother = false;
	public static int waitTime = 0;
	
	// Hero position
	public static Vector3 heroPos = Vector3.zero;
	
	// Others
	public static GameObject currentCheckPoint;
	public Transform vectionCandle;
	public Transform vbParent;
	public Transform candlePrefab;
	public Transform rainPrefab;
	public static Transform level1;
	public static Transform level2;
	public GameObject tutorialLight;
	private static int currentLevel = 0;
	private static bool fadeLight = false;
	public int fadeSpeed = 3;
	
	void Awake() {
		if(GameObject.Find("_Hero") != null){
			vbParent = (new GameObject()).transform;
			vbParent.position = GameObject.Find("_Hero").transform.position;
			vbParent.gameObject.name = "CandleP";
			for (int i=0; i<100; i++) {
				Vector3 pos = new Vector3(Random.value *+720,Random.value*100 - 40, Random.value*200 + 10);
				Transform vb = Instantiate(vectionCandle, pos, Quaternion.identity) as Transform;
				vb.parent = vbParent;
			}
		}
		
		if(GameObject.Find("_Hero") != null)
			heroPos = GameObject.Find("_Hero").transform.position;
	}
	

	
	public static void loadNextLevelTutorial(){
		currentLevel++;
		fadeLight = true;
	}
	
	public static void loadNextLevel(){
		currentLevel++;
		Application.LoadLevel(currentLevel);
	}
	
	public static void FadeLight(){
		fadeLight = true;	
	}
	
	public static void deleteLevel1(){
		level1 = GameObject.Find("FirstLevel").transform;
		List<GameObject> children = new List<GameObject>();
    	foreach (Transform child in level1) 
			children.Add(child.gameObject);
    	children.ForEach(child => Destroy(child));	
	}
	
	public static void deleteLevel2(){
		level2 = GameObject.Find("SecondLevel").transform;
		List<GameObject> children = new List<GameObject>();
    	foreach (Transform child in level1) 
			children.Add(child.gameObject);
    	children.ForEach(child => Destroy(child));	
	}
	
	public static void turnOffLight(){
		Transform parent = GameObject.Find("CandleP").transform;
		foreach(Transform child in parent){
			child.FindChild("Candle_Halo").light.enabled = false;	
		}
		Transform lantern = GameObject.Find("_Hero").transform.FindChild("Lantern");
		lantern.FindChild("Candle_Light").light.enabled = false;
		
		
	}
	
	public static void turnOnLight(){
		/*Transform parent = GameObject.Find("CandleP").transform;
		foreach(Transform child in parent){
			child.FindChild("Candle_Halo").light.enabled = true;	
		}*/
		Transform lantern = GameObject.Find("_Hero").transform.FindChild("Lantern");
		lantern.FindChild("Candle_Light").light.enabled = true;
	}
	
	void Update(){
		if(GameObject.Find("_Hero") != null){
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
				
				if(-heroY+child.position.y > 80){
					Vector3 loc = child.position;
					loc.y -= 100;
					child.position = loc;
				}
				
				else if(heroY - child.position.y > 80){
					Vector3 loc = child.position;
					loc.y += 100;
					child.position = loc;	
				}
			}
		}
		
		if(fadeLight){
			float intensity = tutorialLight.light.intensity - Time.deltaTime/(fadeSpeed);
			tutorialLight.light.intensity = intensity;
			turnOffLight();
			
			if(intensity <= 0){
				fadeLight = false;
				Invoke("goToNextLevel",waitTime);
				
			}
		}
	}
	
	void goToNextLevel(){
		Application.LoadLevel(currentLevel);
	}
	

	
	
}
