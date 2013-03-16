using UnityEngine;
using System.Collections;

public class Music_Manager : MonoBehaviour {
	public AudioClip village;
	public AudioClip motherDeath;
	public AudioClip darkness;
	public AudioClip newFriend;
	private AudioSource mySource;
	
	
	public void Awake(){
		mySource = this.GetComponent<AudioSource>();
		DontDestroyOnLoad(this);	
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
		mySource.Stop();
		mySource.clip = newFriend;
		mySource.Play();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
