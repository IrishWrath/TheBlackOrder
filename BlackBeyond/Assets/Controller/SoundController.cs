using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour{

	//the view (speaker) of the music controller
	private GameObject soundView;
	//has all our music and sfx stored.
	private AudioSource[] sounds;
    //gives each sound number a name.
    public enum Sound { main, battle, trade, endTurn, move, buttonPress, shoot1, shoot2, shoot3, buy, destroy, damage }

    protected SoundController.Sound currentlyPlaying = SoundController.Sound.main;

	private Slider musicSlider;
	private Slider sfxSlider;

    public SoundController()
    {
    }
    // Sets the view
    public void SetSoundView(GameObject soundView)
    {
        this.soundView = soundView;
		this.sounds = this.soundView.GetComponents<AudioSource>();
    }

    public void SwitchMusic(Sound songNum)
    {
        if (currentlyPlaying != songNum)
        {
            //stop all music first.
            for (int i = 0; i < 3; i++)
            {
                sounds[i].Stop();
            }
            //play selected song
            sounds[(int)songNum].Play();
            currentlyPlaying = songNum;
        }
    }

	//makes the volume for our sounds match the sliders in the menu.
	public void ChangeVolume(){
		//for music
        for (int i = 0; i < 3; i++)
        {
            sounds[i].volume = musicSlider.value;
        }
	
		//for sfx
		for (int i = 3; i < sounds.Length; i++)
        {
            sounds[i].volume = sfxSlider.value;
        }

	}
	
	public void SetSliders(Slider musicSlider, Slider sfxSlider){
		this.musicSlider = musicSlider;
		this.sfxSlider = sfxSlider;
	}
	
    //toggles mute of sounds.
    public void MuteSounds(){
	    foreach (AudioSource sound in sounds){
		    sound.mute = !sound.mute;
	    }
	}	
	
	public void PlaySound(Sound soundNum){
		sounds[(int)soundNum].Play();
	}
	// Update is called once per frame
    void Update()
    {
        //Test press B to switch to battle music. Later have it switch by natural causes
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchMusic(SoundController.Sound.battle);
        }

        //Test press T to switch to trade music. Later have it switch by natural causes
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchMusic(SoundController.Sound.trade);
        }

        //Test press D to switch to default music. Later have it switch by natural causes
        if (Input.GetKeyDown(KeyCode.D))
        {
            SwitchMusic(SoundController.Sound.main);
        }
		
		ChangeVolume();
		
    }
}
