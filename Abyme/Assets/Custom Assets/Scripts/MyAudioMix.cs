using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MyAudioMix : MonoBehaviour {

    
    public AudioSource coeurCalme;
    public AudioSource coeurStress;
    public AudioMixerSnapshot calmSnapshot;
    public AudioMixerSnapshot stressSnapshot; 
	public GameObject player;
    public bool chasing = false;
    public float transitionTime;

	void Update () {
        Vector3 playerPos = player.transform.position;
        //Debug.DrawRay(transform.position, playerPos - transform.position, Color.red);
        Vector3 facing = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerPos - transform.position, out hit, 20f)){
            if(player == hit.collider.gameObject && Vector3.Angle(facing,playerPos-transform.position) < 45) {
                if(!chasing){
                coeurCalme.Play();
                stressSnapshot.TransitionTo(transitionTime);
                chasing = true;
                }
            }
            else {
                if(chasing){
                chasing = false;
                calmSnapshot.TransitionTo(transitionTime);
                }
            }
        }
    }
}
