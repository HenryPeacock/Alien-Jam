using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    public float m_fadeTime;
    private Color m_baseColour;

	// Use this for initialization
	void Start ()
    {
        m_baseColour = GetComponent<MeshRenderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        m_baseColour.a -= m_fadeTime * Time.deltaTime;
        if (m_baseColour.a > 0.0f)
        {  
            GetComponent<MeshRenderer>().material.color = m_baseColour;
        }
	}
}
