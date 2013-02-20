/*
 * Things to implement:
 * - Customize font and guiText position to game screen resolution
 * - After a certain time, disappear text
 * 
 * 
*/

using UnityEngine;
using System.Collections;

public class LowerVoiceGUI : MonoBehaviour
{
	string lowerVoiceTransition;
	public GUIStyle myStyle;
	
	float nextChar;
	int charIndex;
	bool transition;
	
	void Start ()
	{
		lowerVoiceTransition = "";
		transition = false;
	}
	
	void Update ()
	{
		if (!transition)																// If no text is being processed
		{
			if(Globals.lowerVoice != lowerVoiceTransition)								// Check to see if there is new text to process
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
				lowerVoiceTransition += Globals.lowerVoice[charIndex].ToString();
				nextChar = Time.time + Globals.textSpeed;
				charIndex++;
				if(charIndex >= Globals.lowerVoice.Length)
				{
					transition = false;
				}
			}
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 550, 100, 570),lowerVoiceTransition , myStyle);
	}
}
