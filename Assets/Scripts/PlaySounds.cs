using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class PlaySounds : MonoBehaviour {

	[FMODUnity.EventRef] public string playVHSSound;

	[FMODUnity.EventRef] public string playLongerVHSSound;

	[FMODUnity.EventRef] public string televisionLoop;

	public string TelevisionOffParam;
	
	private FMOD.Studio.EventInstance televisionLoopInstance;

	private bool playingTVSound;
	
	
	// Use this for initialization
	void Start () {

		televisionLoopInstance = FMODUnity.RuntimeManager.CreateInstance(televisionLoop);
		playingTVSound = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.C)) {
			//when I press C, I want to play the short VHS Sound wherever this game object is
			FMODUnity.RuntimeManager.PlayOneShot(playVHSSound, transform.position);
		}

		if (Input.GetKeyDown(KeyCode.M)) {
			//when I press M, I want to play the longer VHS sound, wherever this game object is
			FMODUnity.RuntimeManager.PlayOneShotAttached(playLongerVHSSound, gameObject);
		}

		if (Input.GetKey(KeyCode.Y)) {
			//when I first press Y, we want to start the television loop
			//as I keep holding Y down, we want the loop to continue

			if (!playingTVSound) {
				televisionLoopInstance.setParameterValue("TelevisionOff", 0.0f);
				televisionLoopInstance.start();
				playingTVSound = true;
			}
			
		} else if (Input.GetKeyUp(KeyCode.Y)) {
			//when I release Y, we want the loop to exit, and set playingTVSound back to false
			televisionLoopInstance.setParameterValue("TelevisionOff", 1.0f);
			playingTVSound = false;
		}
		
	}
}
