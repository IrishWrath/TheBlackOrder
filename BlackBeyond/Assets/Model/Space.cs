using UnityEngine;

public class Space 
{


    public Space(int row, int column, IModelLink link)
    {
        this.Row = row;
        this.Column = column;
        this.callback = link.CreateSpaceView(row, column);

    }

    public int Row { get; private set; }
    public int Column { get; private set; }
    private readonly ISpaceCallback callback;

    public Vector2 GetPosition()
    {
        return callback.GetPosition();
    }
}
