using UnityEngine;
using System.Collections;

public class Story_Triggers : MonoBehaviour {
	TextMesh textMeshChild;
	int myId;
	private enum StoryEvent{ev0,ev1, ev2, ev3, ev4, ev5, ev6,ev7, do_nothing};
	private StoryEvent myEvent;
	private bool startCutscene = false;
	public GameObject rainPrefab;
	public GameObject lanternPrefab;
	private CharacterMovement playerScript;
	private GUIStyle lowerVoiceStyle;
	
	void Start(){
		Transform f = transform.FindChild("Trigger_Location");
		textMeshChild = f.GetComponent<TextMesh>();
		myId = int.Parse(textMeshChild.text);
		MeshRenderer rend = textMeshChild.GetComponent<MeshRenderer>();
		rend.enabled = false;
		
		lowerVoiceStyle =GameObject.Find("Environment_Scripts").
								GetComponent<LowerVoiceGUI>().myStyle;
		
	}
	
	void OnTriggerEnter(Collider other){
	//Story Events
	if(other.gameObject.tag == "Player"){
			if(myId == 1){
				Globals.upperVoice = "I remember that I was going to my home...";
				Globals.lowerVoice = "Move with A-D";	
			}
			
			if(myId == 2){
				Globals.upperVoice = "My mom was waiting for me...";
			}
			
			if(myId == 3){
				Globals.upperVoice = "I live alone with her...";	
			}
			
			if(myId == 4){
				Globals.upperVoice = "She has taught me everything I know";
				Globals.lowerVoice = "Jump with Space";
			}
			
			if(myId == 5){
				Globals.upperVoice = "But I haven't done anything with my life...";
				Globals.lowerVoice = "I'm so proud of you.";
			}
			
			if(myId == 6){
				Globals.upperVoice = "She seems weaker every day...";
				Globals.lowerVoice = "I'm fine, dear.";
			}
			
			if(myId == 7){
				Globals.lowerVoice = "Climb with W and get me my pills, will you dear?";
			}
			
			if(myId == 8){
				bool hasPills = other.gameObject.GetComponent<CharacterMovement>().hasPills;
				if(hasPills){
					Globals.lowerVoice = "Thank you dear, now go to rest...";
					other.gameObject.GetComponent<CharacterMovement>().nextLevel = true;
				}
				
			}
			
			if(myId == 9){
				bool nextLevel = other.gameObject.GetComponent<CharacterMovement>().nextLevel;
				if(nextLevel && !startCutscene){
					other.gameObject.GetComponent<CharacterMovement>().inCutscene = true;
					startCutscene = true;
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
			}
			
			if(myId == 12){
				Globals.upperVoice = "I didn't know where I was going...";
			}
			
			if(myId == 13){
				Globals.upperVoice = "I just couldn't think...";
			}
			
			if(myId == 14){
				Globals.upperVoice = "I felt as if the world was collapsing around me...";
			}
			
			if(myId == 15){
				Globals.upperVoice = "Until finally...";
			}
			
			if(myId == 16){
				if(!startCutscene){
					startCutscene = true;
					Globals.upperVoice = "I just gave up...";
					GameObject.Find("TutorialLight").light.intensity = 0;
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
			}
			
			if(myId == 19){
				Globals.upperVoice = "That and what she has taught me...";
			}
			
			if(myId == 20){
				Globals.upperVoice = "But this world is different...";
			}
			
			if(myId == 21){
				Globals.upperVoice = "It feels as if it wants me to fail..";
			}
			
			if(myId == 22){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "You will fail";
			}
			
			if(myId == 23){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "You're nothing";
			}
			
			if(myId == 24){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "What do you even hope to accomplish?";
			}
			
			if(myId == 25){
				lowerVoiceStyle.normal.textColor = Color.magenta;
				Globals.lowerVoice = "Pitiful";
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
			}
	 }		
}
	
  //Mother's death cutscene
	void StoryEvent10(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "But the next day...";
			myEvent = StoryEvent.ev1;
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "She was gone...";
			Globals.lowerVoice = "Keep going...";
			myEvent = StoryEvent.ev2;
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "I watched her as she died";
			Globals.lowerVoice = "let your light shine.";
			myEvent = StoryEvent.ev3;
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "I should have done something...";
			Globals.lowerVoice = "I love you so much...";
			myEvent = StoryEvent.ev4;
		}
		
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "But I just stood there...";
			Globals.lowerVoice = "Goodbye";
			myEvent = StoryEvent.ev5;
		}
	}
	
	void StoryEvent17(){
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "It hurts so much...";
			myEvent = StoryEvent.ev1;
		}	
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "Maybe if I just lie here...";
			Globals.lowerVoice = "Why do we fall?";
			myEvent = StoryEvent.ev2;
		}
		
		else if(myEvent == StoryEvent.ev2){
			Instantiate(lanternPrefab);
			Globals.upperVoice = "Is that... my mother's lantern?";
			myEvent = StoryEvent.ev3;
		}
		
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "No, she wouldn't want me to just give up...";
			playerScript.inCutscene = false;
			myEvent = StoryEvent.ev4;
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
		}	
		
		else if(myEvent == StoryEvent.ev1){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.upperVoice = "We would always go there and throw rocks...";
			Globals.lowerVoice = "Throw a rock with F";
			myEvent = StoryEvent.ev2;
		}
		
		else if(myEvent == StoryEvent.ev2){
			lowerVoiceStyle.normal.textColor = Color.gray;
			Globals.upperVoice = "She would always tell me...";
			Globals.lowerVoice = "Throw one whenever you are in doubt";
			myEvent = StoryEvent.ev3;
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
	
	void lowerVoiceStory8(){
		Globals.lowerVoice = "Believe..";	
	}
	
	void lowerVoiceStory13(){
		Globals.lowerVoice = "Whatever doesn't kill us...";	
	}
	
	void lowerVoiceStory14(){
		Globals.lowerVoice = "For now...";	
	}
	void lowerVoiceStory16(){
		Globals.lowerVoice = "Shadows of Doubt";	
	}	
	
}
