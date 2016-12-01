using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class LightOnOff : MonoBehaviour {

    public float minOnTime = 5;
    public float maxOnTime = 30;
    public float onTime;

    public float minOffTime = 1;
    public float maxOffTime = 3;
    public float offTime;

    public GameObject light;
    private MeshRenderer bulbMesh;
    private Color defaultColor;
    private Light lighting;

	// Use this for initialization
	void Start () {
	   lighting = light.GetComponent<Light>();
       lighting.enabled = true;

       bulbMesh = GetComponent<MeshRenderer>();
       defaultColor = bulbMesh.materials[0].color;

       onTime = Random.Range(minOnTime, maxOnTime);
       offTime = Random.Range(minOffTime, maxOffTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (onTime > 0) {
            lighting.enabled = true;
            bulbMesh.materials[0].SetColor("_Color", defaultColor);
            bulbMesh.materials[0].SetColor("_EmissionColor", defaultColor);
            onTime -= Time.deltaTime;
        }
        else if (onTime <= 0) {
            lighting.enabled = false;
            bulbMesh.materials[0].SetColor("_Color", Color.black);
            bulbMesh.materials[0].SetColor("_EmissionColor", Color.black);
            if (offTime <= 0) {
                offTime = Random.Range(minOffTime, maxOffTime);
            	onTime = Random.Range(minOnTime, maxOnTime);
            }            
            offTime -= Time.deltaTime;
        }
	}
}
