using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLink : IModelLink
{

    public GameController GameController { get; private set; }

    public ModelLink(GameController gameController)
    {
        this.GameController = gameController;
    }
    public void CreateSpaceView(Space space)
    {
        GameObject spaceView = Object.Instantiate(GameController.GetSpaceView(), 
                               new Vector2(((float)space.Column - 1) / 2, (0 - space.Row)), Quaternion.identity);
        SpaceController controller = spaceView.GetComponent<SpaceController>();
        controller.spaceView = spaceView;
        controller.SetSpace(space);
        space.SetCallback(controller);
    }
}
