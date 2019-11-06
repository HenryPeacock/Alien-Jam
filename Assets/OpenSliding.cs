using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenSliding : MonoBehaviour {

    public float m_whichDoor;
    private bool m_isOpen = false;

    [SerializeField]
    private bool m_isTogglingOpen = false;

    [SerializeField]
    private bool m_isTogglingClosed = false;
    private int m_openMod = 0;

    [SerializeField]
    private bool m_playerOpening = false;

    public AudioSource m_doorSound;
    public OffMeshLink link;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (m_playerOpening)
        {
            if (m_isTogglingOpen == true)
            {
                if (m_openMod < 4)
                {
                    transform.position += (new Vector3(0.5f * m_whichDoor, 0, 0));
                    m_openMod++;
                }
                else
                {
                    m_isTogglingOpen = false;
                }
            }
            else if (m_isTogglingClosed == true)
            {
                if (m_openMod > 0)
                {
                    transform.position -= (new Vector3(0.5f * m_whichDoor, 0, 0));
                    m_openMod--;
                }
                else
                {
                    m_isTogglingClosed = false;
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
                if (m_openMod < 4)
                {
                    transform.position += (new Vector3(0.5f * m_whichDoor, 0, 0));
                    m_openMod++;
                }
                else
                {
                    m_isTogglingOpen = false;
                }
            }
            else if (m_isTogglingClosed == true)
            {
                if (m_openMod > 0)
                {
                    transform.position -= (new Vector3(0.5f * m_whichDoor, 0, 0));
                    m_openMod--;
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
