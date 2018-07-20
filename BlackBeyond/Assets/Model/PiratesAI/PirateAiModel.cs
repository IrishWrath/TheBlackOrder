using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PirateAiModel
{
    private PirateModel.PirateType pirateType;
    private MapModel map;

    protected PirateAiModel(PirateModel.PirateType pirateType, MapModel map)
    {
        this.pirateType = pirateType;
        this.map = map;
    }

    public abstract void EndTurn();

    protected MapModel GetMap()
    {
        return map;
    }
}
