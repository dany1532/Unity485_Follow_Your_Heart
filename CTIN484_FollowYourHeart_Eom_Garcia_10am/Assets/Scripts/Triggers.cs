/*
 * Temporary script!!
 * 
 * 
 * 
 * 
*/




using UnityEngine;
using System.Collections;

public class Triggers : MonoBehaviour
{
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "TempTriggerObject2")
		{
			Globals.lowerVoice = "Hello world.";
		}
		if(other.gameObject.name == "TempTriggerObject1")
			Globals.upperVoice = "Goodbye world.";
		
		//Story Events
		
		if(other.gameObject.name == "StoryTrigger1")
		{
			Globals.upperVoice = "I know I can move with A-D Keys...";
		}
		
		if(other.gameObject.name == "StoryTrigger2")
		{
			Globals.upperVoice = "I shouldn't have a problem with my SPACE Jump";
		}
		
		if(other.gameObject.name == "StoryTrigger3")
		{
			Globals.upperVoice = "As I progress, I begin to wonder...";
		}
		if(other.gameObject.name == "StoryTrigger4")
		{
			Globals.upperVoice = "Maybe I shouldn't have left...";
		}
		
		if(other.gameObject.name == "StoryTrigger5")
		{
			Globals.upperVoice = "What if I don't find anything...";
			Invoke("lowerVoiceStory5",2);
		}
	}
	
	void lowerVoiceStory5(){
		Globals.lowerVoice = "Believe..";	
	}
}
