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
		
		if(other.gameObject.name == "Death_Fall"){
			Vector3 loc = Globals.currentCheckPoint.transform.position;
			loc.z = 1.25f;
			this.transform.position = loc;
		}
		
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
			Globals.upperVoice = "Nevertheless, these are all things I've done before...";
		}
		if(other.gameObject.name == "StoryTrigger4")
		{
			Globals.upperVoice = "Anyone can do them...";
		}
		
		if(other.gameObject.name == "StoryTrigger5")
		{
			Globals.upperVoice = "A ladder, maybe I can climb it with W";
		}
		
		if(other.gameObject.name == "StoryTrigger6")
		{
			Globals.upperVoice = "As I begin my climb, I start to wonder...";
		}
		
		if(other.gameObject.name == "StoryTrigger7")
		{
			Globals.upperVoice = "Am I doing the right thing?";
		}
		
		if(other.gameObject.name == "StoryTrigger8")
		{
			Globals.upperVoice = "What if I don't find the answer I'm looking for...";
			Invoke("lowerVoiceStory8",2.4f);
		}
		
		if(other.gameObject.name == "StoryTrigger9")
		{
			Globals.upperVoice = "Almost there...";
		}
		
		if(other.gameObject.name == "StoryTrigger10")
		{
			Globals.upperVoice = "This torch, it makes me feel safe...";
		}
	}
	
	void lowerVoiceStory8(){
		Globals.lowerVoice = "Believe..";	
	}
}
