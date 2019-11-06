using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Engineer : NPC
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
        agent.angularSpeed = 300;
        agent.speed = 2.5f;
        agent.acceleration = 6;
        job = "Engineer";

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }
}
