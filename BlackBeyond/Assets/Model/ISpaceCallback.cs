using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpaceCallback 
{
    Vector2 GetPosition();
	void SetSelectable(int number);
}
