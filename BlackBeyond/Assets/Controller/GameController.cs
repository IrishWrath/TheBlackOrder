using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityToolbag;

//using UnityEngine.

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapControllerField { get; private set; }
	public SoundController soundController { get; private set; }
    public StationController stationController { get; private set; }

    // A model link.
    private ModelLink modelLink;
    private int turnNumber = 0;
    private bool playerTurn = true;
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

    //public Button TradeButton;

    //player resources
    public GameObject currency;
    public GameObject metal;
    public GameObject organics;
    public GameObject gas;
    public GameObject water;
    public GameObject fuel;
    public GameObject fuelMax;
    public GameObject totalSpace;

    // The Asteroid Terrain
    public GameObject asteroidTerrain;

    public Text playerMovementText;
	//The Menu audio sliders
	public Slider musicSlider;
	public Slider sfxSlider;

    public Button MoveButton;
    public Button ShootButton;
    public Button TradeButton;
    public Button EndTurnButton;

    // Container for spaces
    public GameObject mapGameObject;
    public GameObject dockUI;
	
    // A reference to the player.
    private PlayerModel playerModel;
    // All the pirates that are currently moving
    public int piratesMoving = 0;

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
		this.soundController.SetSliders(this.musicSlider, this.sfxSlider);
		
        //Output the Game data path to the console
        this.modelLink = new ModelLink(this, mapGameObject, stationModel);

        // Creates the map.

        this.MapControllerField = new MapController(125, 250, modelLink, this, stationModel);

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
        SpaceModel playerSpace = MapControllerField.Map.GetSpace(63, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace, MapControllerField.Map, this, stationModel);

        modelLink.CreatePlayerView(playerModel, playerMovementText, currency, metal, organics, gas, water, fuel, fuelMax, totalSpace);

        MapControllerField.Map.CreateHunterKiller(playerModel, this);
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

    // called when the player presses the move button
    public void PlayerMoveButton()
    {
        if (playerTurn)
        {
            EventSystem.current.SetSelectedGameObject(null);
            // Tells the player to start moving
            playerModel.StartMove();
            //play button sound
            soundController.PlaySound(SoundController.Sound.buttonPress);
        }
    }

    // called when the player presses the shoot button
    public void PlayerShootButton()
    {
        if (playerTurn)
        {
            EventSystem.current.SetSelectedGameObject(null);
            playerModel.StartShoot();
            soundController.PlaySound(SoundController.Sound.buttonPress);
        }
    }

    // called when the player presses the trade button, should be disabled if there is nothing to trade with
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
        if (playerTurn)
        {
            piratesMoving = 0;
            playerTurn = false;

            MoveButton.interactable = false;
            ShootButton.interactable = false;
            TradeButton.interactable = false;
            EndTurnButton.interactable = false;
            EventSystem.current.SetSelectedGameObject(null);
            playerModel.EndTurn();
            soundController.PlaySound(SoundController.Sound.endTurn);

            //attempt to increase the amount of turns since the player was in battle.
            playerModel.turnsSinceShot++;
            //if the player has not been shot for 3 turns, change music.
            if (playerModel.turnsSinceShot > 3)
            {
                soundController.SwitchMusic(SoundController.Sound.main);
            }

            // MapModel will handle the pirates  
            var thread = new Thread(() =>
            {
                this.MapControllerField.Map.EndTurn(++turnNumber);
            });
            thread.Start();
        }
    }

    public void StartTurn()
    {
        playerTurn = true;
        Dispatcher.InvokeAsync(() =>
        {
            MoveButton.interactable = true;
            ShootButton.interactable = true;
            if (stationModel.GetStation(playerModel.GetSpace()) != null)
            {
                TradeButton.interactable = true;
            }
            //if( player is on trade station)
            //TradeButton.interactable = true;
            EndTurnButton.interactable = true;
            playerModel.StartTurn();
        });
    }

    public void AddPirateMoving()
    {
        piratesMoving++;
    }
    public void SetPirateMoving(int number)
    {
        piratesMoving = number;
    }
    public void RemovePirateMoving()
    {
        piratesMoving--;
        if (piratesMoving == 0 && !playerTurn)
        {
            StartTurn();
        }
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
        if (Input.GetKeyUp(KeyCode.T))
        {
            PlayerTradeButton();
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
