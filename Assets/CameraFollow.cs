using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform m_target;
    public float m_distanceZ;
    public float m_distanceY;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(m_target.position.x, m_target.position.y + m_distanceY, m_target.position.z - m_distanceZ);
	}
}
