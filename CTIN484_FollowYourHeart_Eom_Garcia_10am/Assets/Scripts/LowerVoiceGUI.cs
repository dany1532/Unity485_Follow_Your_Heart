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
	public GUIStyle myStyle;				// GUI style for text font color, size, etc.
	public float fadeDuration = 1;			// The amount of time it takes to fade text in and out
	public float freezeDuration = 3;		// The amount of time that text is shown at alpha = 1
	float alpha;							// text color alpha in RGBA
	float previousTime;						// last recorded time, to measure delta time
	
	public enum states { fadeIn, fadeOut, freezeText, doNothing };
	public states state;
	
	void Start ()
	{
		state = states.doNothing;
		alpha = 0f;
	}
	
	void Update()
	{
		if(state == states.doNothing)
		{
			if (Globals.lowerVoice != "")
			{
				state = states.fadeIn;
				previousTime = Time.time;
			}
		}
		else if(state == states.fadeIn)
		{
			float deltaTime = Time.time - previousTime;
			alpha += deltaTime / fadeDuration;					// decrease text color alpha
			previousTime = Time.time;
			if(alpha >= 1)
			{
				alpha = 1;
				state = states.freezeText;
			}
		}
		else if(state == states.freezeText)
		{
			if(Time.time > previousTime + freezeDuration)
			{
				previousTime = Time.time;
				state = states.fadeOut;
			}
		}
		else if(state == states.fadeOut)
		{
			float deltaTime = Time.time - previousTime;			// increase text color alpha
			alpha -= deltaTime / fadeDuration;
			previousTime = Time.time;
			if(alpha <= 0)
			{
				alpha = 0;
				state = states.doNothing;
				Globals.lowerVoice = "";
			}
		}
	}
	/*string lowerVoiceTransition;
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
	} */
	
	void OnGUI()
	{
		//GUI.Label(new Rect(10, 550, 100, 570),lowerVoiceTransition , myStyle);
		//myStyle.normal.textColor = new Color(1f, 1f, 1f, alpha);
		float myR = myStyle.normal.textColor.r;
		float myG = myStyle.normal.textColor.g;
		float myB = myStyle.normal.textColor.b;
		
		myStyle.normal.textColor = new Color(myR, myG, myB, alpha);
		
		GUI.Label(new Rect(10, 550, 100, 570), Globals.lowerVoice, myStyle);
	}
}
