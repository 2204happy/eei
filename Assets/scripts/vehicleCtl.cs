using System.Collections;
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
	private float speed;
	// Use this for initialization
	void Start () {
		objSelf = this.gameObject;
		speed = 0;
	}

	// Update is called once per frame
	void Update() {
		if (speed > topSpeed) {
			speed = topSpeed;
		}
		if (speed < 0) {
			speed = 0;
		}
        
        Debug.Log(speed);
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
}
