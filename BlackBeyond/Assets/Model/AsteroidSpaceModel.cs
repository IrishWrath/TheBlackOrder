public class AsteroidSpaceModel : SpaceModel
{
    public AsteroidSpaceModel(int row, int column, MapModel map) : base(row, column, map)
    {
    }

	public override void GetMovementEffects(ShipModel shipModel)
	{
        if (shipModel != null)
        {
            shipModel.Damage(1);
        }
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