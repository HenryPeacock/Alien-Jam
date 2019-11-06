using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : NPC {

	// Use this for initialization
	new void Start () {
        base.Start();
        agent.angularSpeed = 480;
        agent.speed = 5;
        agent.acceleration = 10;
        job = "Guard";
        
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        
	}
}
