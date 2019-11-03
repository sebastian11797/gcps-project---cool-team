using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    private Transform lookAt;
    private Vector3 startPosition;
    private Vector3 mVector;

    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0, 5, 5);

    public bool IntroScreen { set; get; }

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

        if (transition > 1.0f)
        {
            transform.position = mVector;
        }
        else
        {
            transform.position = Vector3.Lerp(mVector + animationOffset, mVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            //transform.LookAt (lookAt.position + Vector3.up);
        }

        
        //-----------------------------------
	}
}
