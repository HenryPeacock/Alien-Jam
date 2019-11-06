using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorScript : MonoBehaviour {

    private bool m_isOpen = false;
    private bool m_isTogglingOpen = false;
    private bool m_isTogglingClosed = false;
    private bool m_playerOpening = false;
    private int m_rotMod = 0;

    public AudioSource m_doorSound;
    public OffMeshLink link;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_playerOpening)
        {
            if (m_isTogglingOpen == true)
            {
                if (m_rotMod < 45)
                {
                    transform.Rotate(new Vector3(0, 2, 0));
                    m_rotMod++;
                }
                else
                {
                    m_isTogglingOpen = false;
                    m_playerOpening = false;
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
                    m_playerOpening = false;
                }
            }
        }
        else
        {
            if (link.occupied)
            {
                m_isTogglingOpen = true;
                m_isTogglingClosed = false;
            }
            else
            {
                m_isTogglingOpen = false;
                m_isTogglingClosed = true;
            }
            if (m_isTogglingOpen == true)
            {
                if (m_rotMod < 45)
                {
                    transform.Rotate(new Vector3(0, 2, 0));
                    m_rotMod++;
                }
                else
                {
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

	}

    public void OpenToggle()
    {
        if (m_isOpen == true)
        {
            m_doorSound.Play();
            m_isOpen = false;
            m_isTogglingClosed = true;
            m_playerOpening = true;
        }
        else
        {
            m_doorSound.Play();
            m_isOpen = true;
            m_isTogglingOpen = true;
            m_playerOpening = true;
        }        
    }
}
