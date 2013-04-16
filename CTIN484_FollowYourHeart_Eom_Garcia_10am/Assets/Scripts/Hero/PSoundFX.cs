using UnityEngine;
using System.Collections;

public class PSoundFX : MonoBehaviour {
	public AudioClip grassLanding;
	public AudioClip grassWalkingS1;
	public AudioClip grassWalkingS2;
	public AudioClip dryWalkingS1;
	public AudioClip dryWalkingS2;
	public AudioClip dryLanding;
	public AudioClip wetWalkingS1;
	public AudioClip wetWalkingS2;
	public AudioClip wetLanding;
	private AudioSource mySource;
	
	// Use this for initialization
	void Start () {
		mySource = this.GetComponent<AudioSource>();
	}
	
	public void playGrassLanding(){
		mySource.Stop();
		mySource.clip = grassLanding;
		mySource.Play();
	}
	
	public void playDryLanding(){
		mySource.Stop();
		mySource.clip = dryLanding;
		mySource.Play();
	}
	
	public void playWetLanding(){
		mySource.Stop();
		mySource.clip = wetLanding;
		mySource.Play();
	}
	
	public void playGrassWalkingS1(){
		mySource.Stop();
		mySource.clip = grassWalkingS1;
		mySource.Play();
	}
	
	public void playGrassWalkingS2(){
		mySource.Stop();
		mySource.clip = grassWalkingS2;
		mySource.Play();
	}
	
	public void playDryWalkingS1(){
		mySource.Stop();
		mySource.clip = dryWalkingS1;
		mySource.Play();
	}
	
	public void playDryWalkingS2(){
		mySource.Stop();
		mySource.clip = dryWalkingS2;
		mySource.Play();
	}
	
	public void playWetWalkingS1(){
		mySource.Stop();
		mySource.clip = wetWalkingS1;
		mySource.Play();
	}
	
	public void playWetWalkingS2(){
		mySource.Stop();
		mySource.clip = wetWalkingS2;
		mySource.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
