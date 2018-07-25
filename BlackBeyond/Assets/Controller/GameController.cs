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
    public MapController MapController { get; private set; }
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
		
        //Output the Game data path to the console
        this.modelLink = new ModelLink(this, mapGameObject);

        // Creates the map.
        this.MapController = new MapController(125, 250, modelLink);

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
        SpaceModel playerSpace = MapController.Map.GetSpace(63, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace);
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

    public void PlayerMoveButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //TODO Call playership.startmove() or similar
        playerModel.StartMove();
		//play button sound
		soundController.PlaySound(SoundController.Sound.buttonPress);
    }

    // This function is called whe the player presses "end turn"
    public void EndTurn()
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerModel.EndTurn();
		soundController.PlaySound(SoundController.Sound.endTurn);

        // End of turn Housekeeping

        // Pirates move
        // Foreach pirate in map.GetPirates()
        //      pirate.DoTurn();
        // Or something...

        this.MapController.Map.EndTurn();

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
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
