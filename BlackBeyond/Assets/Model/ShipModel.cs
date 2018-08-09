// This is a class for ships. It could be a player or a pirate ship
using System;

public abstract class ShipModel
{
    // Is this ship animating movement?
    protected bool animatingMovement;

    //ship combat stat variables. Both Player and pirates use these. protected so that subclasses can use
    protected int shipHealth;
    protected int maxHealth;
    protected int shotDamage;
    protected int attackRange;
    protected int maxMovement;
    protected int currentMovement;
    protected int shotCounter;
    protected int currentShotCounter;
    // TODO, sort out how combat works
    protected int shipArmor = 0;

    // Cargo hold stats
    protected int maxCargoSpace;
    protected int availableCargoSpace;

    // This ship's space
    protected SpaceModel currentSpace;
    private ShipController shipController;

    //this counts how many turns since the player was last shot. used to control music.
    public int turnsSinceShot = 0;
    internal SoundController soundController;

    public int GetDamage()
    {
        return shotDamage;
    }
    public int GetHealth()
    {
        return shipHealth;
    }
    public int GetAttackRange()
    {
        return attackRange;
    }

    public void Damage(int damage)
    {
        SetHealth(shipHealth - damage);
    }

    public void SetHealth(int health)
    {
        shipHealth = health;
        // update health bar
        shipController.UpdateHealth(shipHealth, maxHealth);

        if (shipHealth <= 0)
        {
            // die
            Die();
            soundController.PlaySound(SoundController.Sound.destroy, 0.4f);
        }
        else
        {
            soundController.PlaySound(SoundController.Sound.damage, 0.3f);
        }
    }

    public void SetSoundController(SoundController soundController)
    {
        this.soundController = soundController;
    }

    public abstract void Die();

    public int GetArmor()
    {
        return shipArmor;
    }

    public int GetMaxCargoSpace()
    {
        return maxCargoSpace;
    }

    public int GetAvailableCargoSpace()
    {
        return availableCargoSpace;
    }

    public void SetAvailableCargoSpace(int currentCargoSpace)
    {
        availableCargoSpace = currentCargoSpace;
    }

    public void Shoot(ShipModel enemy)
    {
        if (currentShotCounter > 0)
        {
            shipController.CreateLaser(currentSpace, enemy.GetSpace(), enemy);
            soundController.PlaySound(SoundController.Sound.shoot1);
            soundController.SwitchMusic(SoundController.Sound.battle);
            //reset counter.
            turnsSinceShot = 0;
            enemy.turnsSinceShot = 0;
        }
    }

    public void ShootDamage(ShipModel enemy)
    {
        int armor = enemy.GetArmor();
        int currentHealth = enemy.GetHealth();
        int adjDamage = shotDamage - armor;
        if (adjDamage <= 0)
        {
            // Always does at least one damage
            adjDamage = 1;
        }
        int remainingHP = currentHealth - adjDamage;
        enemy.SetHealth(remainingHP);
        this.currentShotCounter -= 1;
        // Creates a laser. Finn's animation
    }

    public void ResetShotCounter()
    {
        currentShotCounter = shotCounter;
    }

    public virtual void FinishedAnimatingMovement()
    {
        animatingMovement = false;
    }

    public int GetCurrentMovement()
    {
        return currentMovement;
    }

    public void UpdateCurrentMovement(int movementUsed)
    {
        currentMovement = currentMovement - movementUsed;
    }

    public SpaceModel GetSpace()
    {
        return currentSpace;
    }

    public void SetController(ShipController controller)
    {
        this.shipController = controller;
    }
}


