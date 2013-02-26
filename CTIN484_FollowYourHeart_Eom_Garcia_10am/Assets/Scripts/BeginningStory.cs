using UnityEngine;
using System.Collections;

public class BeginningStory : MonoBehaviour {
	public enum StoryEvent{ev0,ev1, ev2, ev3, ev4, ev5, ev6,ev7, do_nothing};
	public StoryEvent myEvent;
	public int nextEventSeconds = 3;
	public Font myFont;
	
	// Use this for initialization
	void Start () {
		myEvent = StoryEvent.ev0;
		//DontDestroyOnLoad (transform.gameObject);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(myEvent == StoryEvent.ev0){
			Globals.upperVoice = "Somewhere...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event1",nextEventSeconds);
		}
		
		else if(myEvent == StoryEvent.ev1){
			Globals.upperVoice = "\n\n\n             Somehow...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event2", nextEventSeconds);
		}
		
		else if(myEvent == StoryEvent.ev2){
			Globals.upperVoice = "\n\n\n\n                      Something went wrong...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event3", nextEventSeconds);
		}
		else if(myEvent == StoryEvent.ev3){
			Globals.upperVoice = "\n\n        My Life isn't the way it was supposed to be...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event4", nextEventSeconds);
		}
		else if(myEvent == StoryEvent.ev4){
			Globals.upperVoice = "\n\n\n                 I was supposed to do something...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event5", nextEventSeconds);
		}
		else if(myEvent == StoryEvent.ev5){
			Globals.upperVoice = "\n\n\n\n                 I was supposed to be someone...";
			myEvent = StoryEvent.do_nothing;
			Invoke("event6", nextEventSeconds);
		}
		else if(myEvent == StoryEvent.ev6){
			GameObject g = GameObject.Find("Environment_Scripts");
			GUIStyle style = g.GetComponent<UpperVoiceGUI>().myStyle;
			g.GetComponent<UpperVoiceGUI>().freezeDuration = 4f;
			style.font = myFont;
			style.fontSize = 100;
			
			Globals.upperVoice = "\n\n                  Follow Your Heart";
			myEvent = StoryEvent.do_nothing;
			Invoke("event7", nextEventSeconds + 4);
		}
		
	}
	
	private void event1(){
		myEvent = StoryEvent.ev1;	
	}
	
	private void event2(){
		myEvent = StoryEvent.ev2;	
	}
	
	private void event3(){
		myEvent = StoryEvent.ev3;	
	}
	
	private void event4(){
		myEvent = StoryEvent.ev4;	
	}
	
	private void event5(){
		myEvent = StoryEvent.ev5;	
	}
	private void event6(){
		myEvent = StoryEvent.ev6;	
	}
	private void event7(){
		//GameObject g = GameObject.Find("Environment_Scripts");
		//Destroy(g);
		Application.LoadLevel(1);	
	}
	

}
