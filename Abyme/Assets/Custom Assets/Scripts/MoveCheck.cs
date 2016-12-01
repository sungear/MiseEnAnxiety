using UnityEngine;
using System.Collections;

public class MoveCheck : MonoBehaviour {

    private Vector3 playerPosition;
    private Vector3 oldPlayerPosition;

	// Use this for initialization
	void Start () {	
        playerPosition = transform.position;
        oldPlayerPosition = playerPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}    

    public bool Moving() {
        playerPosition = transform.position;
        if(playerPosition != oldPlayerPosition){            
            oldPlayerPosition = playerPosition;
            return true;
        }
        else {
            return false;
        }
    }
}
