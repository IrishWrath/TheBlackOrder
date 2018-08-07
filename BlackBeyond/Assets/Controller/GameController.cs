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
    private int turnNumber = 0;

    private bool playerTurn = true;

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
	//The Menu audio sliders
	public Slider musicSlider;
	public Slider sfxSlider;

    public Button MoveButton;
    public Button ShootButton;
    public Button TradeButton;
    public Button EndTurnButton;

    // Container for spaces
    public GameObject mapGameObject;
	
    // A reference to the player.
    private PlayerModel playerModel;
    // All the pirates that are currently moving
    public List<PirateModel> piratesMoving = new List<PirateModel>();

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
        this.MapControllerField = new MapController(125, 250, modelLink, this);

        // Gets a starting space for the player, based on coordinates. Moving away from coordinates, but they are fine for setup
        SpaceModel playerSpace = MapControllerField.Map.GetSpace(63, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace, MapControllerField.Map);
        modelLink.CreatePlayerView(playerModel, playerMovementText);

        //MapControllerField.Map.CreateHunterKiller(playerModel, this);
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
        if (playerTurn)
        {
            EventSystem.current.SetSelectedGameObject(null);
            // player.OpenTrade
        }
    }

    // This function is called whe the player presses "end turn"
    public IEnumerator EndTurn()
    {
        if (playerTurn)
        {
            piratesMoving.Clear();
            playerTurn = false;
            MoveButton.interactable = false;
            ShootButton.interactable = false;
            TradeButton.interactable = false;
            EndTurnButton.interactable = false;

            EventSystem.current.SetSelectedGameObject(null);
            playerModel.EndTurn();
            soundController.PlaySound(SoundController.Sound.endTurn);

            // MapModel will handle the pirates  
            yield return this.MapControllerField.Map.EndTurn(++turnNumber);

            //attempt to increase the amount of turns since the player was in battle.
            playerModel.turnsSinceShot++;
            //if the player has not been shot for 3 turns, change music.
            if(playerModel.turnsSinceShot > 3)
            {
                soundController.SwitchMusic(SoundController.Sound.main);
            }
        }
    }

    public void StartTurn()
    {
        playerTurn = true;
        MoveButton.interactable = true;
        ShootButton.interactable = true;
        //if( player is on trade station)
        //TradeButton.interactable = true;
        EndTurnButton.interactable = true;
        playerModel.StartTurn();
    }

    public void AddPirateMoving(PirateModel pirate)
    {
        piratesMoving.Add(pirate);
    }
    public void RemovePirateMoving(PirateModel pirate)
    {
        piratesMoving.Remove(pirate);
        if (piratesMoving.Count == 0 && !playerTurn)
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
            StartCoroutine(EndTurn());
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
