using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {
    public string job;
    public bool infested;
    public Transform[] waypoints;
    public int waypointPos = 0;
    public Animator anim;
    public Rigidbody rb;

    public int mood = 0;
    public int suspicion = 0;
    public bool isSeeing;
    public Vector3 investigatePos;

    public NavMeshAgent agent;
    public GameObject player;
    public int movetimer = 0;
    public Vector3 movePos;
    public int layerMask;

    // Use this for initialization
    virtual public void Start () {
        agent = GetComponent<NavMeshAgent>();
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        layerMask = ~layerMask;
        movePos = transform.position;
    }
	
	// Update is called once per frame
	virtual public void Update () {
        if (!infested){
            player = GameObject.FindGameObjectWithTag("Player");
            isSeeing = canSee(player.transform.position);
            if (suspicion > 1500 && mood < 2){
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
                suspicion+= 3;
                if (player.GetComponent<PlayerController>().m_flying) {
                    mood = 2;
                }
            }
            if (anim != null)
            {
                anim.SetBool("isMoving", agent.velocity.magnitude > 0);
            }
            else {
                if (suspicion > 0){
                    suspicion--;
                }
                if (player.GetComponent<PlayerController>().m_flying && suspicion > 0) {
                    suspicion = 500;
                    mood = 1;
                }
            }
        }
	}

	virtual public void Patrol(){
        GetComponentInChildren<Light>().color = Color.white;
        if (waypoints.Length > 1)
        {
            if (Vector3.Distance(transform.position, waypoints[waypointPos].position) < 5)
            {
                waypointPos++;
                if (waypointPos >= waypoints.Length)
                {
                    waypointPos = 0;
                }

            }
            else
            {
                agent.SetDestination(waypoints[waypointPos].position);
            }
        }
        else {
            if (movetimer <= 0)
            {
                movePos = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
                movetimer = Random.Range(100, 500);
            }
            agent.SetDestination(movePos);
            movetimer--;
        }
	}

    virtual public void Suspicious() {
        GetComponentInChildren<Light>().color = Color.yellow;
        if (suspicion > 600 && isSeeing)
        {
            investigatePos = player.transform.position;
            agent.SetDestination(investigatePos);
            movetimer = 0;
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
        GetComponentInChildren<Light>().color = Color.red;
        suspicion = 3000;
        if (isSeeing)
        {
            agent.speed = 10;
            agent.angularSpeed = 800;
            investigatePos = player.transform.position;
            agent.SetDestination(investigatePos);
            movetimer = 0;
        }
        else
        {
            agent.speed = 3.5f;
            agent.angularSpeed = 360;
            if (movetimer <= 0)
            {
                movePos = investigatePos + new Vector3(Random.Range(-4.0f, 4.0f), 0, Random.Range(-4.0f, 4.0f));
                movetimer = 200;
            }
            agent.SetDestination(movePos);
            movetimer--;
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
        print("alert!");
        if (mood < 1)
        {
            mood = -1;
            suspicion += 500;
            investigatePos = alertPos;
        }
    }

    public void ToggleInfested() {
        infested = !infested;
        agent.enabled = !infested;
        if (infested)
        {
            suspicion = 0;
            agent.ResetPath();
        }
        else {
            suspicion = 600;
            Invoke("Reactivate", 2);
            GetComponent<NPC>().enabled = false;
        }
    }


    public bool canSee(Vector3 objPos) {
        bool nearby = (Vector3.Distance(transform.position, objPos) < 3);
        bool lineOfSight = !Physics.Linecast(transform.position, objPos, layerMask);
        bool inViewCone = (Vector3.Distance(transform.position, objPos) < 20 && Vector3.Angle(transform.forward, (objPos - transform.position)) < 45);
        return ((lineOfSight && inViewCone) || (nearby && lineOfSight));
    }

    public void Reactivate() {
        GetComponent<NPC>().enabled = true;
    }
}
