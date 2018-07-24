﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleCtl : MonoBehaviour {
	private GameObject objSelf;
	public float turnModif;
	public float moveModif;
	public float friction;
	public float topSpeed;
	public float acceleration;
	public float brakeSpeed;
	public GameObject childObj;
	private int segment;
	private float speed;
	private Vector3 initPos;
	private Quaternion initRot;
	// Use this for initialization
	void Start () {
		objSelf = this.gameObject;
		speed = 0;
		initPos = objSelf.transform.position;
		initRot = objSelf.transform.rotation;
		segment = 0;
	}

	// Update is called once per frame
	void Update() {
		if (speed > topSpeed) {
			speed = topSpeed;
		}
		if (speed < 0) {
			speed = 0;
		}
        
		childObj.transform.localPosition = new Vector3(0, speed, 0);
		objSelf.transform.position = childObj.transform.position;
		speed -= friction;
	}
	public void turnLeft() {
		objSelf.transform.Rotate(new Vector3(0,0,turnModif));
	}

    public void turnRight()
    {
        objSelf.transform.Rotate(new Vector3(0, 0, -turnModif));
    }
	public void accelerate() {
		speed += acceleration;
	}
	public void brake() {
		speed -= brakeSpeed;
	}
	void OnCollisionEnter2D(Collision2D col) {
		resetVehicle();

	}
	void OnTriggerEnter2D(Collider2D col) {
		GameObject objCol = col.gameObject;
		segment = objCol.GetComponent<checkpointNum>().checkPoint;
		Debug.Log(segment);
	}
	public void resetVehicle() {
		objSelf.transform.position = initPos;
        speed = 0;
        objSelf.transform.rotation = initRot;
	}
}
