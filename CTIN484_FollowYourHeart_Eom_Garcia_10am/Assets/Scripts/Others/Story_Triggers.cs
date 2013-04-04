using UnityEngine;
using System.Collections;

public class Story_Triggers : MonoBehaviour {
	TextMesh textMeshChild;
	int myId;
	public enum StoryEvent{ev0,ev1, ev2, ev3, ev4, ev5, ev6,ev7, do_nothing};
	public StoryEvent myEvent;
	private bool startCutscene = false;
	public GameObject rainPrefab;
	public GameObject lanternPrefab;
	private CharacterMovement playerScript;
	private GUIStyle lowerVoiceStyle;
	private UpperVoiceGUI upGUI;
	private LowerVoiceGUI lowGUI;
	private string newLowerVoice;
	public GameObject platform;
	public GameObject umbrellaGirl;
	private Music_Manager mng;
	private bool not = false;

	void Start(){
		Transform f = transform.FindChild("Trigger_Location");
		textMeshChild = f.GetComponent<TextMesh>();
		myId = int.Parse(textMeshChild.text);
		MeshRenderer rend = textMeshChild.GetComponent<MeshRenderer>();
		rend.enabled = false;
		
		lowerVoiceStyle =GameObject.Find("Environment_Scripts").
								GetComponent<LowerVoiceGUI>().myStyle;
		upGUI = GameObject.Find("Environment_Scripts").
								GetComponent<UpperVoiceGUI>();
		lowGUI = GameObject.Find("Environment_Scripts").
								GetComponent<LowerVoiceGUI>();
		if(GameObject.Find("Music_Manager") != null){
			mng = GameObject.Find("Music_Manager").GetComponent<Music_Manager>();
		}
		
	}
	
	void writeLowerVoice(string lowVoice){
		newLowerVoice = lowVoice;
		Invoke("printLowerVoice",1f);
	}
	
	void printLowerVoice(){
		Globals.lowerVoice = newLowerVoice;
	}
	
	public void nextLevel(){
		Globals.loadNextLevel();	
	}
	
	
	
	void OnTriggerEnter(Collider other){
	//Story Events
	if(other.gameObject.name == "_Hero"){
		if(!startCutscene){
			if(myId == 1){
				//Globals.upperVoice = "I remember that I was going to my home...";
				Globals.lowerVoice = "Move with A-D";
				//writeLowerVoice("Move with A-D");
				upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 2){
				//Globals.upperVoice = "My mom was waiting for me...";
				Globals.upperVoice = "I remember that I was going back home...";
				writeLowerVoice("Jump with Space");
				upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 3){
				//Globals.upperVoice = "She is my only family...";	
				Globals.upperVoice = "Just like every other day";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
			}
			
			if(myId == 4){
				Globals.upperVoice = "I always lived alone with my mom";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 5){
				Globals.upperVoice = "We were happy together";
				//Globals.lowerVoice = "I'm so proud of you.";
				writeLowerVoice("You can climb ladders with W");
				
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 6){
				Globals.upperVoice = "I would always help her with the chores";
				//Globals.lowerVoice = "I'm fine, dear.";
				writeLowerVoice("Be a dear and get us some fruits from outside");
				
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(55f);
			}
			
			if(myId == 7){
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent7", 0, 5.5f);
				lowGUI.setMyLeft(15f);
			}
			
			if(myId == 8){
				not = true;
				bool hasApples = other.gameObject.GetComponent<CharacterMovement>().hasApples;
				if(hasApples){
					Globals.lowerVoice = "Thank you dear, go upstairs and rest now";
					lowGUI.setMyLeft(155f);
					other.gameObject.GetComponent<CharacterMovement>().nextLevel = true;
				}
				
			}
			
			if(myId == 9){
				bool nextLevel = other.gameObject.GetComponent<CharacterMovement>().nextLevel;
				not = true;
				if(nextLevel && !startCutscene){
					other.gameObject.GetComponent<CharacterMovement>().inCutscene = true;
					startCutscene = true;
					mng.playDeath();
					Globals.turnOffLight();
					Globals.waitTime = 11;
					Globals.loadNextLevelTutorial();
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent9", 2, 5.5f);
				}
			}
			
			if(myId == 10){
				if(!startCutscene){
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent10", 0, 5.5f);
					//startCutscene = true;
					//Globals.deathMother = true;
				}
			}
			
			if(myId == 11){
				/*if(!startCutscene){
					Vector3 pos = other.gameObject.transform.position;
					pos.y += 50;
					pos.x -= 165;
					Instantiate(rainPrefab,pos,Quaternion.identity);
				}
				Globals.upperVoice = "I was scared, so I ran...";
				startCutscene = true; */
				Globals.upperVoice = "She was on the floor and at that moment...";
				upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 12){
				other.gameObject.GetComponent<CharacterMovement>().inCutscene = true;
				startCutscene = true;
				Globals.deathMother = true;
				mng.playDeath();
				Globals.turnOffLight();
				Globals.waitTime = 30;
				Globals.loadNextLevelTutorial();
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent12", 2, 5.5f);
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 13){
				Globals.upperVoice = "I didn't know what to do, so I ran away...";
				
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 14){
				//Globals.upperVoice = "I felt as if the world was collapsing around me...";
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent14", 0, 5.5f);
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(255f);
					
					Vector3 pos = other.gameObject.transform.position;
					pos.y += 50;
					pos.x -= 195;
					Instantiate(rainPrefab,pos,Quaternion.identity);
			}
			
			if(myId == 15){
				Globals.upperVoice = "Until finally...";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 16){
				if(!startCutscene){
					startCutscene = true;
					Globals.upperVoice = "I just gave up...";
					upGUI.setMyLeft(455f);
					upGUI.setMyTop(100f);
					lowGUI.setMyLeft(255f);
					Globals.turnOffLight();
					Invoke("nextLevel",4f);
					//GameObject.Find("TutorialLight").light.intensity = 0;
					mng.playDarkness();
					//Globals.loadNextLevelTutorial();
				}
			}
			
			if(myId == 17){
				if(!startCutscene){
					playerScript = other.gameObject.GetComponent<CharacterMovement>();
					playerScript.inCutscene = true;
					myEvent = StoryEvent.ev0;
					Globals.turnOffLight();
					InvokeRepeating("StoryEvent17", 0, 5.5f);
					startCutscene = true;
				}
			}
			
			if(myId == 18){
				Globals.upperVoice = "I wanted to be strong for her";
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 19){
				Globals.upperVoice = "And so I pushed forward";
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 20){
				Globals.upperVoice = "But this world is different...";
				Globals.lowerVoice = "You are an insect";
				lowGUI.setFearFont();
				lowGUI.setRedColor();
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 21){
				Globals.upperVoice = "It felt as if it wanted me to fail";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 22){
				Globals.lowerVoice = "You will fail";
				lowGUI.setFearFont();
				lowGUI.setRedColor();
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 23){
				Globals.lowerVoice = "You're scared";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 24){
				Globals.lowerVoice = "Doubt will tear you down";
				
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(100f);
			}
			
			if(myId == 25){
				Globals.lowerVoice = "Laughable";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(400f);
			}
			
			if(myId == 26){
				if(!startCutscene){
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent26", 0, 5.5f);
					startCutscene = true;
				}
			}
			
			if(myId == 27){
				Globals.lowerVoice = "Poor little insect";
				lowGUI.setFearFont();
				lowGUI.setRedColor();
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 28){
				Globals.upperVoice = "I couldn't let my doubts bring me down...";
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 29){
				Globals.upperVoice = "The pain was still very present ";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 30){
				Globals.upperVoice = "Like walking through needles";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 31){
				Globals.upperVoice = "I should have visited her grave but...";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				Globals.deleteLevel1();
				Globals.deleteLevel2();
			}
			
			if(myId == 32){
				Globals.upperVoice = "I'm not ready yet";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 33){
				Globals.lowerVoice = "Pain awaits you";
				lowGUI.setRedColor();
				lowGUI.setFearFont();
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 34){
				Globals.lowerVoice = "It is inevitable";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(200f);
			}
			
			if(myId == 35){
				lowerVoiceStyle.normal.textColor = Color.red;
				Globals.lowerVoice = "Step forward";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				platform.SetActive(true);
			}
			
			if(myId == 36){
				lowerVoiceStyle.normal.textColor = Color.red;
				Globals.lowerVoice = "Why keep going?";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 37){
				Globals.upperVoice = "But I still felt lost";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 38){
				Globals.upperVoice = "Until I met her";
				upGUI.setMyLeft(245f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				mng.playFriend();
			}
			
			if(myId == 39){
				Globals.upperVoice = "There was a girl";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 40){
				Globals.upperVoice = "She seemed free of worries";
				upGUI.setMyLeft(345f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				
			}
			
			if(myId == 41){
				if(!startCutscene){
					umbrellaGirl.GetComponent<UmbrellaGirl>().startAnim = true;
					Globals.upperVoice = "But I could see that she had lost someone too";
					startCutscene = true;
					upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				}
			}
			
			if(myId == 42){
				if(!startCutscene){
					Globals.upperVoice = "She was all alone in the rain";
					upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
					Vector3 pos = other.gameObject.transform.position;
					pos.y += 50;
					pos.x += 145;
					GameObject rain = Instantiate(rainPrefab,pos,Quaternion.identity) as GameObject;
					rain.name = "Rain";
					startCutscene = true;
				}
			}
			
			if(myId == 43){
				if(!startCutscene){
					Globals.upperVoice = "I wanted to help her but...";
					upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
					startCutscene = true;
				}
			}
			
			if(myId == 44){
				if(!startCutscene){
					Globals.upperVoice = "I didn't know how to reach her...";
					startCutscene = true;
					upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				}
			}
			
			if(myId == 45){
				if(!startCutscene){
					playerScript = other.gameObject.GetComponent<CharacterMovement>();
					playerScript.inCutscene = true;
					startCutscene = true;
					Globals.turnOffLight();
					myEvent = StoryEvent.ev0;
					Destroy(GameObject.Find("Rain"));
					InvokeRepeating("StoryEvent45", 0, 5.5f);
				}
			}
			
			if(myId == 46){
				if(!startCutscene){
					Globals.upperVoice = "I have to find a way";
					writeLowerVoice("Press E to float");
					lowGUI.setMotherFont();
					lowGUI.setYellowColor();
					startCutscene = true;
					upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
				}
			}
				
			if(myId == 47){
				Globals.lowerVoice = "Why would she talk to an insect?";
				lowGUI.setFearFont();
				lowGUI.setRedColor();
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 48){
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent48", 0, 5.5f);
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 49){
				Globals.upperVoice = "We no longer felt alone";
				upGUI.setMyLeft(350f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 50){
				Globals.upperVoice = "We did many things together";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 51){
				Globals.upperVoice = "And little by little";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
			
			if(myId == 52){
				Globals.upperVoice = "We started to accept our losses";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
				
			if(myId == 53){
				Globals.upperVoice = "But there was something...";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 54){
				Globals.upperVoice = "Something that I needed to do alone";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
			
			if(myId == 59){
				playerScript = other.gameObject.GetComponent<CharacterMovement>();
				playerScript.inCutscene = true;
				startCutscene = true;
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent59", 0, 5.5f);
				Globals.turnOffLight();
				Globals.waitTime = 7;
				Globals.loadNextLevelTutorial();
			}
				
			if(myId == 60){
				GameObject cam = GameObject.Find("Final_Camera");
				cam.GetComponent<Final_Camera>().unparent();
				Globals.upperVoice = "That's what mom would've wanted";
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				writeLowerVoice("Jump");
				lowGUI.setMyLeft(400f);
				lowGUI.setYellowColor();
				lowGUI.setMotherFont();
			}
				
			if(myId == 61){
				playerScript = other.gameObject.GetComponent<CharacterMovement>();
				playerScript.inCutscene = true;
				startCutscene = true;
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent61", 0, 5.5f);
				Globals.turnOffLight();
				Globals.waitTime = 7;
				Globals.loadNextLevelTutorial();
			}
				
			if(myId == 62){
				Globals.upperVoice = "Though there are times where I doubt myself";
				lowGUI.setRedColor();
				lowGUI.setFearFont();
				writeLowerVoice("You're an insect, less than human");
				
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
				
			if(myId == 63){
				Globals.upperVoice = "No matter how impossible things might be";
				lowGUI.setRedColor();
				lowGUI.setFearFont();
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 64){
				Globals.upperVoice = "I won't give up";
				writeLowerVoice("Dead end");
				lowGUI.setRedColor();
				lowGUI.setFearFont();
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
				
			if(myId == 65){
				myEvent = StoryEvent.ev0;
				InvokeRepeating("StoryEvent65", 0, 5.5f);
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				lowGUI.setMyLeft(200f);
			}
		}
		
		if(!not)
		startCutscene = true;
			
	 }		
}
	void StoryEvent7(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I always wondered about my future";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(300f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "How life would be after I leave";
			
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(355f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "But didn't want to leave my home or my mother";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I just wanted to stay and be happy";
			
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "But I knew that someday, everything would change";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(55f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(355f);
		}
		
		else if(myEvent == StoryEvent.ev5){
			Globals.upperVoice = "But not like that... never like that";
			myEvent = StoryEvent.ev6;
			
			upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
	}
	
	void StoryEvent9(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "But later that night";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "the unimaginable happened";
			
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
	}
	
	void StoryEvent10(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I heard a loud noise downstairs";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "I feared the worst";
			//Globals.lowerVoice = "Keep going...";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "I felt as if the walls were closing in on me";
			//Globals.lowerVoice = "let your light shine.";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "Every step was harder than the last";
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "I needed to hurry";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
	}
	
	void StoryEvent12(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I knew this would be the last moment";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			//Globals.upperVoice = "I feared the worst";
			Globals.lowerVoice = "Don't be afraid";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			//Globals.upperVoice = "I felt as if the walls were closing in on me";
			Globals.lowerVoice = "It's all going to be all right";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			//Globals.upperVoice = "Every step was harder than the last";
			Globals.lowerVoice = "I love you so much";
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.lowerVoice = "Always...follow your heart...";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
	}
	
		void StoryEvent14(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I was scared";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			//Globals.upperVoice = "I feared the worst";
			Globals.upperVoice = "I didn't even know where I was running off to";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
			
			
		}
		
		else if(myEvent == StoryEvent.ev2){
			//Globals.upperVoice = "I felt as if the walls were closing in on me";
			Globals.upperVoice = "but it was all I could do";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			//Globals.upperVoice = "Every step was harder than the last";
			//Globals.lowerVoice = "I felt as if my whole world was collapsing";
			Globals.upperVoice = "I had no one else";
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			//Globals.upperVoice = "Every step was harder than the last";
			//Globals.lowerVoice = "I felt as if my whole world was collapsing";
			Globals.upperVoice = "I could feel my world collapsing";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		
	}
	
	void StoryEvent17(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I was in a dark place in my life";
			myEvent = StoryEvent.ev1;
			
			upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "I couldn't find the strength to get up";
			//Globals.lowerVoice = "Why do we fall?";
			writeLowerVoice("Why do we fall?");
			myEvent = StoryEvent.ev2;
			
			upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Instantiate(lanternPrefab);
			Globals.upperVoice = "Until I found my mother's lantern";
			myEvent = StoryEvent.ev3;
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I knew she wouldn't want me to just give up";
			playerScript.inCutscene = false;
			myEvent = StoryEvent.ev4;
			upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		
		
		/*else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "I have to keep going...";
			myEvent = StoryEvent.ev5;
		}*/
	}
	
	void StoryEvent26(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "This place reminds me of that lake back home";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "We would always go there and throw rocks...";
			writeLowerVoice("Throw a rock with F");
			lowGUI.setMotherFont();
			lowGUI.setYellowColor();
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "This always made me feel better";
			writeLowerVoice("Don't let fears consume you");
			myEvent = StoryEvent.ev3;
			upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(100f);
		}
		
		/*else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I should have done something...";
			Globals.lowerVoice = "I love you so much...";
			myEvent = StoryEvent.ev4;
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "But I just stood there...";
			Globals.lowerVoice = "Goodbye";
			myEvent = StoryEvent.ev5;
		} */
	}
	
	void StoryEvent45(){
		if(myEvent == StoryEvent.ev0){
			Globals.lowerVoice = "Thank you so much.";
			lowGUI.setMotherFont();
			lowGUI.setYellowColor();
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(300f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.lowerVoice = "If you hadn't come, I...";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.lowerVoice = "I won't forget this kindness";
			myEvent = StoryEvent.ev3;
			upGUI.setMyLeft(400f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(100f);
			umbrellaGirl.GetComponent<UmbrellaGirl>().goLeft();
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.lowerVoice = "I will repay you somehow";
			myEvent = StoryEvent.ev4;
			upGUI.setMyLeft(400f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(100f);
			umbrellaGirl.GetComponent<UmbrellaGirl>().goLeft();
		}
		
		else if(myEvent == StoryEvent.ev4){
			this.playerScript.inCutscene = false;
			Globals.turnOnLight();
		}
		
	}
	
	void StoryEvent48(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "And helped she did";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			//Globals.upperVoice = "I feared the worst";
			Globals.upperVoice = "She would appear and help me find my way";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			//Globals.upperVoice = "I felt as if the walls were closing in on me";
			Globals.upperVoice = "Our friendship grew";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
	}
	
	void StoryEvent59(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I'm almost there";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			//Globals.upperVoice = "I feared the worst";
			Globals.upperVoice = "It's been a long journey";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
	}
	
	void StoryEvent61(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "It's ok mom, I did what you said";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			//Globals.upperVoice = "I feared the worst";
			Globals.upperVoice = "I followed my heart";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
	}
	
	void StoryEvent65(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "I refuse to go back to that haze of darkness";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(100f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "I don't care how much pain it might bring";
			
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "How many tries I have to make";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I won't go back to being an insect";
			
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "I want to be happy";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(400f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(355f);
		}
		
		else if(myEvent == StoryEvent.ev5){
			Globals.upperVoice = "Like when I helped her or just being with her";
			myEvent = StoryEvent.ev6;
			
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
		
		else if(myEvent == StoryEvent.ev6){
			Globals.upperVoice = "I don't want my mom to worry anymore";
			myEvent = StoryEvent.ev7;
			
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
	}
	
	
	
	
	
}
