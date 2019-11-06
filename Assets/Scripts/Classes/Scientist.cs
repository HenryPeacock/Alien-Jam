using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scientist : NPC
{

    // Use this for initialization
    new void Start()
    {
        base.Start();
        agent.angularSpeed = 360;
        job = "Scientist";

    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }
}
