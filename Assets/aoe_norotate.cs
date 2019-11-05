using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoe_norotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
    }
}
