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
	
	void Start ()
	{
		upperVoiceTransition = "";
		transition = false;
	}
	
	void Update ()
	{
		if (!transition)																// If no text is being processed
		{
			if(Globals.upperVoice != upperVoiceTransition)								// Check to see if there is new text to process
			{
				transition = true;
				charIndex = 0;
				nextChar = Time.time + Globals.textSpeed;
			}
		}
		else
		{																				// If text is being processed
			if(Time.time > nextChar)													// At some time delta, add a new char to text
			{
				upperVoiceTransition += Globals.upperVoice[charIndex].ToString();
				nextChar = Time.time + Globals.textSpeed;
				charIndex++;
				if(charIndex >= Globals.upperVoice.Length)
				{
					transition = false;
				}
			}
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 20),upperVoiceTransition , myStyle);
	}
}