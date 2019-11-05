using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {
    public bool infested;
    NavMeshAgent agent;
    GameObject player;
    [SerializeField]
	int mood = 0;
    [SerializeField]
    int suspicion = 0;

    int movetimer = 0;
    Vector3 movePos;

    public Transform[] waypoints;
    public int waypointPos = 0;

    [SerializeField]
    Vector3 investigatePos;
    int layerMask;
    [SerializeField]
    bool isSeeing;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        layerMask = ~layerMask;
    }
	
	// Update is called once per frame
	void Update () {
        if (!infested){
            player = GameObject.FindGameObjectWithTag("Player");
            isSeeing = canSee(player.transform.position);
            if (suspicion > 2000 && mood < 2){
                mood = 2;
            }
            else if (suspicion > 600 && mood < 1){
                if (isSeeing){
                    investigatePos = player.transform.position;
                }
                mood = 1;
            }
            else if (suspicion <= 0)
            {
                mood = 0;
            }
            if (mood == 0){
                Patrol();
            }
            if (mood == -1){
                Investigate();
            }
            if (mood == 1){
                Suspicious();
            }
            if (mood == 2){
                Angry();
            }

            if (isSeeing)
            {
                suspicion++;
            }
            else {
                if (suspicion > 0){
                    suspicion--;
                }
            }
        }
	}

	virtual public void Patrol(){
        if (Vector3.Distance(transform.position, waypoints[waypointPos].position) < 5)
        {
            waypointPos++;
            if (waypointPos >= waypoints.Length)
            {
                waypointPos = 0;
            }
            
        }
        else {
            agent.SetDestination(waypoints[waypointPos].position);
        }
	}

    virtual public void Suspicious() {
        if (suspicion > 600 && isSeeing)
        {
            investigatePos = player.transform.position;
            agent.SetDestination(investigatePos);
        }
        else if (suspicion > 100 && isSeeing) {
            suspicion = 600;
        }
        else
        {
            if (movetimer <= 0)
            {
                movePos = investigatePos + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                movetimer = 200;
            }
            agent.SetDestination(movePos);
            movetimer--;
        }
    }

    virtual public void Angry() {
        suspicion = 3000;
        if (isSeeing)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    virtual public void Investigate() {
        if (Vector3.Distance(transform.position, investigatePos) > 10)
        {
            agent.SetDestination(investigatePos);
        }
        else {
            mood = 1;
        }
    }

    virtual public void Alert(Vector3 alertPos) {
        if (mood < 1)
        {
            mood = -1;
            suspicion += 50;
            investigatePos = alertPos;
        }
    }

    public void ToggleInfested() {
        infested = !infested;
        if (infested)
        {
            suspicion = 0;
            agent.ResetPath();
        }
        else {
            suspicion = 600;
        }
    }


    bool canSee(Vector3 objPos) {
        bool lineOfSight = !Physics.Linecast(transform.position, objPos, layerMask);
        bool inViewCone = (Vector3.Distance(transform.position, objPos) < 20 && Vector3.Angle(transform.forward, (objPos - transform.position)) < 45);
        return (lineOfSight && inViewCone);
    }
}
