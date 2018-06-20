using UnityEngine;

public class Space 
{
    public Space(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }

    public int Row { get; private set; }
    public int Column { get; private set; }
    private ISpaceCallback callback;

    public ISpaceCallback GetCallback()
    {
        return callback;
    }

    public void SetCallback(ISpaceCallback callback)
    {
        this.callback = callback;
    }
}
