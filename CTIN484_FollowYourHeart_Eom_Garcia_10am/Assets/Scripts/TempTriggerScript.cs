using UnityEngine;
using System.Collections;

public class TempTriggerScript : MonoBehaviour
{
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "TempTriggerObject2")
		{
			Globals.lowerVoice = "Hello world.";
		}
		if(other.gameObject.name == "TempTriggerObject1")
		{
			Globals.upperVoice = "Goodbye world.";
		}
	}
}
