using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.queriesHitTriggers = false;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformPoint(Vector2.up)-transform.position);
        //transform.TransformPoint(new Vector3(0, 1, 0)));
		Debug.Log(hit.collider);
		Debug.Log(hit.distance);
		Debug.DrawLine(transform.position, transform.TransformPoint(new Vector3(0,hit.distance/2,0)),Color.green);
	}
}
