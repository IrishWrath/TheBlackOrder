// This is a class for ships. It could be a player or a pirate ship
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
    protected int shipArmor = 1;

    // Cargo hold stats
    protected int maxCargoSpace;
    protected int availableCargoSpace;

    // This ship's space
    protected SpaceModel currentSpace;
    private ShipController shipController;

    public int GetDamage()
    {
        return shotDamage;
    }
    public int GetHealth()
    {
        return shipHealth;
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
        }
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
        if (shotCounter > 0)
        {
            int armor = enemy.GetArmor();
            int currentHealth = enemy.GetHealth();
            int adjDamage = armor - shotDamage;
            if (adjDamage <= 0)
            {
                // Always does at least one damage
                adjDamage = 1;
            }
            int remainingHP = currentHealth - adjDamage;
            enemy.SetHealth(remainingHP);
            currentShotCounter -= 1;
            // Creates a laser. Finn's animation
            shipController.CreateLaser(currentSpace, enemy.GetSpace());
        }
    }

    public void ResetShotCounter()
    {
        currentShotCounter = shotCounter;
    }

    public void FinishedAnimatingMovement()
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


