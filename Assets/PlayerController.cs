using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    // Declaring public variables
    public float m_playerSpeed = 1.0f;
    public Transform m_infestedTransform;
    public bool m_flying = false;
    public GameObject m_playerVisual;
    public GameObject m_indicateInfest;
    public GameObject m_AreaOfEffect;
    public AudioSource m_nope;
    public GameObject m_alertPing;
    // Declaring private variables
    private Transform m_playerTransform;
    private Transform m_target;
    private float m_infestRange = 10.0f;
    private Vector3 m_distToTravel;
    private long m_startTime;
    private Vector3 m_distanceEachTime;

    // Use this for initialization
    void Start()
    {
        m_playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_flying)
        {
            // Load input controller for keyboard
            InputController();
            // Look at the mouse
            LookAtMouse();
            // Follow the infested target
            FollowInfested();
        }
        else
        {
            FlyToTarget();
        }
    }

    // Check player inputs and act on them
    void InputController()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_infestedTransform.position += (new Vector3(0.0f, 0.0f, 0.1f) * m_playerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_infestedTransform.position -= (new Vector3(0.1f, 0.0f, 0.0f) * m_playerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_infestedTransform.position -= (new Vector3(0.0f, 0.0f, 0.1f) * m_playerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_infestedTransform.position += (new Vector3(0.1f, 0.0f, 0.0f) * m_playerSpeed);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckInfest();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckIfInteractable();
        }
    }

    // Make the player object look at the mouse
    void LookAtMouse()
    {
        // Make the player look at the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            m_infestedTransform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    // Make the player objects transform attach to the object it's infesting
    void FollowInfested()
    {
        m_playerTransform.position = m_infestedTransform.position;
    }

    // Check if the object the player is attempting to infest is actually infestable
    void CheckInfest()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].transform.tag);
            if ((hits[i].transform.tag == "Infestable") && (hits[i].transform != m_infestedTransform))
            {
                if (CheckInfestForWalls(hits[i].transform.position))
                {
                    Infest(hits[i].transform);
                    i = hits.Length;
                }
            }
        }
    }

    // Infest the object the player has casted on
    void Infest(Transform _toInfest)
    {
        // Set the target and check if it's within the range limit of the infest ability
        m_target = _toInfest;
        m_distToTravel = m_playerTransform.position - m_target.position;
        if (m_distToTravel.magnitude < m_infestRange)
        {
            // If it is, set values to fly toward the new target
            //m_infestedTransform.gameObject.GetComponent<NPC>().ToggleInfested();
            m_infestedTransform = m_playerTransform;
            m_flying = true;
            m_startTime = System.DateTime.Now.Ticks;
            m_playerVisual.SetActive(true);
            m_indicateInfest.SetActive(false);
            //_toInfest.gameObject.GetComponent<NPC>().ToggleInfested();
        }
        else
        {
            // If we're not in range, enter the out of range function
            InfestOutofRange();
        }
    }

    // !!!! DO NOT OPEN !!!!
    void FlyToTarget()
    {
        // Just don't bother
        m_distToTravel = m_playerTransform.position - m_target.position;
        long elapsedTime = System.DateTime.Now.Ticks - m_startTime;
        TimeSpan elap = new TimeSpan(elapsedTime);
        m_distanceEachTime = m_distToTravel * ((float)elap.TotalMilliseconds/10000.0f);
        m_playerTransform.position -= m_distanceEachTime;
        if (elap.TotalMilliseconds >= 1000.0f)
        {
            m_flying = false;
            m_playerVisual.SetActive(false);
            m_indicateInfest.SetActive(true);
            m_infestedTransform = m_target;   
        }
    }
    //   !!!! HAZARD !!!!

    // If the Infest is out of range, give the player feedback
    void InfestOutofRange()
    {
        Debug.Log("Out of range");
        m_nope.Play();
        DisplayRing();
    }

    // Display the range ring
    void DisplayRing()
    {
        Debug.Log("Hello There");
        // Create the ring object
        GameObject ring = Instantiate(m_AreaOfEffect) as GameObject;
        // Connect it to the player
        ring.transform.parent = m_infestedTransform;
        // Make it remain with the player but on the floor
        float yPos = 0.01f - m_infestedTransform.position.y;
        ring.transform.localPosition = new Vector3(0.0f,yPos,0.0f);
        // Destroy it after 1 second
        Destroy(ring, 0.5f);
    }

    // Check if there's an object between the current infested and the attempted new one
    bool CheckInfestForWalls(Vector3 _targetPos)
    {
        //
        Vector3 directionToTarget = _targetPos - m_infestedTransform.position;
        // Cast a ray from self to target
        Ray ray= new Ray(m_infestedTransform.position, directionToTarget);
        Debug.DrawRay(m_infestedTransform.position, directionToTarget, Color.green);
        // Save in an array of objects hit
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        // Iterate through all objects hit and if there's any blockers, return that it cannot pass
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].transform.tag);
            if (hits[i].transform.tag == "InfestBlocker")
            {
                if (directionToTarget.magnitude > (hits[i].point - m_infestedTransform.position).magnitude)
                {
                    AlertPing(hits[i].point, hits[i].transform);
                    Debug.Log("Blocked");
                    return false;
                }
            }
        }
        return true;
    }

    // Ping an alert at the location of failure
    void AlertPing(Vector3 _pingPos, Transform objectHit)
    {
        float height = objectHit.localScale.y;
        _pingPos += new Vector3(0.0f, height, 0.0f);
        GameObject alert = Instantiate(m_alertPing) as GameObject;
        alert.transform.position = _pingPos;
        Destroy(alert, 0.5f);
    }

    // Check if the object the player is attempting to interact with is actually an interactable object
    void CheckIfInteractable()
    {
        //Debug.Log("Checking Interact");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++)
        {
            //Debug.Log(hits[i].transform.tag);
            if (hits[i].transform.gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                //Debug.Log("checking tag");
                if (CheckIfInInteractRange(hits[i].transform))
                {
                    if (hits[i].transform.tag == "NormalDoor")
                    {
                        OpenDoor(0, hits[i].transform);
                    }

                }
            }
        }
    }

    bool CheckIfInInteractRange(Transform _attemptedInteract)
    {
        return true;
    }

    void InteractWithObject(Transform _object)
    {

    }

    void OpenDoor(int _doorType, Transform _toOpen)
    {
        if (_doorType == 0)
        {
            //Debug.Log("Made it");
            _toOpen.parent.gameObject.GetComponent<DoorScript>().OpenToggle();
        }
    }

    void FlipSwitch(Transform _switch)
    {

    }

    void TurnOffCamera(Transform _camera)
    {

    }

    void EnterSpaceship(Transform _spaceship)
    {

    }


}


