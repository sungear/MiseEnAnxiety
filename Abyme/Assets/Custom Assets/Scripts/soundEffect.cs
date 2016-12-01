using UnityEngine;
using System.Collections;

public class soundEffect : MonoBehaviour {

    public AudioSource coeurCalme;
    public AudioSource coeurStress;

	public void ToggleCoeurs() {
        if (coeurCalme.isPlaying) {
            coeurCalme.Stop();
            coeurStress.Play();
        } else{
            coeurStress.Stop();
            coeurCalme.Play();
        }
    }
}
