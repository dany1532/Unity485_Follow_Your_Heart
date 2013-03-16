using UnityEngine;
using System.Collections;

public class EndStory : MonoBehaviour {
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
			Globals.upperVoice = "\n\n\n                              Thanks For Playing";
			myEvent = StoryEvent.do_nothing;
			
		}
	}
}
