using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour{

	//the view (speaker) of the music controller
	private GameObject musicView;
	
    public MusicController()
    {
	   
    }
	
	    // Sets the view
    public void SetMusicView(GameObject musicView)
    {
        this.musicView = musicView;
    }
	
	public void MuteMusic(){
		AudioSource audioData = this.musicView.GetComponent<AudioSource>();
		audioData.mute=true;
	}	
	// Update is called once per frame
	void Update () {
		
	}
}
