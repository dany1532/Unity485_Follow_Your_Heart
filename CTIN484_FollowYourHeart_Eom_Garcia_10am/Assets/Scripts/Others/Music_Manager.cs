using UnityEngine;
using System.Collections;

public class Music_Manager : MonoBehaviour {
	public AudioClip firstScene;
	public AudioClip village;
	public AudioClip motherDeath;
	public AudioClip darkness;
	public AudioClip newFriend;
	private float lowerSpeed = 30f;
	private float volume;
	private bool shouldLowerVol = false;
	private AudioSource mySource;
	
	
	public void Awake(){
		mySource = this.GetComponent<AudioSource>();
		//DontDestroyOnLoad(this);	
	}
	
	public void playHome(){
		mySource.Stop();
		mySource.clip = village;
		mySource.Play();
	}
	
	public void playDeath(){
		mySource.Stop();
		mySource.clip = motherDeath;
		mySource.Play();
	}
	
	public void playDarkness(){
		mySource.Stop();
		mySource.clip = darkness;
		mySource.Play();
	}
	
	public void playFriend(){
		shouldLowerVol = false;
		mySource.volume = 0.2f;
		mySource.Stop();
		mySource.clip = newFriend;
		mySource.Play();
	}
	
	public void lowerMusic(){
		shouldLowerVol = true;
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	public void DontDestroy(){
		DontDestroyOnLoad(this);
	}
	
	public void destroyObject(){
		GameObject.DestroyObject(this);
	}
	
	// Update is called once per frame
	void Update () {
		if(shouldLowerVol){
			volume = mySource.volume - Time.deltaTime/lowerSpeed;
			mySource.volume = volume;
			if(volume <= 0)
				shouldLowerVol = false;
		}
			
	}
}
