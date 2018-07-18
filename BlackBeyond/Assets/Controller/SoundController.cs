using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour{

	//the view (speaker) of the music controller
	private GameObject soundView;
	//has all our music and sfx stored.
	private AudioSource[] sounds;
    //gives each sound number a name.
    public enum SoundNum { main, battle, trade, endTurn }
	
    public SoundController()
    {
    }
	
	// Sets the view
    public void SetSoundView(GameObject soundView)
    {
        this.soundView = soundView;
		this.sounds = this.soundView.GetComponents<AudioSource>();
    }

    public void SwitchMusic(SoundNum songNum)
    {
        //stop all music first.
        for(int i = 3; i<3; i++){
            sounds[i].Stop();
        }
        //play selected song
        sounds[(int)songNum].Play();
    }
	
	//toggles mute of sounds.
	public void MuteSounds(){
		foreach (AudioSource sound in sounds){
			sound.mute = !sound.mute;
		}
	}	
	
	public void PlaySound(SoundNum soundNum){
		sounds[(int)soundNum].Play();
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
