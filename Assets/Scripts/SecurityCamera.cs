using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {
    int layerMask;
    bool isSeeing;
    [SerializeField]
    int suspicion;
	// Use this for initialization
	void Start () {
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        layerMask = ~layerMask;
    }
	
	// Update is called once per frame
	void Update () {
        isSeeing = canSee(GameObject.FindGameObjectWithTag("Player").transform.position);
        if (isSeeing)
        {
            suspicion++;
        }
        else {
            suspicion--;
        }
        if (suspicion > 600) {
            Collider[] cols = Physics.OverlapSphere(transform.position, 100);
            for (int i = 0; i < cols.Length; i++) {
                //print(cols[i].gameObject.name);
                if (cols[i].gameObject.GetComponent<Guard>() != null)
                {
                    cols[i].gameObject.GetComponent<NPC>().Alert(transform.position);
                    
                }
            }
            suspicion = 0;

        }
	}

    public bool canSee(Vector3 objPos)
    {
        bool nearby = (Vector3.Distance(transform.position, objPos) < 3);
        bool lineOfSight = !Physics.Linecast(transform.position, objPos, layerMask);
        bool inViewCone = (Vector3.Distance(transform.position, objPos) < 20 && Vector3.Angle(transform.forward, (objPos - transform.position)) < 45);
        return ((lineOfSight && inViewCone) || (nearby && lineOfSight));
    }
}
