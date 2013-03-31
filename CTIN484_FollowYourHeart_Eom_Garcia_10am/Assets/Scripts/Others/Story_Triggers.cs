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
	public GameObject platform;
	public GameObject umbrellaGirl;
	private Music_Manager mng;
	
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
	
	void OnTriggerEnter(Collider other){
	//Story Events
	if(other.gameObject.name == "_Hero"){
		if(!startCutscene){
			if(myId == 1){
				Globals.upperVoice = "I remember that I was going to my home...";
				Globals.lowerVoice = "Move with A-D";
				upGUI.setMyLeft(155f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 2){
				Globals.upperVoice = "My mom was waiting for me...";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
			}
			
			if(myId == 3){
				Globals.upperVoice = "She is my only family...";	
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
			}
			
			if(myId == 4){
				Globals.upperVoice = "She has taught me everything I know";
				Globals.lowerVoice = "Jump with Space";
				
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 5){
				Globals.upperVoice = "But I haven't done anything with my life...";
				Globals.lowerVoice = "I'm so proud of you.";
				
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 6){
				Globals.upperVoice = "She seems weaker every day...";
				Globals.lowerVoice = "I'm fine, dear.";
				
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 7){
				Globals.lowerVoice = "Climb with W and get me my pills, will you dear?";
				lowGUI.setMyLeft(15f);
			}
			
			if(myId == 8){
				bool hasPills = other.gameObject.GetComponent<CharacterMovement>().hasPills;
				if(hasPills){
					Globals.lowerVoice = "Thank you dear, now go to rest...";
					lowGUI.setMyLeft(155f);
					other.gameObject.GetComponent<CharacterMovement>().nextLevel = true;
				}
				
			}
			
			if(myId == 9){
				bool nextLevel = other.gameObject.GetComponent<CharacterMovement>().nextLevel;
				if(nextLevel && !startCutscene){
					other.gameObject.GetComponent<CharacterMovement>().inCutscene = true;
					startCutscene = true;
					mng.playDeath();
					Globals.loadNextLevelTutorial();
				}
			}
			
			if(myId == 10){
				if(!startCutscene){
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent10", 0, 5.5f);
					startCutscene = true;
					Globals.deathMother = true;
				}
			}
			
			if(myId == 11){
				if(!startCutscene){
					Vector3 pos = other.gameObject.transform.position;
					pos.y += 50;
					pos.x -= 165;
					Instantiate(rainPrefab,pos,Quaternion.identity);
				}
				Globals.upperVoice = "I was scared, so I ran...";
				startCutscene = true;
				upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 12){
				Globals.upperVoice = "I didn't know where I was going...";
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 13){
				Globals.upperVoice = "I just couldn't think...";
				upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 14){
				Globals.upperVoice = "I felt as if the world was collapsing around me...";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
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
					GameObject.Find("TutorialLight").light.intensity = 0;
					mng.playDarkness();
					Globals.loadNextLevelTutorial();
				}
			}
			
			if(myId == 17){
				if(!startCutscene){
					playerScript = other.gameObject.GetComponent<CharacterMovement>();
					playerScript.inCutscene = true;
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent17", 0, 5.5f);
					startCutscene = true;
				}
			}
			
			if(myId == 18){
				Globals.upperVoice = "This lantern is all I have left of her...";
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 19){
				Globals.upperVoice = "That and what she has taught me...";
				upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 20){
				Globals.upperVoice = "But this world is different...";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 21){
				Globals.upperVoice = "It feels as if it wants me to fail..";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 22){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "You will fail";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 23){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "You're nothing";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 24){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "What do you even hope to accomplish?";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(100f);
			}
			
			if(myId == 25){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "Pitiful";
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
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "You will never succeed...";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 28){
				Globals.upperVoice = "I can't let my doubts bring me down...";
				upGUI.setMyLeft(255f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 29){
				Globals.upperVoice = "I haven't visited her grave, nor shed a single tear";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 30){
				Globals.upperVoice = "I'm still not strong enough...";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 31){
				Globals.upperVoice = "I want her to be proud of me...";
				upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				Globals.deleteLevel1();
				Globals.deleteLevel2();
			}
			
			if(myId == 32){
				Globals.upperVoice = "But it still hurts...";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 33){
				lowerVoiceStyle.normal.textColor = Color.red;
				Globals.lowerVoice = "Pain awaits you";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 34){
				lowerVoiceStyle.normal.textColor = Color.red;
				Globals.lowerVoice = "Turn back and you won't suffer";
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
				Globals.lowerVoice = "Why hurt yourself?";
				upGUI.setMyLeft(100f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 37){
				Globals.upperVoice = "Though it hurts, I can't just give up...";
				upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 38){
				Globals.upperVoice = "I'm not the only one who is in pain...";
				upGUI.setMyLeft(245f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				mng.playFriend();
			}
			
			if(myId == 39){
				Globals.upperVoice = "There is a girl";
				upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
			}
			
			if(myId == 40){
				Globals.upperVoice = "She seems free of worries";
				upGUI.setMyLeft(345f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				
			}
			
			if(myId == 41){
				if(!startCutscene){
					umbrellaGirl.GetComponent<UmbrellaGirl>().startAnim = true;
					Globals.upperVoice = "But I can see that she too lost someone";
					startCutscene = true;
					upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
				}
			}
			
			if(myId == 42){
				if(!startCutscene){
					Globals.upperVoice = "She is alone in the rain...";
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
					Globals.upperVoice = "I want to help her, but I don't know how...";
					upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
					startCutscene = true;
				}
			}
			
			if(myId == 44){
				if(!startCutscene){
					Globals.upperVoice = "Is there anything I can do?";
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
					Globals.upperVoice = "Perhaps I can use this...";
					Globals.lowerVoice = "Press E to float";
					startCutscene = true;
					upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
				}
			}
		}
		
		startCutscene = true;
			
	 }		
}
	
  //Mother's death cutscene
	void StoryEvent10(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "But the next day...";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "She was gone...";
			Globals.lowerVoice = "Keep going...";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(455f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "I watched her as she died";
			Globals.lowerVoice = "let your light shine.";
			myEvent = StoryEvent.ev3;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I should have done something...";
			Globals.lowerVoice = "I love you so much...";
			myEvent = StoryEvent.ev4;
			
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "But I just stood there...";
			Globals.lowerVoice = "Goodbye";
			myEvent = StoryEvent.ev5;
			
			upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(355f);
		}
	}
	
	void StoryEvent17(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "It hurts so much...";
			myEvent = StoryEvent.ev1;
			
			upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "Maybe if I just lie here...";
			Globals.lowerVoice = "Why do we fall?";
			myEvent = StoryEvent.ev2;
			
			upGUI.setMyLeft(400f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Instantiate(lanternPrefab);
			Globals.upperVoice = "Is that... my mother's lantern?";
			myEvent = StoryEvent.ev3;
			upGUI.setMyLeft(355f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "No, she wouldn't want me to just give up...";
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
			Globals.upperVoice = "This place reminds me of that lake...";
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(300f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.upperVoice = "We would always go there and throw rocks...";
			Globals.lowerVoice = "Throw a rock with F";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
				upGUI.setMyTop(100f);
				
				lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.upperVoice = "She would always tell me...";
			Globals.lowerVoice = "Throw one whenever you are in doubt";
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
			myEvent = StoryEvent.ev1;
			upGUI.setMyLeft(300f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}	
		
		else if(myEvent == StoryEvent.ev1){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.lowerVoice = "If you hadn't come, I...";
			myEvent = StoryEvent.ev2;
			upGUI.setMyLeft(200f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(255f);
		}
		
		else if(myEvent == StoryEvent.ev2){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.lowerVoice = "I won't forget this kindness";
			myEvent = StoryEvent.ev3;
			upGUI.setMyLeft(400f);
			upGUI.setMyTop(100f);
			lowGUI.setMyLeft(100f);
			umbrellaGirl.GetComponent<UmbrellaGirl>().goLeft();
		}
		
		else if(myEvent == StoryEvent.ev3){
			lowerVoiceStyle.normal.textColor = Color.gray;
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
	
	
}
