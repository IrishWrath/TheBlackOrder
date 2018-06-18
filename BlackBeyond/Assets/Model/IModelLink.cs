using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelLink 
{
    ISpaceCallback CreateSpaceView(int row, int column);
}
