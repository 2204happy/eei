using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour {
	public float angle;
	public bool straight;
	public bool pointingLeft;
	private float upMod;
	private float sideMod;
	private float dist;
	public float findist;
	// Use this for initialization
	void Start () {
		Physics2D.queriesHitTriggers = false;
		if (!straight)
		{
			upMod = Mathf.Cos(angle /180 * Mathf.PI);
			sideMod = Mathf.Sin(angle /180 * Mathf.PI);
		}
		else {
			upMod = 1;
			sideMod = 0;
		}
		if (pointingLeft) {
			sideMod = -sideMod;
		}
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformPoint(new Vector2(1*sideMod,1*upMod))-transform.position);
		dist = hit.distance;
		findist = dist / 50;
		if(findist > 1) {
			findist = 1;
		}
		if(findist < 0) {
			findist = 0;
		}
        
		//Debug.Log(hit.collider);
		//Debug.Log(hit.distance);
		Debug.DrawLine(transform.position, transform.TransformPoint(new Vector3(hit.distance/2*sideMod,hit.distance/2*upMod,0)),Color.green);
	}
}
