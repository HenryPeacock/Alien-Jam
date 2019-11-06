using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform m_target;
    public float m_distanceZ;
    public float m_distanceY;

    Vector3 moveDir;
    public float zoom;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (zoom < 0.8f)
            {
                zoom += 0.05f;
            }
        }
        else if (Input.mouseScrollDelta.y < 0) {
            if (zoom > -0.8f)
            {
                zoom -= 0.05f;
            }
        }
        moveDir = m_target.position - transform.position;
        transform.position = new Vector3(m_target.position.x, m_target.position.y + m_distanceY, m_target.position.z - m_distanceZ) + (moveDir*zoom);
	}
}
