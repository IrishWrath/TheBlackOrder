using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour{

	//the view (speaker) of the music controller
	private GameObject soundView;
	//has all our music and sfx stored.
	private AudioSource[] sounds;
	
    public SoundController()
    {
    }
	
	// Sets the view
    public void SetSoundView(GameObject soundView)
    {
        this.soundView = soundView;
		this.sounds = this.soundView.GetComponents<AudioSource>();
    }
	
	//toggles mute of sounds.
	public void MuteSounds(){
		foreach (AudioSource sound in sounds){
			sound.mute = !sound.mute;
		}
	}	
	
	public void PlayEndTurnSound(){
		sounds[1].Play();
	}
	// Update is called once per frame
    void Update()
    {
		//Mute with M key
        if (Input.GetKeyDown(KeyCode.N)){
            MuteSounds();
		}
    }
}
