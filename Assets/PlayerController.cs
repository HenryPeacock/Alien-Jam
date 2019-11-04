using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Declaring public variables
    public float m_playerSpeed = 1.0f;
    public Transform m_infestedTransform;
    // Declaring private variables
    private Transform m_playerTransform;


    // Use this for initialization
    void Start()
    {
        m_playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Load input controller for keyboard
        InputController();
        // Look at the mouse
        LookAtMouse();
        // Follow the infested target
        FollowInfested();
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
        Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray2);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].transform.tag);
            if (hits[i].transform.tag == "Infestable")
            {

                Infest(hits[i].transform);
                i = hits.Length;
            }
        }
    }

    // Infest the object the player has casted on
    void Infest(Transform _toInfest)
    {
        Debug.Log("Works");
        m_infestedTransform = _toInfest;
    }


}
