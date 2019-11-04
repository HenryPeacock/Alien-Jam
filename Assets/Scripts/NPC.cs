using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {
    NavMeshAgent agent;

	[SerializeField]
	int mood = 0;
    int suspicion = 0;
    int movetimer = 0;
    Vector3 movePos;

    public Transform[] waypoints;
    public int waypointPos = 0;

    Transform investigatePos;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mood == 0) {
			Patrol ();
		}
        if (mood == -1) {
            Investigate();
        }
        if (mood == 1) {
            Suspicious();
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
        if (movetimer <= 0) {
            movePos = investigatePos.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
            movetimer = 100;
        }
        agent.SetDestination(movePos);
        movetimer--;
        suspicion--;
        if (suspicion <= 0) {
            mood = 0;
        }
    }

    virtual public void Investigate() {
        if (Vector3.Distance(transform.position, investigatePos.position) > 5)
        {
            agent.SetDestination(investigatePos.position);
        }
        else {
            mood = 1;
        }
    }

    virtual public void Alert(Transform alertPos) {
        mood = -1;
        suspicion += 300;
        investigatePos = alertPos;
    }
}
