using UnityEngine;
using System.Collections;

public class Story_Triggers : MonoBehaviour {
	TextMesh textMeshChild;
	int myId;
	public enum StoryEvent{ev0,ev1, ev2, ev3, ev4, ev5, ev6,ev7, do_nothing};
	public StoryEvent myEvent;
	public bool startCutscene = false;
	
	void Start(){
		Transform f = transform.FindChild("Trigger_Location");
		textMeshChild = f.GetComponent<TextMesh>();
		myId = int.Parse(textMeshChild.text);
		MeshRenderer rend = textMeshChild.GetComponent<MeshRenderer>();
		rend.enabled = false;
		
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
				}
			}
			
			if(myId == 9){
				other.gameObject.GetComponent<CharacterMovement>().inCutscene = true;
				Globals.loadNextLevelTutorial();
			}
			
			if(myId == 10){
				if(!startCutscene){
					myEvent = StoryEvent.ev0;
					InvokeRepeating("StoryEvent10", 0, 5.5f);
					startCutscene = true;
					Globals.deathMother = true;
				}
			}
	 }		
}
	
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
