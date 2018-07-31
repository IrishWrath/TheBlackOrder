using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapControllerField { get; private set; }
	public SoundController soundController { get; private set; }

    // A model link.
    private ModelLink modelLink;

    // The Prefab for Spaces
    public GameObject spaceView;
    // The Prefab for Player's ship
    public GameObject playership;
    // The Prefab for Pirate ships
    public GameObject pirateship;
    // The Prefab for stations
    public GameObject stationView;
    // The Nebula Terrain
    public GameObject nebulaTerrain;
	// The Prefab for our music
	public GameObject soundView;

    // The Asteroid Terrain
    public GameObject asteroidTerrain;

    public Text playerMovementText;
	
	public Slider musicSlider;
	public Slider sfxSlider;

    // Container for spaces
    public GameObject mapGameObject;
	
    // A reference to the player.
    private PlayerModel playerModel;

    // Use this for initialization. Starting method for our code.
    public void Start()
    {
		        //Get the path of the Game data folder
        string m_Path = Application.dataPath;

		//Creates the sound view and sound controller.
		this.soundView = UnityEngine.Object.Instantiate(this.soundView);
		// Gets the controller from the musicView GameObject.
        this.soundController = this.soundView.GetComponent<SoundController>();
        // Lets the Controller access the GameObject
        this.soundController.SetSoundView(this.soundView);
		this.soundController.SetSliders(this.musicSlider, this.sfxSlider);
		
        //Output the Game data path to the console
        this.modelLink = new ModelLink(this, mapGameObject);

        // Creates the map.
        this.MapControllerField = new MapController(125, 250, modelLink);

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
        SpaceModel playerSpace = MapControllerField.Map.GetSpace(63, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace, MapControllerField.Map);
        modelLink.CreatePlayerView(playerModel, playerMovementText);
    }

    // Returns the Prefabs
    public GameObject GetSpaceView()
    {
        return spaceView;
    }
    public GameObject GetPlayerView()
    {
        return playership;
    }
    public GameObject GetPirateView()
    {
        return pirateship;
    }

    public GameObject GetStationView()
    {
        return stationView;
    }

    public GameObject GetNebula()
    {
        return nebulaTerrain;
    }
    public GameObject GetAsteroid()
    {
        return asteroidTerrain;
    }

	public GameObject GetMusicView()
	{
		return soundView;
	}

    // called when the player presses the move button
    public void PlayerMoveButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // Tells the player to start moving
        playerModel.StartMove();
		//play button sound
		soundController.PlaySound(SoundController.Sound.buttonPress);
    }

    // called when the player presses the shoot button
    public void PlayerShootButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerModel.StartShoot();
    }

    // called when the player presses the trade button, should be disabled if there is nothing to trade with
    public void PlayerTradeButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // player.OpenTrade
    }

    // This function is called whe the player presses "end turn"
    public void EndTurn()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerModel.EndTurn();
		soundController.PlaySound(SoundController.Sound.endTurn);

        // MapModel will handle the pirates   
        this.MapControllerField.Map.EndTurn();

        playerModel.StartTurn();
    }

    // This Update should be avoided. Only place testing code here.
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            EndTurn();
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            PlayerMoveButton();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            PlayerShootButton();
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
