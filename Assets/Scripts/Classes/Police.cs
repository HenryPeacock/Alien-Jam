using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : NPC
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
        agent.angularSpeed = 500;
        agent.speed = 5.5f;
        agent.acceleration = 11;
        job = "Police";

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }
}
