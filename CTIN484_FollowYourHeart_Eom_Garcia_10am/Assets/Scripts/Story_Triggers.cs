using UnityEngine;
using System.Collections;

public class Story_Triggers : MonoBehaviour {
	TextMesh textMeshChild;
	int myId;
	
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
				Globals.upperVoice = "I remember that I was going to my home...";
			}
			
			if(myId == 8){
				Globals.upperVoice = "I remember that I was going to my home...";
			}
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
