using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Finish : MonoBehaviour {
    public bool PresidentYES;
    public bool launchActive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col) {
        if (col.GetComponent<NPC>().infested && PresidentYES && launchActive){
            print("You're Winner");
        }
    }
}
