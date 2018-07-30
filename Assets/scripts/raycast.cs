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
		RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, transform.TransformPoint(transform.forward));
		Debug.Log(hit.collider);
		Debug.Log(hit.distance);
		Debug.DrawLine(transform.position, transform.TransformPoint(transform.forward*hit.distance),Color.green);
	}
}
