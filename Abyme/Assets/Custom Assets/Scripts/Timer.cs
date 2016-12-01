using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class Timer : MonoBehaviour {

    public float timerTime;
    public Text timerText;

    public GameObject mainCam;
    private NoiseAndScratches noiseEffect;
    private VignetteAndChromaticAberration vignetteEffect;

	// Use this for initialization
	void Start () {
        timerText.text = timerTime.ToString();

        noiseEffect = mainCam.GetComponent<NoiseAndScratches>();
        vignetteEffect = mainCam.GetComponent<VignetteAndChromaticAberration>();

        noiseEffect.grainIntensityMin = 0f;
        noiseEffect.grainIntensityMax = 0f;
        noiseEffect.enabled = false;

        vignetteEffect.intensity = 0.25f;
        vignetteEffect.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (timerTime > 0)
        {
            timerTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timerTime).ToString();
        }
	}

    public void EndEffect () {
        noiseEffect.enabled = true;
        if (noiseEffect.grainIntensityMin < 5 && noiseEffect.grainIntensityMax < 5 && vignetteEffect.intensity < 0.4){
            noiseEffect.grainIntensityMin += 1f * Time.deltaTime;
            noiseEffect.grainIntensityMax += 1f * Time.deltaTime;
            vignetteEffect.intensity += 0.1f * Time.deltaTime;
        }
        if (vignetteEffect.intensity >= 0.4){
             if (vignetteEffect.intensity < 1f) {
                 vignetteEffect.intensity += 0.03f;
             }
        }
    }

    public bool EffectEnded () {
        if (vignetteEffect.intensity >= 1) {
            return true;
        }
        return false;
    }
}
