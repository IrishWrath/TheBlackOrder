using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    private IPlayerCallback callback;
    private Space playerSpace;

    public Player(Space playerSpace)
    {
        this.playerSpace = playerSpace;
    }

    public IPlayerCallback GetCallback()
    {
        return callback;
    }

    public void SetCallback(IPlayerCallback callback)
    {
        this.callback = callback;
    }

    public Space GetSpace()
    {
        return playerSpace;
    }
}
