using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

    public float timeToDie;

    private float life = 0f;
	
	// Update is called once per frame
	void Update () {
        life += Time.deltaTime;
        if (life > timeToDie)
        {
            Destroy(this.gameObject);
        }
    }
}
