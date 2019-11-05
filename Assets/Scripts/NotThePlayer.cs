using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotThePlayer : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward*speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * speed);
        }
    }
}
