using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspiciousObject : MonoBehaviour {
    public bool alert;

	// Use this for initialization
	void Start () {
        alert = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (alert == true){
            Alert();
            alert = false;
        }
	}

    void Alert() {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Collider[] cols = Physics.OverlapSphere(transform.position, 20);
        for (int i = 0; i < cols.Length; i++) {
            if (!Physics.Linecast(transform.position, cols[i].transform.position, layerMask)){
                if (cols[i].gameObject.GetComponent<NPC>() != null)
                {
                    cols[i].gameObject.GetComponent<NPC>().Alert(transform);
                }
            }
        }
    }
}
