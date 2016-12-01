using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class FakeEnemyLightSpawner : MonoBehaviour {

    public GameObject lightPrefab;
    private GameObject actualLight;

    public float minExistingTime;
    public float maxExistingTime;
    public float existingTime;

    public float minDelayTime;
    public float maxDelayTime;
    public float delayTime;

	// Use this for initialization
	void Start () {
	   delayTime = Random.Range(minDelayTime, maxDelayTime);
       existingTime = Random.Range(minExistingTime, maxExistingTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (delayTime > 0) {
            delayTime -= Time.deltaTime;
        }
        else if (delayTime <= 0) {
            if (actualLight == null) {
                actualLight = (GameObject) Instantiate(lightPrefab, transform.position, transform.rotation);
            }
            existingTime -= Time.deltaTime;
            if (existingTime <= 0) {
                Destroy(actualLight);
                existingTime = Random.Range(minExistingTime, maxExistingTime);
                delayTime = Random.Range(minDelayTime, maxDelayTime);
            }
        }
	}
}
