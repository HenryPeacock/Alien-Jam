using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    private bool m_isOpen = false;
    private bool m_isTogglingOpen = false;
    private bool m_isTogglingClosed = false;
    private int m_rotMod = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (m_isTogglingOpen == true)
        {
            if (m_rotMod < 45)
            {
                transform.Rotate(new Vector3(0, 2, 0));
                m_rotMod++;
            }
            else {
                m_isTogglingOpen = false;
            }
        }
        else if (m_isTogglingClosed == true)
        {
            if (m_rotMod > 0)
            {
                transform.Rotate(new Vector3(0, -2, 0));
                m_rotMod--;
            }
            else
            {
                m_isTogglingClosed = false;
            }
        }
	}

    public void OpenToggle()
    {
        if (m_isOpen == true)
        {
            m_isOpen = false;
            m_isTogglingClosed = true;
        }
        else
        {
            m_isOpen = true;
            m_isTogglingOpen = true;
        }        
    }
}
