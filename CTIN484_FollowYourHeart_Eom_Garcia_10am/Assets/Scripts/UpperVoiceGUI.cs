/*
 * Things to implement:
 * - Customize font and guiText position to game screen resolution
 * 
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class UpperVoiceGUI : MonoBehaviour
{
	string upperVoiceTransition;
	public GUIStyle myStyle;
	
	float nextChar;
	int charIndex;
	bool transition;
	
	public enum states {beginning, appearTransition, disappearTransition, do_nothing};
	public states myState = states.do_nothing;
	
	void Start ()
	{
		upperVoiceTransition = "";
		//transition = false;
		myState = states.beginning;
	}
	
	void Update ()
	{
		//if (!transition)
																		// If no text is being processed
		if(myState == states.beginning)
		{
			if(Globals.upperVoice != upperVoiceTransition)								// Check to see if there is new text to process
			{
				//transition = true;
				myState = states.appearTransition;
				charIndex = 0;
				nextChar = Time.time + Globals.textSpeed;
			}
		}
		//else
		if(myState == states.appearTransition)
		{																				// If text is being processed
			if(Time.time > nextChar)													// At some time delta, add a new char to text
			{
				upperVoiceTransition += Globals.upperVoice[charIndex].ToString();
				nextChar = Time.time + Globals.textSpeed;
				charIndex++;
				if(charIndex >= Globals.upperVoice.Length)
				{
					//transition = false;
					myState = states.do_nothing;
					Invoke("Dissapear", 2);
				}
			}
		}
		
		if(myState == states.disappearTransition)
		{																				// If text is being processed
			if(Time.time > nextChar)													// At some time delta, add a new char to text
			{
				Globals.upperVoice.Remove(charIndex,1);
				Globals.upperVoice.Insert(charIndex,"0");
				
				upperVoiceTransition += Globals.upperVoice[charIndex].ToString();
				
				nextChar = Time.time + Globals.textSpeed;
				charIndex++;
				if(charIndex >= Globals.upperVoice.Length)
				{
					//transition = false;
					myState = states.do_nothing;
					//Invoke("Dissapear", 2);
				}
			}
		}
	}
	
	void Dissapear(){
		charIndex = 0;
		upperVoiceTransition = "";
		myState = states.disappearTransition;	
		
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20),upperVoiceTransition , myStyle);
	}
}