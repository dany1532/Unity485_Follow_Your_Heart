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
	public GUIStyle myStyle;				// GUI style for text font color, size, etc.
	public float fadeDuration = 1;			// The amount of time it takes to fade text in and out
	public float freezeDuration = 3;		// The amount of time that text is shown at alpha = 1
	float alpha;							// text color alpha in RGBA
	float previousTime;						// last recorded time, to measure delta time
	private float myLeft = 10;
	private float myTop = 10;
	
	
	public enum states { fadeIn, fadeOut, freezeText, doNothing };
	public states state;
	
	void Start ()
	{
		state = states.doNothing;
		alpha = 0f;
	}
	
	public void setMyLeft(float newLeft){
		myLeft = newLeft;	
	}
	
	public void setMyTop(float newTop){
		myTop = newTop;	
	}
	
	void Update()
	{
		if(state == states.doNothing)
		{
			if (Globals.upperVoice != "")
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
				Globals.upperVoice = "";
			}
		}
	}
	
	void OnGUI()
	{
		float myR = myStyle.normal.textColor.r;
		float myG = myStyle.normal.textColor.g;
		float myB = myStyle.normal.textColor.b;
		
		myStyle.normal.textColor = new Color(myR, myG, myB, alpha);
		
		GUI.Label(new Rect(myLeft, myTop, 100, 20), Globals.upperVoice, myStyle);
	}
}