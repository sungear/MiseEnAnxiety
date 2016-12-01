using UnityEngine;
using System.Collections;

public class sound : MonoBehaviour {
    public AudioSource sfx;

	// Use this for initialization
	void Start () {
	
	}

     void OnTriggerEnter(Collider other) {

       sfx.Play();

       Destroy(gameObject);
    }
    	
}
