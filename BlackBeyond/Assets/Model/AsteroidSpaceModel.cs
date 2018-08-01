public class AsteroidSpaceModel : SpaceModel
{
    public AsteroidSpaceModel(int row, int column, MapModel map) : base(row, column, map)
    {
    }

	public override void GetMovementEffects(ShipModel shipModel)
	{
        //TODO Implement this after damage
        //shipModel.Damage();
	}

    public override bool IsHazardous()
    {
        return true;
    }

    public override bool BlocksLOS()
    {
        return true;
    }
}