using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsOnCommand : MonoBehaviour {
	
	//These first two are the easiest way to make sound in FMOD via scripting - we
	//do a "play one shot" that plays the VHS Event
	[FMODUnity.EventRef]
	public string VHSAudioEvent;

	[FMODUnity.EventRef] 
	public string LongerVHSAudioEvent;
	
	//This one is a bit different - we need 
	[FMODUnity.EventRef] 
	public string TVSustainEvent;
	private FMOD.Studio.EventInstance tvEvent;

	private bool turnedOnTV = false;
	
	
	
	
	//we're going to have an arbitrary parameter that we increase by pressing 'C',
	//and use this to adjust things in FMOD
	public float playerCoolness = 0f;
	
	// Use this for initialization
	void Start () {
		//We need to create an instance of our TV event, even before we start playing it.	
		tvEvent = FMODUnity.RuntimeManager.CreateInstance(TVSustainEvent);
		turnedOnTV = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.C)) {
			PlayOneShotSound();
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			PlayTrackedOneShotSound();
		}
		
		
		//This Controls the Television Loop Sound

		if (Input.GetKey(KeyCode.Y)) {
			//manage sustained sound
			if (!turnedOnTV) {
				tvEvent.setParameterValue("TelevisionOff", 0f);
				turnedOnTV = true;
				Debug.Log("Turned On TV");
				tvEvent.start();
			}
			
		} else if (Input.GetKeyUp(KeyCode.Y)) {
			//when i release the key, i turn off the tv
			tvEvent.setParameterValue("TelevisionOff", 1.0f);
			turnedOnTV = false;
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			playerCoolness++;
			//Attach to FMOD Parameter
		}
		
		//todo --
		//have the following functions working for the demo on Monday:
		//pressing C does the vhs sound as a one shot (unattached)
		//pressing M does the vhs sound as a one shot (attached)
		//pressing and holding Y goes through an AHDSR - esque curve
		//pressing K increases coolness, which adjusts a global audio effect
		
	}

	void PlayOneShotSound() {
		//PlayOneShot functions similarly as it does in unity, though now we can give it a position
		//This is probably the easiest way to 
		FMODUnity.RuntimeManager.PlayOneShot(VHSAudioEvent, gameObject.transform.position);
	}

	void PlayTrackedOneShotSound() {
		//This will keep track of the game object's position over the sound's lifetime
		FMODUnity.RuntimeManager.PlayOneShotAttached(LongerVHSAudioEvent, gameObject);
	}
}
