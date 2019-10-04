using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startPosition;
    private Vector3 mVector;

	// Use this for initialization
	void Start () {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update () {

        //Camera Movement ---------------------------
        mVector = lookAt.position + startPosition;

        // Keeps camera centered on X
        mVector.x = 0; 

        transform.position = mVector;
        //-----------------------------------
	}
}
