using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateModel : MonoBehaviour {
        private PirateController pirateController;
        private SpaceModel pirateSpaceModel;
        
      
        
    //ship combat stat variables
       
        private int health;
        private int damage;
        private int detectRange;
        private int attackRange;
        private int maxPirateMovement;
        public int currentPirateMovement;
        private int currentMovement;

    //pirate ship builder template
    public PirateModel(int health, int damage, int detectRange, int attackRange , int maxPirateMovement)
    {
     
        this.health = health;
        this.damage = damage;
        this.detectRange = detectRange;
        this.attackRange = attackRange;
        this.maxPirateMovement = maxPirateMovement;
        this.currentPirateMovement = maxPirateMovement;
    }

    //pirate ship classes and stats
    PirateModel scout = new PirateModel(2, 1, 3, 1, 4);
    PirateModel frigate = new PirateModel(4, 2, 3, 2, 3);
    PirateModel platform = new PirateModel(4, 1, 4, 4, 0);
    PirateModel destroyer = new PirateModel(7, 2, 3, 2, 2);
    PirateModel dreadnaught = new PirateModel(10, 4, 2, 1, 2);

    

    public PirateModel(SpaceModel pirateSpace)
        {
            this.pirateSpaceModel = pirateSpace;
        }
        

        public PirateController GetController()
        {
            return pirateController;
        }

        public void SetController(PirateController controller)
        {
            this.pirateController = controller;
        }

        public SpaceModel GetSpace()
        {
            return pirateSpaceModel;
        }

        public int GetCurrentMovement()
        {
            return currentMovement;
        }

        public void UpdateCurrentMovement(int movementUsed)
        {
            currentMovement = currentMovement - movementUsed;
        }
    }
