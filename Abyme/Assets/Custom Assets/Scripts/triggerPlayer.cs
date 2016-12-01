using UnityEngine;
using System.Collections;

public class triggerPlayer : MonoBehaviour {

	public Anxiety anxiety;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnTriggerEnter(Collider other)
	{

		if(other.tag =="Player")
		{
				anxiety.threatLevel++;
			
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.tag =="Player")
		{
				anxiety.threatLevel--;
			
		}
	}
}
