using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class enemyAI : MonoBehaviour {

    private AICharacterControl controller;
    public Transform waypoints;

    private GameObject player;

    private Transform currentWp;
    public bool chasing = false;

    public bool searchingLastPos = false;
    public Transform lastPlayerPos;
    //public int levelAnxiety = 0;
    Transform oldD;
    Transform newD;
    public float distance2View = 20f;
    public float distanceWithoutAngle=1f;
    public float angle2View = 45f;

    public bool view_target = false;
    

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        controller = GetComponent<AICharacterControl>();
        controller.target = SelectDestination();
        currentWp = controller.target;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
        //playerPos.y += 1f;
        Vector3 facing = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        view_target = false;

        Vector3 forward = playerPos - myPos;
        if(Physics.Raycast(transform.position, forward, out hit, distance2View))
        {
            Debug.DrawLine(myPos, hit.point, Color.cyan);
            if ( hit.transform.gameObject == player)
            {
                if (Vector3.Angle(facing, playerPos - myPos) < angle2View || chasing)
                {
                    view_target = true;
                    if (!chasing)
                    {
                        //levelAnxiety += 1;
                        chasing = true;
                    }
                    controller.target = player.transform;
                }
            }
        }
        if(!view_target) {
            //Va au dernier point du joueur
            if (chasing)  
            {
                if (!searchingLastPos)
                {
                    lastPlayerPos.position = player.transform.position;
                    searchingLastPos = true;
                }

                if (searchingLastPos)
                {
                    if ((lastPlayerPos.position - transform.position).magnitude > 1 )
                    {
                        controller.target = lastPlayerPos;
                    }
                    else
                    {
                        searchingLastPos = false;
                        chasing = false;
                    }
                }
            }

            if (!chasing)  {
                controller.target = currentWp;
            }
        }
    }


    void OnTriggerEnter(Collider other) {
        if (other.transform != controller.target) {
            return;
        }

        oldD = controller.target;
        newD = SelectDestination();
        while (oldD == newD) {
            newD = SelectDestination();
        }
        controller.target = newD;
        currentWp = controller.target;
    }

    private Transform SelectDestination() {
            Transform[] childList = waypoints.GetComponentsInChildren<Transform>();
            int child = waypoints.childCount;
            int index = Random.Range(1, child + 1);
            Transform selectedWp = childList[index];
            return selectedWp;
    }

}