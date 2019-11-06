using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class President : NPC
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
        agent.angularSpeed = 600;
        agent.speed = 4.0f;
        agent.acceleration = 8;
        job = "President";

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }
}
