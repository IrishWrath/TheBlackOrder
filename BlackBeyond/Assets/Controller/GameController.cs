using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }
	public SoundController soundController { get; private set; }
    public StationController stationController { get; private set; }

    // A model link.
    private ModelLink modelLink;
    private StationModel stationModel;

    private TradeGUIController tradeGUIController;

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

    public Button TradeButton;

    // The Asteroid Terrain
    public GameObject asteroidTerrain;

    public Text playerMovementText;

    // Container for spaces
    public GameObject mapGameObject;
    public GameObject dockUI;
	
    // A reference to the player.
    private PlayerModel playerModel;

    // Use this for initialization. Starting method for our code.
    public void Start()
    {
		        //Get the path of the Game data folder
        string m_Path = Application.dataPath;

        tradeGUIController = dockUI.GetComponent<TradeGUIController>();

        stationModel = new StationModel(tradeGUIController);

		//Creates the sound view and sound controller.
		this.soundView = UnityEngine.Object.Instantiate(this.soundView);
		// Gets the controller from the musicView GameObject.
        this.soundController = this.soundView.GetComponent<SoundController>();
        // Lets the Controller access the GameObject
        this.soundController.SetSoundView(this.soundView);
		
        //Output the Game data path to the console
        this.modelLink = new ModelLink(this, mapGameObject, stationModel);

        // Creates the map.
        this.MapController = new MapController(125, 250, modelLink, stationModel);

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
        SpaceModel playerSpace = MapController.Map.GetSpace(63, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace, this, stationModel);
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

    public GameObject GetDockUI()
    {
        return dockUI;
    }

    public void PlayerMoveButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // Tells the player to start moving
        playerModel.StartMove();
		//play button sound
		soundController.PlaySound(SoundController.Sound.buttonPress);
    }

    public void PlayerShootButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // player.StartShoot
    }

    public void PlayerTradeButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // player.OpenTrade
        //StationModel station = new StationModel(MapController.Map.GetSpace(61,125));
        //modelLink.CreateStationView(station); -- ERROR!!

        Station station = stationModel.GetStation(playerModel.GetSpace());
        if(station != null)
        {
            station.ShowDockUI(playerModel);
        }
        //play button sound
        soundController.PlaySound(SoundController.Sound.buttonPress);

        //dockUI.SetActive(true);
    }

    public void SetTradeable(bool isTradable)
    {
        TradeButton.interactable = isTradable;
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
        if (Input.GetKeyUp(KeyCode.T))
        {
            PlayerTradeButton();
        }
    }
}
